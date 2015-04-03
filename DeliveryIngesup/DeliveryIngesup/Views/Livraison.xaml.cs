using System;
using System.Collections.Generic;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Bing.Maps;
using DeliveryIngesup.Manager;
using DeliveryIngesup.ViewModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DeliveryIngesup.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Livraison : Page
    {
        public Livraison()
        {
            InitializeComponent();
            LivraisonMap.ZoomLevel = 12.5;
            LivraisonMap.Center = new Bing.Maps.Location(43.617, 1.450);

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Models.Commande> selectedItems = new List<Models.Commande>();

            foreach (Models.Commande item in ListView.SelectedItems)
            {
                selectedItems.Add(item);
            }

            LivraisonViewModel model = DataContext as LivraisonViewModel;
            if (model != null)
            {
                model.GestionCommandesSelectionnees(selectedItems);
                model = DataContext as LivraisonViewModel;
                LivraisonMap.ShapeLayers.Clear();

                List<PushPinModel> listePushPin = new List<PushPinModel>(MapManager.CalculerTrajet(model.ListePushPin, model.LivreurPosition));

                for (int i = 0; i < listePushPin.Count; i++)
                {
                    if(i == 0)
                        TracerRoute(model.LivreurPosition.Coordinate.Latitude, model.LivreurPosition.Coordinate.Longitude, listePushPin[i].Location.Latitude, listePushPin[i].Location.Longitude);
                    else
                        TracerRoute(listePushPin[i - 1].Location.Latitude, listePushPin[i - 1].Location.Longitude, listePushPin[i].Location.Latitude, listePushPin[i].Location.Longitude);

                }
            }
        }

        private void TracerRoute(double latitude1, double longitude1, double latitude2, double longitude2)
        {
            var map = MapManager.TracerRoute(latitude1.ToString().Replace(",", "."), longitude1.ToString().Replace(",", "."), latitude2.ToString().Replace(",", "."), longitude2.ToString().Replace(",", "."));
            LivraisonMap.ShapeLayers.Add(map);
        }
    }
}
