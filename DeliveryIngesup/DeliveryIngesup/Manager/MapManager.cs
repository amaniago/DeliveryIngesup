using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using Windows.Devices.Geolocation;
using Windows.UI;
using Bing.Maps;

namespace DeliveryIngesup.Manager
{
    public class MapManager
    {
        public static Response GetResponse(Uri uri)
        {
            HttpClient client = new HttpClient();
            var response = client.GetAsync(uri).Result;

            using (var stream = response.Content.ReadAsStreamAsync().Result)
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Response));
                return ser.ReadObject(stream) as Response;
            }
        }

        public static MapShapeLayer TracerRoute(string latitude1, string longitude1, string latitude2, string longitude2)
        {
            Uri routeRequest = new Uri(string.Format("http://dev.virtualearth.net/REST/V1/Routes/walking?wp.0={0}&wp.1={1}&rpo=Points&key={2}", latitude1 + "," + longitude1, latitude2 + "," + longitude2, "AhWiWDUDKdVXoUu7DZhJlXlG9aqsWf2NG4PWXALYaWoSH7ezlIVBzdS7QYzdsteK"));
            Response r = GetResponse(routeRequest);

            if (r != null &&
                r.ResourceSets != null &&
                r.ResourceSets.Length > 0 &&
                r.ResourceSets[0].Resources != null &&
                r.ResourceSets[0].Resources.Length > 0)
            {
                Route route = r.ResourceSets[0].Resources[0] as Route;

                double[][] routePath = route.RoutePath.Line.Coordinates;
                MapPolyline trajetPolyline = new MapPolyline();
                trajetPolyline.Locations = new LocationCollection();
                trajetPolyline.Color = Colors.Blue;
                trajetPolyline.Width = 5.0;

                for (int i = 0; i < routePath.Length; i++)
                {
                    if (routePath[i].Length >= 2)
                    {
                        trajetPolyline.Locations.Add(new Bing.Maps.Location(routePath[i][0], routePath[i][1]));
                    }
                }

                MapShapeLayer shapeLayer = new MapShapeLayer();
                shapeLayer.Shapes.Add(trajetPolyline);

                return shapeLayer;
            }
            else
            {
                return null;
            }
        }

        public static IEnumerable<PushPinModel> CalculerTrajet(ObservableCollection<PushPinModel> listePushPin, Geoposition livreurPosition)
        {
            List<PushPinModel> listeInitiale = new List<PushPinModel>(listePushPin);
            List<PushPinModel> listePushPinOrdonnee = new List<PushPinModel>();
            

            PushPinModel commandeLaPlusProche = null;
            var maxcount = listeInitiale.Count;

            for (int i = listePushPin.Count; i > 0; i--)
            {
                double distanceCommandePlusProche = 0;

                for (int j = 0; j < listeInitiale.Count; j++)
                {
                    double distanceCommande = 0;

                    if (i == maxcount)
                    {
                        distanceCommande = CalculerDistance(livreurPosition.Coordinate.Latitude,
                            livreurPosition.Coordinate.Longitude, listeInitiale[j].Location.Latitude,
                            listeInitiale[j].Location.Longitude);
                    }
                    else
                    {
                        if (commandeLaPlusProche != null)
                            distanceCommande = CalculerDistance(commandeLaPlusProche.Location.Latitude,
                                commandeLaPlusProche.Location.Longitude, listeInitiale[j].Location.Latitude,
                                listeInitiale[j].Location.Longitude);
                    }

                    if (distanceCommande < distanceCommandePlusProche ||
                        distanceCommandePlusProche == 0)
                    {
                        distanceCommandePlusProche = distanceCommande;
                        commandeLaPlusProche = listeInitiale[j];
                    }
                }

                listeInitiale.Remove(commandeLaPlusProche);
                listePushPinOrdonnee.Add(commandeLaPlusProche);
            }

            return listePushPinOrdonnee;
        }


        private static double CalculerDistance(double latitudePoint1, double longitudePoint1, double latitudePoint2, double longitudePoint2)
        {
            double distance = 0;

            double dLat = (latitudePoint2 - latitudePoint1) / 180 * Math.PI;
            double dLong = (longitudePoint2 - longitudePoint1) / 180 * Math.PI;

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2)
                        + Math.Cos(latitudePoint1 / 180 * Math.PI) * Math.Cos(latitudePoint2 / 180 * Math.PI)
                        * Math.Sin(dLong / 2) * Math.Sin(dLong / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            //Calculate radius of earth
            // For this you can assume any of the two points.
            double radiusE = 6378135; // Equatorial radius, in metres
            double radiusP = 6356750; // Polar Radius

            //Numerator part of function
            double nr = Math.Pow(radiusE * radiusP * Math.Cos(latitudePoint1 / 180 * Math.PI), 2);
            //Denominator part of the function
            double dr = Math.Pow(radiusE * Math.Cos(latitudePoint1 / 180 * Math.PI), 2)
                            + Math.Pow(radiusP * Math.Sin(latitudePoint1 / 180 * Math.PI), 2);
            double radius = Math.Sqrt(nr / dr);

            //Calculate distance in meters.
            distance = radius * c;
            return distance; // distance in meters
        }


    }
}
