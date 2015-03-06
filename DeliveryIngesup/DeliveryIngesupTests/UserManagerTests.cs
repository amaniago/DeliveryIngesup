using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DeliveryIngesup.Manager;
using DeliveryIngesup.Models;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using SQLite;

namespace DeliveryIngesupTests
{
    [TestClass]
    public class UserManagerTests
    {
        [TestMethod]
        public async Task TestConnexion()
        {
            var connection = new SQLiteAsyncConnection("deliveryingesup.bdd");
            var user = new Utilisateur { Email = "mail@mail.com", Password = "password", Nom = "Nom", Prenom = "Prenom" };

            await connection.CreateTableAsync<Utilisateur>();

            user.Password = UserManager.Instance.GetType()
                .GetTypeInfo()
                .GetDeclaredMethod("ComputeMd5")
                .Invoke(UserManager.Instance, new object[]{user.Password}) as string;

            await connection.InsertAsync(user);

            var connectedUser = UserManager.Instance.Connexion(user.Email, "password");

            Assert.AreEqual(user.Email, connectedUser.Email);
            Assert.AreEqual(user.Nom, connectedUser.Nom);
            Assert.AreEqual(user.Prenom, connectedUser.Prenom);
            Assert.AreEqual(user.Password, connectedUser.Password);

            await connection.DeleteAsync(user);
        }

        [TestMethod]
        public async Task TestInscription()
        {
            var connection = new SQLiteAsyncConnection("deliveryingesup.bdd");
            var user = new Utilisateur { Email = "mail@mail.com", Password = "password", Nom = "Nom", Prenom = "Prenom" };
            await connection.CreateTableAsync<Utilisateur>();
            UserManager.Instance.Inscription(user, "password");

            Assert.IsTrue(await connection.Table<Utilisateur>().Where(u => user.Email == u.Email).CountAsync() > 0);

            await connection.DeleteAsync(user);
        }
    }
}
