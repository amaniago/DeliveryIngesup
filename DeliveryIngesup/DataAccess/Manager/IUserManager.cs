using DataAccess.Models;

namespace DataAccess.Manager
{
    public interface IUserManager
    {
        Utilisateur Connexion(string email, string password);
        Utilisateur Inscription(Utilisateur nouvelUtilisateur, string checkPassword);
    }
}
