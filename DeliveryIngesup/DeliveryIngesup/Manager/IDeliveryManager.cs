using System.Collections.ObjectModel;
using DeliveryIngesup.Models;

namespace DeliveryIngesup.Manager
{
    interface IDeliveryManager
    {
        void Initialiser();
        ObservableCollection<Produit> GetAllProduit();
        void AddUser(Utilisateur user);
        bool CheckUser(string email, string mdp);
        void AddCommand(string email, string produit, string horaire);
    }
}
