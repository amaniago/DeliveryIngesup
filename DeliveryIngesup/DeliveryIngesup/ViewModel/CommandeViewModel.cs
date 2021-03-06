﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Windows.Input;
using Windows.ApplicationModel.Store;
using Windows.UI.Popups;
using DeliveryIngesup.Manager;
using DeliveryIngesup.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace DeliveryIngesup.ViewModel
{
    public class CommandeViewModel : ViewModelBase
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

        private ObservableCollection<Produit> _listeProduits;

        public ObservableCollection<Produit> ListeProduits
        {
            get { return _listeProduits; }
            set
            {
                _listeProduits = value;
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

        #endregion

        #region Commands

        public ICommand AjouterProduitCommand { get; set; }
        public ICommand SupprimerProduitCommand { get; set; }
        public ICommand ValiderPanierCommand { get; set; }
        public ICommand RetourCommand { get; set; }

        #endregion

        private readonly INavigationService _navigationService;
        public CommandeViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            MessengerInstance.Register<Utilisateur>(this, user => CurrentUser = user);
            CommandeManager.Instance.Initialisation();
            ListeProduits = CommandeManager.Instance.GetProduits();
            AjouterProduitCommand = new RelayCommand<int>(param => AjouterArticle(CommandeManager.Instance.GetProduit(param)));
            SupprimerProduitCommand = new RelayCommand<int>(param => SupprimerArticle(CommandeManager.Instance.GetProduit(param)));
            Panier = new ObservableCollection<Produit>();
            ValiderPanierCommand = new RelayCommand(ValiderPanier);
            RetourCommand = new RelayCommand(Retour);
        }

        private void Retour()
        {
            _navigationService.NavigateTo("Main");
        }

        private void AjouterArticle(Produit produit)
        {
            var panierTemp = Panier;

            Produit produitExistant = panierTemp.FirstOrDefault(p => p.IdProduit == produit.IdProduit);

            if (produitExistant == null)
            {
                produit.Quantite++;
                panierTemp.Add(produit);
            }
            else
            {
                produitExistant.Quantite++;
            }

            Panier = new ObservableCollection<Produit>(panierTemp);
        }

        private void SupprimerArticle(Produit produit)
        {
            Panier.Remove(Panier.FirstOrDefault(p => p.IdProduit == produit.IdProduit));
        }

        private void ValiderPanier()
        {
            if (Panier.Count > 0)
            {
                MessengerInstance.Send(Panier);
                _navigationService.NavigateTo("Paiement");
            }
            else
            {
                new MessageDialog("Panier vide").ShowAsync();
            }
        }
    }
}