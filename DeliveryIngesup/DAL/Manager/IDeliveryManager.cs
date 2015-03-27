using System.Collections.ObjectModel;
using DAL.Models;

namespace DAL.Manager
{
    interface IDeliveryManager
    {
        ObservableCollection<Produit> GetProduits();
        Produit GetProduit(int id);
        void CreerCommande(Utilisateur currentUser, ObservableCollection<Produit> panier);
    }
}
