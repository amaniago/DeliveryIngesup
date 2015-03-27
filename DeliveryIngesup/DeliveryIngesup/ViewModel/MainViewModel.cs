using System.Windows.Input;
using System.Xml.Linq;
using DAL.Manager;
using DAL.Models;
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
            //TODO : Check si tous les champs sont remplis
            CurrentUser = UserManager.Instance.Connexion(CurrentUser.Email, CurrentUser.Password);
            if (CurrentUser != null)
            {
                MessengerInstance.Send(CurrentUser);
                _navigationService.NavigateTo("Commande");
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