using System.Windows.Input;
using System.Xml.Linq;
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
        //private string _login;

        //public string Login
        //{
        //    get { return _login; }
        //    set
        //    {
        //        _login = value;
        //        RaisePropertyChanged();
        //    }
        //}

        //private string _password;

        //public string Password
        //{
        //    get { return _password; }
        //    set
        //    {
        //        _password = value;
        //        RaisePropertyChanged();
        //    }
        //}

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
            CurrentUser = UserManager.Instance.Connexion(CurrentUser.Email, CurrentUser.Password);
            if (CurrentUser != null)
            {
                //TODO : Navigation vers commande
                int x = 0;
                //_navigationService.NavigateTo("Commande");
            }
            else
            {
                CurrentUser = new Utilisateur();
                //TODO : Message d'erreur
            }

        }
        
        private void Inscription()
        {
            _navigationService.NavigateTo("Inscription");
        }

    }
}