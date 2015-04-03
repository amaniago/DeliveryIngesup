using System.Collections.ObjectModel;
using DeliveryIngesup.Models;

namespace DeliveryIngesup.Manager
{
    interface ICommandeManager
    {
        ObservableCollection<Produit> GetProduits();
        Produit GetProduit(int id);
        void CreerCommande(Utilisateur currentUser, ObservableCollection<Produit> panier, string adresse, string codePostal, string ville);
    }
}
