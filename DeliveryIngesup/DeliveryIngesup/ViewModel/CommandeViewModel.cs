using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Windows.Input;
using Windows.ApplicationModel.Store;
using DeliveryIngesup.Manager;
using DeliveryIngesup.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

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
        #endregion

        public CommandeViewModel()
        {
            MessengerInstance.Register<Utilisateur>(this, user => CurrentUser = user);
            DeliveryManager.Instance.Initialisation();
            ListeProduits = DeliveryManager.Instance.GetProduits();
            AjouterProduitCommand = new RelayCommand<int>(param => AjouterArticle(DeliveryManager.Instance.GetProduit(param)));
            Panier = new ObservableCollection<Produit>();
            ValiderPanierCommand = new RelayCommand(ValiderPanier);
        }

        private void AjouterArticle(Produit produit)
        {
            Produit produitExistant = Panier.FirstOrDefault(p => p.IdProduit == produit.IdProduit);

            if (produitExistant == null)
            {
                produit.Quantite ++;
                Panier.Add(produit);
            }
            else
            {
                produitExistant.Quantite++;
                //TODO : Notifier la collection
            }
        }

        private void ValiderPanier()
        {
            
        }
    }
}