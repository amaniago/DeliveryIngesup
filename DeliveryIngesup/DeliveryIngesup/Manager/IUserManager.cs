using DAL.Models;

namespace DeliveryIngesup.Manager
{
    public interface IUserManager
    {
        Utilisateur Connexion(string email, string password);
        Utilisateur Inscription(Utilisateur nouvelUtilisateur, string checkPassword);
    }
}
