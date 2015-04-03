using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using DeliveryIngesup.Manager;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using SQLite;
using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Platform.WinRT;

namespace DeliveryIngesupTests
{
    [TestClass]
    public class DeliveryManagerTests
    {
        private static SQLiteAsyncConnection _connection;
        private static Produit _produit;
        private static Commande _commande;
        private static Utilisateur _utilisateur;

        [ClassInitialize]
        public static async Task Initialize(TestContext context)
        {
            _connection = new SQLiteAsyncConnection(() => new SQLiteConnectionWithLock(new SQLitePlatformWinRT(), new SQLiteConnectionString("deliveryingesup.bdd", false)));

            await _connection.CreateTableAsync<Utilisateur>();
            await _connection.CreateTableAsync<Produit>();
            await _connection.CreateTableAsync<Commande>();

            _produit = new Produit{IdProduit = 999999, Nom = "Produit", Prix = 10, Quantite = 100};
            _commande = new Commande {Horaire = DateTime.Now, IdCommande = 999999, Utilisateur = "mail@mail.com"};
            _utilisateur = new Utilisateur {Email = "mail@mail.com", Nom = "Nom", Prenom = "Prenom", Password = ""};
        }

        [TestMethod]
        public async Task TestGetProduits()
        {
            var manager = DeliveryManager.Instance;

            if (manager.GetProduits().FirstOrDefault() == null)
                 await _connection.InsertAsync(_produit);
            Assert.IsTrue(manager.GetProduits().Any());
        }

        [TestMethod]
        public async Task TestGetProduit()
        {
            await _connection.InsertAsync(_produit);
            var produit = DeliveryManager.Instance.GetProduit(_produit.IdProduit);

            Assert.AreEqual(produit.IdProduit, _produit.IdProduit);
            Assert.AreEqual(produit.Nom, _produit.Nom);
            Assert.AreEqual(produit.Prix, _produit.Prix);
            Assert.AreEqual(produit.Quantite, _produit.Quantite);

        }

        [TestMethod]
        public async Task TestCreerCommande()
        {
            await _connection.InsertAsync(_utilisateur);
            await _connection.InsertAsync(_produit);
            
            DeliveryManager.Instance.CreerCommande(_utilisateur, new ObservableCollection<Produit>{_produit});

            Assert.IsTrue(_connection.Table<Commande>().Where(c => c.Horaire == _commande.Horaire).CountAsync().Result > 0);
        }

        [ClassCleanup]
        public static async Task CleanUp()
        {
            await _connection.DeleteAsync(_produit);
            await _connection.InsertAsync(_utilisateur);
        }
    }
}
