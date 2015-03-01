using System;
using System.Collections.ObjectModel;
using DeliveryIngesup.Models;

namespace DeliveryIngesup.Manager
{
    class MockDeliveryManager : IDeliveryManager
    {
        public void Initialiser()
        {
            //Fonctionnalité de test
        }

        public ObservableCollection<Produit> GetAllProduit()
        {
            return new ObservableCollection<Produit>
            {
                new Produit {Nom = "Nom1", Prix = "1"},
                new Produit {Nom = "Nom2", Prix = "2"},
                new Produit {Nom = "Nom3", Prix = "3"},
                new Produit {Nom = "Nom4", Prix = "4"}
            };
        }

        public void AddUser(Utilisateur user)
        {
            throw new NotImplementedException();
        }

        public bool CheckUser(string email, string mdp)
        {
            throw new NotImplementedException();
        }

        public void AddCommand(string email, string produit, string horaire)
        {
            throw new NotImplementedException();
        }
    }
}
