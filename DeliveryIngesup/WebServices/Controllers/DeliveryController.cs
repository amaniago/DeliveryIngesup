using System.Collections.ObjectModel;
using System.Web.Http;
using DAL.Manager;
using Model.Models;
using Newtonsoft.Json;
using SQLite.Net.Platform.Win32;

namespace WebServices.Controllers
{
    class DeliveryController : ApiController
    {
        DeliveryManager manager = DeliveryManager.Instance(new SQLitePlatformWin32(), "delivery.bdd");

        public string GetProduit(int id)
        {
            return JsonConvert.SerializeObject(manager.GetProduit(id));
        }

        public string GetProduits()
        {
            return JsonConvert.SerializeObject(manager.GetProduits());
        }

        public void CreerCommande(Utilisateur user, ObservableCollection<Produit> productList)
        {
            manager.CreerCommande(user, productList);
        }


    }
}
