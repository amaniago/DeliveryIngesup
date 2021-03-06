﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using DeliveryIngesup.Models;
using SQLite;

namespace DeliveryIngesup.Manager
{
    public class LivreurManager : ILivreurManager
    {
        private static LivreurManager _instance;

        private LivreurManager() { }

        public static LivreurManager Instance
        {
            get { return _instance ?? (_instance = new LivreurManager()); }
        }



        public Livreur Connexion(string email, string password)
        {

            password = ComputeMd5(password);

            try
            {
                var connection = new SQLiteAsyncConnection("deliveryingesup.bdd");

                //connection.CreateTableAsync<Livreur>();
                //var var = connection.InsertAsync(new Livreur() {Email = email, Password = password}).Result;

                Livreur user = connection.Table<Livreur>().Where(l => l.Email == email && l.Password == password).FirstAsync().Result;

                return user;
            }
            catch (Exception e)
            {
                return new Livreur();
            }
        }

        public void SaveLivraison(ObservableCollection<Commande> listeCommandesSelectionnees, Livreur livreur)
        {
            try
            {
                var connection = new SQLiteAsyncConnection("deliveryingesup.bdd");

                foreach (var commande in listeCommandesSelectionnees)
                {
                    connection.InsertAsync(new LivreurCommande() { Commande = commande.IdCommande, Livreur =  livreur.IdLivreur});
                    commande.Etat = "En cours de livraison";
                    connection.UpdateAsync(commande);
                }
            }
            catch (Exception)
            {
                throw new Exception("La connexion à la base de données n'a pas pu être établie.");
            }
        }


        private static string ComputeMd5(string str)
        {
            var alg = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
            IBuffer buff = CryptographicBuffer.ConvertStringToBinary(str, BinaryStringEncoding.Utf8);
            var hashed = alg.HashData(buff);
            var res = CryptographicBuffer.EncodeToHexString(hashed);
            return res;
        }
    }
}
