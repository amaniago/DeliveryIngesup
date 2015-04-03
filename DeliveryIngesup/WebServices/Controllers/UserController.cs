using System.Web.Http;
using DAL.Manager;
using Model.Models;
using Newtonsoft.Json;
using SQLite.Net.Platform.Win32;

namespace WebServices.Controllers
{
    public class UserController : ApiController
    {
        UserManager manager = UserManager.Instance(new SQLitePlatformWin32(), "delivery.bdd");

        public string Connexion(string email, string password)
        {
            return JsonConvert.SerializeObject(manager.Connexion(email, password));
        }

        public string Inscription(Utilisateur utilisateur, string checkPassword)
        {
            return JsonConvert.SerializeObject(manager.Inscription(utilisateur, checkPassword));
        }
    }
}
