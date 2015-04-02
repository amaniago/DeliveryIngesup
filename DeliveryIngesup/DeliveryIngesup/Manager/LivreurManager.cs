using System;
using System.Linq;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;
using DeliveryIngesup.Models;
using SQLite;

namespace DeliveryIngesup.Manager
{
    public class LivreurManager
    {
        private static LivreurManager _instance;

        private LivreurManager() { }

        public static LivreurManager Instance
        {
            get { return _instance ?? (_instance = new LivreurManager()); }
        }

        public Livreur Connexion(string nom, string password)
        {
            password = ComputeMd5(password);

            try
            {
                var connection = new SQLiteAsyncConnection("deliveryingesup.bdd");

                Livreur user = connection.Table<Livreur>().Where(l => l.Nom == nom && l.Password == password).FirstAsync().Result;

                return user;
            }
            catch (Exception)
            {
                return new Livreur();
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
