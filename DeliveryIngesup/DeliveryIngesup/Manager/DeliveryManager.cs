using System;
using System.Collections.ObjectModel;
using DeliveryIngesup.Models;
using SQLite;

namespace DeliveryIngesup.Manager
{
    class DeliveryManager : IDeliveryManager
    {
        public async void Initialiser()
        {
            var connection = new SQLiteAsyncConnection("deliveryLocal.bdd");
            await connection.CreateTableAsync<Produit>();
            await connection.CreateTableAsync<Commande>();
            await connection.CreateTableAsync<Utilisateur>();
        }

        public ObservableCollection<Produit> GetAllProduit()
        {
            try
            {
                var con = new SQLiteAsyncConnection("deliveryLocal.bdd");
                return new ObservableCollection<Produit>(con.Table<Produit>().ToListAsync().Result);
            }
            catch (Exception)
            {
                throw new Exception("La connexion à la base de données n'a pas pu être établie.");
            }
        }

        public void AddUser(Utilisateur user)
        {
            try
            {
                var con = new SQLiteAsyncConnection("deliveryLocal.bdd");
                con.InsertAsync(user);
            }
            catch (Exception)
            {
                throw new Exception("La connexion à la base de données n'a pas pu être établie.");
            }
        }

        public bool CheckUser(string email, string mdp)
        {
            try
            {
                var con = new SQLiteAsyncConnection("deliveryLocal.bdd");
                return con.Table<Utilisateur>().Where(u => u.Email == email && u.Password == mdp).ToListAsync().Result.Count == 1;
            }
            catch (Exception)
            {
                throw new Exception("La connexion à la base de données n'a pas pu être établie.");
            }
        }

        public void AddCommand(string email, string produit, string horaire)
        {
            try
            {
                var con = new SQLiteAsyncConnection("deliveryLocal.bdd");
                con.InsertAsync(new Commande {Utilisateur = email, Produit = produit, Horaire = horaire});
            }
            catch (Exception)
            {
                throw new Exception("La connexion à la base de données n'a pas pu être établie.");
            }
        }
    }
}
