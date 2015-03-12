using System.Collections.ObjectModel;
using DeliveryIngesup.Models;

namespace DeliveryIngesup.Manager
{
    interface IDeliveryManager
    {
        ObservableCollection<Produit> GetProduits();
        Produit GetProduit(int id);
        void CreerCommande(Utilisateur currentUser, ObservableCollection<Produit> panier);
    }
}
