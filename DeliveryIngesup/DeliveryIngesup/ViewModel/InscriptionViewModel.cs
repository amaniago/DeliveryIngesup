using System.Windows.Input;
using DAL.Models;
using DeliveryIngesup.Manager;
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
        #endregion

        private readonly INavigationService _navigationService;

        public InscriptionViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NouvelUtilisateur = new Utilisateur();
            InscriptionCommand = new RelayCommand(Inscription);
        }

        private void Inscription()
        {
            NouvelUtilisateur = UserManager.Instance.Inscription(NouvelUtilisateur, CheckPassword);
            _navigationService.NavigateTo("Main");             
        }
    }
}