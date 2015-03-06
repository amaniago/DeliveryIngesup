using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliveryIngesup.Models;

namespace DeliveryIngesup.Manager
{
    public interface IUserManager
    {
        Utilisateur Connexion(string email, string password);
        Utilisateur Inscription(Utilisateur nouvelUtilisateur, string checkPassword);
    }
}
