using System;
using System.Linq;
using DataAccess.Models;
using SQLite.Net.Async;
using SQLite.Net.Interop;

namespace DataAccess.Manager
{
    public class UserManager : IUserManager
    {
        private static UserManager _instance;
        private static ISQLitePlatform _platform;
        private static string _path;

        private UserManager(ISQLitePlatform platform, string path)
        {
            _platform = platform;
            _path = path;
        }

        public static UserManager Instance(ISQLitePlatform platform, string path)
        {
            return _instance ?? (_instance = new UserManager(platform, path)); 
        }

        private static SQLiteAsyncConnection Connection
        {
            get { return ConnectionHelper.GetConnection(_platform, _path); }
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
