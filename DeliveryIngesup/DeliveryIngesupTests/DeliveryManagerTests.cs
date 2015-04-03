using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using DeliveryIngesup.Views;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace DeliveryIngesupTests
{
    //FIXME: Déporter dans un projet de test basique (non Windows 8.1)
    [TestClass]
    public class DeliveryManagerTests
    {
        private static Produit _produit;
        private static Commande _commande;
        private static Utilisateur _utilisateur;
        private static StorageFile _storage;

        private static SQLiteAsyncConnection Connection
        {
            get { return ConnectionHelper.GetConnection(new SQLitePlatformWinRT(), _storage.Path); }
        }

        [ClassInitialize]
        public static async Task Initialize(TestContext context)
        {
            _storage = ApplicationData.Current.LocalFolder.CreateFileAsync("deliveryingesup.bdd", CreationCollisionOption.OpenIfExists).AsTask().Result;

            await Connection.CreateTableAsync<Utilisateur>();
            await Connection.CreateTableAsync<Produit>();
            await Connection.CreateTableAsync<Commande>();

            _produit = new Produit{IdProduit = 999999, Nom = "Produit", Prix = 10, Quantite = 100};
            _commande = new Commande {Horaire = DateTime.Now, IdCommande = 999999, Utilisateur = "mail@mail.com"};
            _utilisateur = new Utilisateur {Email = "mail@mail.com", Nom = "Nom", Prenom = "Prenom", Password = ""};
        }

        [TestMethod]
        public async Task TestGetProduits()
        {
            var manager = DeliveryManager.Instance;

            if (manager.GetProduits().FirstOrDefault() == null)
                await Connection.InsertAsync(_produit);
            Assert.IsTrue(manager.GetProduits().Any());
        }

        [TestMethod]
        public async Task TestGetProduit()
        {
            await Connection.InsertAsync(_produit);
            var produit = DeliveryManager.Instance.GetProduit(_produit.IdProduit);

            Assert.AreEqual(produit.IdProduit, _produit.IdProduit);
            Assert.AreEqual(produit.Nom, _produit.Nom);
            Assert.AreEqual(produit.Prix, _produit.Prix);
            Assert.AreEqual(produit.Quantite, _produit.Quantite);

        }

        [TestMethod]
        public async Task TestCreerCommande()
        {
            await Connection.InsertAsync(_utilisateur);
            await Connection.InsertAsync(_produit);
            
            DeliveryManager.Instance.CreerCommande(_utilisateur, new ObservableCollection<Produit>{_produit});

            Assert.IsTrue(Connection.Table<Commande>().Where(c => c.Horaire == _commande.Horaire).CountAsync().Result > 0);
        }

        [ClassCleanup]
        public static async Task CleanUp()
        {
            await Connection.DeleteAsync(_produit);
            await Connection.InsertAsync(_utilisateur);
        }
    }
}
