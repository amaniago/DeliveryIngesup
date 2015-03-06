using System.Collections.ObjectModel;
using DeliveryIngesup.Models;

namespace DeliveryIngesup.Manager
{
    interface IDeliveryManager
    {
        ObservableCollection<Produit> GetProduits();
        void CreerCommande(string email, string produit, string horaire);
    }
}
