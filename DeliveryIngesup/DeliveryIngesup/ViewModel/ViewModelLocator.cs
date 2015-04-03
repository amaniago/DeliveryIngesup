/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:DeliveryIngesup"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using DeliveryIngesup.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;

namespace DeliveryIngesup.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}
            INavigationService navigationService = CreateNavigationService();
            SimpleIoc.Default.Register<INavigationService>(() => navigationService);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<InscriptionViewModel>(true);
            SimpleIoc.Default.Register<CommandeViewModel>(true);
            SimpleIoc.Default.Register<PaiementViewModel>(true);
            SimpleIoc.Default.Register<LivraisonViewModel>(true);
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public InscriptionViewModel Inscription
        {
            get
            {
                return ServiceLocator.Current.GetInstance<InscriptionViewModel>();
            }
        }

        public CommandeViewModel Commande
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CommandeViewModel>();
            }
        }

        public PaiementViewModel Paiement
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PaiementViewModel>();
            }
        }

        public LivraisonViewModel Livraison
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LivraisonViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }

        INavigationService CreateNavigationService()
        {
            var navigationService = new NavigationService();
            navigationService.Configure("Main", typeof(MainPage));
            navigationService.Configure("Inscription", typeof(Inscription));
            navigationService.Configure("Commande", typeof(Commande));
            navigationService.Configure("Paiement", typeof(Paiement));
            navigationService.Configure("Livraison", typeof(Livraison));

            return navigationService;
        }

    }
}