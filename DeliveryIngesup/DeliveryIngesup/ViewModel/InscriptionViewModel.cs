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
    public class InscriptionViewModel : ViewModelBase
    {
        #region Properties
        private Utilisateur _nouvelUtilisateur;

        public Utilisateur NouvelUtilisateur
        {
            get { return _nouvelUtilisateur; }
            set
            {
                _nouvelUtilisateur = value;
                RaisePropertyChanged();
            }
        }

        private string _checkPassword;

        public string CheckPassword
        {
            get { return _checkPassword; }
            set
            {
                _checkPassword = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Commands
        public ICommand InscriptionCommand { get; set; }
        public ICommand RetourCommand { get; set; }
        #endregion

        private readonly INavigationService _navigationService;

        public InscriptionViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NouvelUtilisateur = new Utilisateur();
            InscriptionCommand = new RelayCommand(Inscription);
            RetourCommand = new RelayCommand(Retour);
        }

        private void Retour()
        {
            _navigationService.NavigateTo("Main");
        }

        private void Inscription()
        {
            if (!String.IsNullOrEmpty(NouvelUtilisateur.Email) && !String.IsNullOrEmpty(NouvelUtilisateur.Password) && !String.IsNullOrEmpty(NouvelUtilisateur.Nom) && !String.IsNullOrEmpty(NouvelUtilisateur.Prenom))
            {
                Regex regex = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");

                if (regex.IsMatch(NouvelUtilisateur.Email))
                {
                    NouvelUtilisateur = UserManager.Instance.Inscription(NouvelUtilisateur, CheckPassword);
                    _navigationService.NavigateTo("Main");
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
    }
}