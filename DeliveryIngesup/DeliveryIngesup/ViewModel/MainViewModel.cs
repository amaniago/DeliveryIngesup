using System.Collections.ObjectModel;
using System.Windows.Input;
using DeliveryIngesup.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace DeliveryIngesup.ViewModel
{
    
    public class MainViewModel : ViewModelBase
    {
        #region Properties

        private INavigationService _navigation;

        private ObservableCollection<Produit> _listeProduit;
        public ObservableCollection<Produit> ListeProduit
        {
            get { return _listeProduit; }
            set
            {
                _listeProduit = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<Produit> _listeProduitPanier;
        public ObservableCollection<Produit> ListeProduitPanier
        {
            get { return _listeProduitPanier; }
            set
            {
                _listeProduitPanier = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Commands
        public ICommand NavigateToCommande { get; set; }
        #endregion

        public MainViewModel(INavigationService navigation)
        {
            _navigation = navigation;
            NavigateToCommande = new RelayCommand(() =>
            {
                //ListeProduit = new MockDeliveryManager().GetAllProduit();
                _navigation.NavigateTo("Commande");
            });
        }
    }
}