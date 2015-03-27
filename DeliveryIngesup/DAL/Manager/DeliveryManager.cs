using System;
using System.Collections.ObjectModel;
using DAL.Models;
using SQLite;

namespace DAL.Manager
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

        public void CreerCommande(Utilisateur currentUser, ObservableCollection<Produit> panier)
        {
            try
            {
                var connection = new SQLiteAsyncConnection("deliveryingesup.bdd");
                connection.InsertAsync(new Commande () {Horaire = DateTime.Now, Utilisateur = currentUser.Email});
                var commande = connection.Table<Commande>()
                    .Where(c => c.Utilisateur == currentUser.Email)
                    .OrderByDescending(c => c.Horaire).FirstAsync();

                foreach (var produit in panier)
                {
                    connection.InsertAsync(new CommandeProduit() {Commande = commande.Id, Produit = produit.IdProduit});
                }
            }
            catch (Exception)
            {
                throw new Exception("La connexion à la base de données n'a pas pu être établie.");
            }
        }
    }
}
