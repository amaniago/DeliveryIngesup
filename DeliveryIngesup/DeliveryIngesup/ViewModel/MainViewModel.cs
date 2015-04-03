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
            //TODO : Check si tous les champs sont remplis
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
                    //TODO : Message d'erreur
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
                    //TODO : Message d'erreur
                }
            }
            

        }
        
        private void Inscription()
        {
            _navigationService.NavigateTo("Inscription");
        }

    }
}