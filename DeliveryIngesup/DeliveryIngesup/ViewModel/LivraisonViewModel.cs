using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using DeliveryIngesup.Manager;
using DeliveryIngesup.Models;
using GalaSoft.MvvmLight;

namespace DeliveryIngesup.ViewModel
{
    public class LivraisonViewModel : ViewModelBase
    {
        #region Properties
        private Livreur _currentLivreur;

        public Livreur CurrentLivreur
        {
            get { return _currentLivreur; }
            set
            {
                _currentLivreur = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<Commande> _listeCommandes;

        public ObservableCollection<Commande> ListeCommandes
        {
            get { return _listeCommandes; }
            set
            {
                _listeCommandes = value;
                RaisePropertyChanged();
            }
        }

        private Geoposition _livreurPosition;

        public Geoposition LivreurPosition
        {
            get { return _livreurPosition; }
            set
            {
                _livreurPosition = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Commande> ListeCommandesSelectionnees;

        private ObservableCollection<PushPinModel> _listePushpin;

        public ObservableCollection<PushPinModel> ListePushPin
        {
            get { return _listePushpin; }
            set
            {
                _listePushpin = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        public LivraisonViewModel()
        {
            CalculPositionUtilisateur();
            MessengerInstance.Register<Livreur>(this, livreur => CurrentLivreur = livreur);
            ListeCommandes = DeliveryManager.Instance.GetCommandesALivrer();
            ListeCommandesSelectionnees = new ObservableCollection<Commande>();
        }


        private async void CalculPositionUtilisateur()
        {
            Geolocator locator = new Geolocator();
            locator.DesiredAccuracy = PositionAccuracy.High;
            LivreurPosition = await locator.GetGeopositionAsync();
        }

        public void GestionCommandesSelectionnees(List<Commande> listeCommandesSelectionnees)
        {
            ListeCommandesSelectionnees = new ObservableCollection<Commande>(listeCommandesSelectionnees);
            ListePushPin = new ObservableCollection<PushPinModel>();

            foreach (var commande in ListeCommandesSelectionnees)
            {
                Uri geocodeRequest = new Uri(string.Format("http://dev.virtualearth.net/REST/v1/Locations?q={0}&key={1}", commande.Adresse + ", " + commande.CodePostal + " " + commande.Ville, "AgdAwbVE7MR8KAjhbtQgvA3ZbW76rxkpg4jD7siqcvCMmwEgU4z6HAVX3mniYQP4"));
                Response response = MapManager.GetResponse(geocodeRequest);

                if (response != null && response.ResourceSets != null && response.ResourceSets.Length > 0 && response.ResourceSets[0].Resources != null && response.ResourceSets[0].Resources.Length > 0)
                {
                    foreach (var location in from Location location in response.ResourceSets[0].Resources select new Bing.Maps.Location(location.Point.Coordinates[0], location.Point.Coordinates[1]))
                    {
                        ListePushPin.Add(new PushPinModel() { Location = location });
                    }
                }
            }

        }
    }
}
