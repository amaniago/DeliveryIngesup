using System;
using System.Collections.ObjectModel;
using Windows.Storage;
using DataAccess;
using DataAccess.Models;
using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Platform.WinRT;

namespace DeliveryIngesup.Manager
{
    public class DeliveryManager : IDeliveryManager
    {
        private static DeliveryManager _instance;
        private static StorageFile _storage;

        private DeliveryManager()
        {
            _storage = ApplicationData.Current.LocalFolder.CreateFileAsync("deliveryingesup.bdd", CreationCollisionOption.OpenIfExists).AsTask().Result;
        }

        private static SQLiteAsyncConnection Connection
        {
            get { return ConnectionHelper.GetConnection(new SQLitePlatformWinRT(), _storage.Path); }
        }

        public static DeliveryManager Instance
        {
            get { return _instance ?? (_instance = new DeliveryManager()); }
        }

        public async void Initialisation()
        {
            //await connection.DropTableAsync<Produit>();
            await Connection.CreateTableAsync<Produit>();
            await Connection.CreateTableAsync<Commande>();
            await Connection.CreateTableAsync<Utilisateur>();
            await Connection.CreateTableAsync<CommandeProduit>();

            //await connection.InsertAsync(new Produit() {Nom = "Fraise", Prix = 10});
            //await connection.InsertAsync(new Produit() {Nom = "Cerise", Prix = 15});
            //await connection.InsertAsync(new Produit() {Nom = "Poire", Prix = 5});
            //await connection.InsertAsync(new Produit() {Nom = "Pomme", Prix = 7});
        }

        public ObservableCollection<Produit> GetProduits()
        {
            try
            {
                return new ObservableCollection<Produit>(Connection.Table<Produit>().ToListAsync().Result);
            }
            catch (Exception)
            {
                throw new Exception("La connexion à la base de données n'a pas pu être établie.");
            }
        }

        public Produit GetProduit(int id)
        {
            try
            {
                return Connection.Table<Produit>().Where(p => p.IdProduit == id).FirstAsync().Result;
            }
            catch (Exception)
            {
                throw new Exception("La connexion à la base de données n'a pas pu être établie.");
            }
        }

        public void CreerCommande(Utilisateur currentUser, ObservableCollection<Produit> panier)
        {
            try
            {
                Connection.InsertAsync(new Commande () {Horaire = DateTime.Now, Utilisateur = currentUser.Email});
                var commande = Connection.Table<Commande>()
                    .Where(c => c.Utilisateur == currentUser.Email)
                    .OrderByDescending(c => c.Horaire).FirstAsync();

                foreach (var produit in panier)
                {
                    Connection.InsertAsync(new CommandeProduit() {Commande = commande.Id, Produit = produit.IdProduit});
                }
            }
            catch (Exception)
            {
                throw new Exception("La connexion à la base de données n'a pas pu être établie.");
            }
        }
    }
}
