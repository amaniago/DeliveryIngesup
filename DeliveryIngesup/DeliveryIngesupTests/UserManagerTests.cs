using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DAL.Manager;
using DAL.Models;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using SQLite;

namespace DeliveryIngesupTests
{
    [TestClass]
    public class UserManagerTests
    {
        private static SQLiteAsyncConnection _connection;
        private static Utilisateur _utilisateur;
        private const String Password = "Password";

        [ClassInitialize]
        public static async Task Initialize(TestContext context)
        {
            _connection = new SQLiteAsyncConnection("deliveryingesup.bdd");
            _utilisateur = new Utilisateur { Email = "mail@mail.com", Password = "", Nom = "Nom", Prenom = "Prenom" };
            _utilisateur.Password = UserManager.Instance.GetType()
                .GetTypeInfo()
                .GetDeclaredMethod("ComputeMd5")
                .Invoke(UserManager.Instance, new object[] { Password }) as string;
           
            await _connection.CreateTableAsync<Utilisateur>();
        }

        [ClassCleanup]
        public static async Task CleanUp()
        {
            await _connection.DeleteAsync(_utilisateur);
        }

        [TestMethod]
        public async Task TestConnexion()
        {
            if (await _connection.Table<Utilisateur>().Where(u => u.Email == _utilisateur.Email).CountAsync() == 0)
                await _connection.InsertAsync(_utilisateur);

            var connectedUser = UserManager.Instance.Connexion(_utilisateur.Email, Password);

            Assert.IsNotNull(connectedUser);
            Assert.AreEqual(_utilisateur.Email, connectedUser.Email);
            Assert.AreEqual(_utilisateur.Nom, connectedUser.Nom);
            Assert.AreEqual(_utilisateur.Prenom, connectedUser.Prenom);
            Assert.AreEqual(_utilisateur.Password, connectedUser.Password);

        }

        [TestMethod]
        public async Task TestInscription()
        {
            await _connection.CreateTableAsync<Utilisateur>();
            UserManager.Instance.Inscription(_utilisateur, Password);

            Assert.IsTrue(await _connection.Table<Utilisateur>().Where(u => _utilisateur.Email == u.Email).CountAsync() > 0);
        }
    }
}
