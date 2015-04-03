
using System.Collections.ObjectModel;
using DeliveryIngesup.Models;

namespace DeliveryIngesup.Manager
{
    public interface ILivreurManager
    {
        Livreur Connexion(string email, string password);
        void SaveLivraison(ObservableCollection<Commande> listeCommandesSelectionnees, Livreur livreur);
    }
}
