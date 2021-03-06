﻿using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Windows.UI.Popups;
using DeliveryIngesup.Manager;
using DeliveryIngesup.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace DeliveryIngesup.ViewModel
{
    public class PaiementViewModel : ViewModelBase
    {
        #region Properties
        private Utilisateur _currentUser;

        public Utilisateur CurrentUser
        {
            get { return _currentUser; }
            set
            {
                _currentUser = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<Produit> _panier;

        public ObservableCollection<Produit> Panier
        {
            get { return _panier; }
            set
            {
                _panier = value;
                RaisePropertyChanged();
            }
        }

        private string _numeroCarte;

        public string NumeroCarte
        {
            get { return _numeroCarte; }
            set
            {
                _numeroCarte = value;
                RaisePropertyChanged();
            }
        }

        private string _adresse;

        public string Adresse
        {
            get { return _adresse; }
            set
            {
                _adresse = value;
                RaisePropertyChanged();
            }
        }

        private string _codePostal;

        public string CodePostal
        {
            get { return _codePostal; }
            set
            {
                _codePostal = value;
                RaisePropertyChanged();
            }
        }

        private string _ville;

        public string Ville
        {
            get { return _ville; }
            set
            {
                _ville = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Commands
        public ICommand PaiementCommand { get; set; }
        #endregion

        private readonly INavigationService _navigationService;

        public PaiementViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            MessengerInstance.Register<Utilisateur>(this, user => CurrentUser = user);
            MessengerInstance.Register<ObservableCollection<Produit>>(this, panier => Panier = panier);
            PaiementCommand = new RelayCommand(Paiement);
        }

        private void Paiement()
        {
            if (!String.IsNullOrEmpty(Adresse) && !String.IsNullOrEmpty(CodePostal) && !String.IsNullOrEmpty(Ville) && !String.IsNullOrEmpty(NumeroCarte))
            {
                if (PaiementManager.IsValidCardNumber(NumeroCarte))
                {
                    CommandeManager.Instance.CreerCommande(CurrentUser, Panier, Adresse, CodePostal, Ville);
                    new MessageDialog("Paiement validé");
                    _navigationService.NavigateTo("Main");
                }
                else
                {
                    new MessageDialog("Informations de paiement non valide");
                }
            }
            else
            {
                new MessageDialog("Veuillez remplir tous les champs");
            }
        }
    }
}