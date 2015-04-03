using System;
using System.Linq;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage;
using Windows.Storage.Streams;
using DataAccess;
using DataAccess.Models;
using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Platform.WinRT;

namespace DeliveryIngesup.Manager
{
    public class UserManager : DeliveryIngesup.Manager.IUserManager
    {
        private static UserManager _instance;
        private static StorageFile _storage;

        private UserManager()
        {
            _storage = ApplicationData.Current.LocalFolder.CreateFileAsync("deliveryingesup.bdd", CreationCollisionOption.OpenIfExists).AsTask().Result;
        }

        public static UserManager Instance
        {
            get { return _instance ?? (_instance = new UserManager()); }
        }

        private static SQLiteAsyncConnection Connection
        {
            get { return ConnectionHelper.GetConnection(new SQLitePlatformWinRT(), _storage.Path); }
        }

        public async void Initialisation()
        {
            //await connection.DropTableAsync<Utilisateur>();
            await Connection.CreateTableAsync<Utilisateur>();
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
                return Connection.Table<Utilisateur>().Where(u => u.Email == email && u.Password == password).FirstAsync().Result;
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

                int x = 0;
                if(Connection.Table<Utilisateur>().ToListAsync().Result.All(u => u.Email != nouvelUtilisateur.Email))
                    x = Connection.InsertAsync(nouvelUtilisateur).Result;

                return x > 0 ? Connection.Table<Utilisateur>().Where(u => u.Email == nouvelUtilisateur.Email).FirstAsync().Result : new Utilisateur();
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
