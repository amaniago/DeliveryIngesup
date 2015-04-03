using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using DataAccess;
using DataAccess.Models;
using DeliveryIngesup.Manager;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using SQLite;
using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Platform.WinRT;

namespace DeliveryIngesupTests
{
    //FIXME: Déporter dans un projet de test basique (non Windows 8.1)
    [TestClass]
    public class UserManagerTests
    {
        private static Utilisateur _utilisateur;
        private const String Password = "Password";
        private static StorageFile _storage;

        private static SQLiteAsyncConnection Connection
        {
            get { return ConnectionHelper.GetConnection(new SQLitePlatformWinRT(), _storage.Path); }
        }

        [ClassInitialize]
        public static async Task Initialize(TestContext context)
        {
            _storage = ApplicationData.Current.LocalFolder.CreateFileAsync("deliveryingesup.bdd", CreationCollisionOption.OpenIfExists).AsTask().Result;
            _utilisateur = new Utilisateur { Email = "mail@mail.com", Password = "", Nom = "Nom", Prenom = "Prenom" };
            _utilisateur.Password = UserManager.Instance.GetType()
                .GetTypeInfo()
                .GetDeclaredMethod("ComputeMd5")
                .Invoke(UserManager.Instance, new object[] { Password }) as string;

            await Connection.CreateTableAsync<Utilisateur>();
        }

        [ClassCleanup]
        public static async Task CleanUp()
        {
            await Connection.DeleteAsync(_utilisateur);
        }

        [TestMethod]
        public async Task TestConnexion()
        {
            if (await Connection.Table<Utilisateur>().Where(u => u.Email == _utilisateur.Email).CountAsync() == 0)
                await Connection.InsertAsync(_utilisateur);

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
            await Connection.CreateTableAsync<Utilisateur>();
            UserManager.Instance.Inscription(_utilisateur, Password);

            Assert.IsTrue(await Connection.Table<Utilisateur>().Where(u => _utilisateur.Email == u.Email).CountAsync() > 0);
        }
    }
}
