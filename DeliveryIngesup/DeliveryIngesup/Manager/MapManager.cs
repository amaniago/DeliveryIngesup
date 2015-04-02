using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
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
    }
}
