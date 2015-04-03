using System;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Windows.UI.Popups;
using DeliveryIngesup.Manager;
using DeliveryIngesup.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace DeliveryIngesup.ViewModel
{

    public class MainViewModel : ViewModelBase
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

        private bool _isLivreur;

        public bool IsLivreur
        {
            get { return _isLivreur; }
            set
            {
                _isLivreur = value;
                RaisePropertyChanged();
            }
        }

        public Livreur CurrentLivreur { get; set; }

        #endregion

        #region Commands

        public ICommand ConnexionCommand { get; set; }
        public ICommand InscriptionCommand { get; set; }
        #endregion

        private readonly INavigationService _navigationService;
        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            UserManager.Instance.Initialisation();
            CurrentUser = new Utilisateur();
            ConnexionCommand = new RelayCommand(Connexion);
            InscriptionCommand = new RelayCommand(Inscription);
        }

        private void Connexion()
        {
            if (!String.IsNullOrEmpty(CurrentUser.Email) && !String.IsNullOrEmpty(CurrentUser.Password))
            {
                Regex regex = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");

                if (regex.IsMatch(CurrentUser.Email))
                {
                    if (IsLivreur)
                    {
                        CurrentLivreur = LivreurManager.Instance.Connexion(CurrentUser.Email, CurrentUser.Password);
                        if (CurrentLivreur != null)
                        {
                            MessengerInstance.Send(CurrentLivreur);
                            _navigationService.NavigateTo("Livraison");
                        }
                        else
                        {
                            CurrentLivreur = new Livreur();
                            new MessageDialog("Erreur d'authenficiation").ShowAsync();
                        }
                    }
                    else
                    {
                        CurrentUser = UserManager.Instance.Connexion(CurrentUser.Email, CurrentUser.Password);
                        if (CurrentUser != null)
                        {
                            MessengerInstance.Send(CurrentUser);
                            _navigationService.NavigateTo("Commande");
                        }
                        else
                        {
                            CurrentUser = new Utilisateur();
                            new MessageDialog("Erreur d'authenficiation").ShowAsync();
                        }
                    }
                }
                else
                {
                    new MessageDialog("Email invalide").ShowAsync();
                }
            }
            else
            {
                new MessageDialog("Veuillez remplir tous les champs").ShowAsync();
            }
        }

        private void Inscription()
        {
            _navigationService.NavigateTo("Inscription");
        }

    }
}