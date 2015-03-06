using System;
using System.Collections.ObjectModel;
using DeliveryIngesup.Models;
using SQLite;

namespace DeliveryIngesup.Manager
{
    public class DeliveryManager : IDeliveryManager
    {
        private static DeliveryManager _instance;

        private DeliveryManager() { }

        public static DeliveryManager Instance
        {
            get { return _instance ?? (_instance = new DeliveryManager()); }
        }

        public async void Initialisation()
        {
            var connection = new SQLiteAsyncConnection("deliveryingesup.bdd");
            //await connection.DropTableAsync<Produit>();
            await connection.CreateTableAsync<Produit>();
            await connection.CreateTableAsync<Commande>();
            await connection.CreateTableAsync<Utilisateur>();
            await connection.CreateTableAsync<CommandeProduit>();

            //await connection.InsertAsync(new Produit() {Nom = "Fraise", Prix = 10});
            //await connection.InsertAsync(new Produit() {Nom = "Cerise", Prix = 15});
            //await connection.InsertAsync(new Produit() {Nom = "Poire", Prix = 5});
            //await connection.InsertAsync(new Produit() {Nom = "Pomme", Prix = 7});
        }

        public ObservableCollection<Produit> GetProduits()
        {
            try
            {
                var connection = new SQLiteAsyncConnection("deliveryingesup.bdd");
                return new ObservableCollection<Produit>(connection.Table<Produit>().ToListAsync().Result);
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
                var connection = new SQLiteAsyncConnection("deliveryingesup.bdd");

                return connection.Table<Produit>().Where(p => p.IdProduit == id).FirstAsync().Result;
            }
            catch (Exception)
            {
                throw new Exception("La connexion à la base de données n'a pas pu être établie.");
            }
        }

        public void CreerCommande(string email, string produit, string horaire)
        {
            try
            {
                var connection = new SQLiteAsyncConnection("deliveryingesup.bdd");
                //con.InsertAsync(new Commande {CurrentUser = email, Produit = produit, Horaire = horaire});
            }
            catch (Exception)
            {
                throw new Exception("La connexion à la base de données n'a pas pu être établie.");
            }
        }
    }
}
