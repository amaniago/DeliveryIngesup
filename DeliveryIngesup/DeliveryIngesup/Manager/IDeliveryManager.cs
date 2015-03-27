using System.Collections.ObjectModel;
using DAL.Models;

namespace DeliveryIngesup.Manager
{
    interface IDeliveryManager
    {
        ObservableCollection<Produit> GetProduits();
        Produit GetProduit(int id);
        void CreerCommande(Utilisateur currentUser, ObservableCollection<Produit> panier);
    }
}
