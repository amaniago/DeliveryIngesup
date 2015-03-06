using System;
using System.Linq;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;
using DeliveryIngesup.Models;
using SQLite;

namespace DeliveryIngesup.Manager
{
    public class UserManager
    {
        private static UserManager _instance;

        private UserManager() { }

        public static UserManager Instance
        {
            get { return _instance ?? (_instance = new UserManager()); }
        }

        public async void Initialisation()
        {
            var connection = new SQLiteAsyncConnection("deliveryingesup.bdd");
            //await connection.DropTableAsync<Utilisateur>();
            await connection.CreateTableAsync<Utilisateur>();
            //await connection.InsertAsync(new Utilisateur()
            //{
            //    Email = "test@gmail.com",
            //    Nom = "Test",
            //    Password = ComputeMd5("Test"),
            //    Prenom = "Test"
            //});
        }

        public Utilisateur Connexion(string email, string password)
        {
            password = ComputeMd5(password);

            try
            {
                var connection = new SQLiteAsyncConnection("deliveryingesup.bdd");

                Utilisateur user = connection.Table<Utilisateur>().Where(u => u.Email == email && u.Password == password).FirstAsync().Result;

                return user;
            }
            catch (Exception)
            {
                return new Utilisateur();
            }
        }

        public Utilisateur Inscription(Utilisateur nouvelUtilisateur, string checkPassword)
        {
            try
            {
                //TODO : Check password
                nouvelUtilisateur.Password = ComputeMd5(nouvelUtilisateur.Password);
                var connection = new SQLiteAsyncConnection("deliveryingesup.bdd");

                int x = 0;
                if(connection.Table<Utilisateur>().ToListAsync().Result.All(u => u.Email != nouvelUtilisateur.Email))
                    x = connection.InsertAsync(nouvelUtilisateur).Result;

                return x > 0 ? connection.Table<Utilisateur>().Where(u => u.Email == nouvelUtilisateur.Email).FirstAsync().Result : new Utilisateur();
            }
            catch (Exception)
            {
                return new Utilisateur();
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
