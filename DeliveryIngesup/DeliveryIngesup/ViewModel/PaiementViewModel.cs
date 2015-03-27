using System.Collections.ObjectModel;
using System.Windows.Input;
using DAL.Models;
using DeliveryIngesup.Manager;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

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
        #endregion

        #region Commands
        public ICommand PaiementCommand { get; set; }
        #endregion

        public PaiementViewModel()
        {
            MessengerInstance.Register<Utilisateur>(this, user => CurrentUser = user);
            MessengerInstance.Register<ObservableCollection<Produit>>(this, panier => Panier = panier);
            PaiementCommand = new RelayCommand(Paiement);
        }

        private void Paiement()
        {
            //TODO : Paiement avec Paypal

            DeliveryManager.Instance.CreerCommande(CurrentUser, Panier);
        }
    }
}