using System.Collections.ObjectModel;
using DataAccess.Models;

namespace DeliveryIngesup.Manager
{
    interface IDeliveryManager
    {
        ObservableCollection<Produit> GetProduits();
        Produit GetProduit(int id);
        void CreerCommande(Utilisateur currentUser, ObservableCollection<Produit> panier);
    }
}
