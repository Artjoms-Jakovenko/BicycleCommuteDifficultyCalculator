using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicycleWindCommute
{
    public static class GoogleMapsAPI
    {
        public static double GetPathDirection()
        {
            string jsonResponse = HttpRequestsUtility.Get("https://maps.googleapis.com/maps/api/directions/json?origin=55.674640,12.569228&destination=55.626792,12.574206&mode=bicycling&key=AIzaSyByjhJ5jaYEGtO3SrusXXyIkPk0fnuIyis");
            MapsJsonRoot path = JsonConvert.DeserializeObject<MapsJsonRoot>(jsonResponse);

            Leg leg = path.Routes[0].Legs[0];

            return TrigonometryUtility.GetDegreeBearing(leg.StartLocation.Latitude, leg.StartLocation.Longitude, leg.EndLocation.Latitude, leg.EndLocation.Longitude);
        }
    }

    #region JsonSerializationClasses
    public class MapsJsonRoot
    {
        [JsonProperty("routes")]
        public List<Route> Routes;
        [JsonProperty("status")]
        public string Status;
    }

    public class Route
    {
        [JsonProperty("legs")]
        public List<Leg> Legs;
    }

    public class Leg
    {
        [JsonProperty("start_location")]
        public Location StartLocation;
        [JsonProperty("end_location")]
        public Location EndLocation;
    }

    public class Location
    {
        [JsonProperty("lat")]
        public double Latitude;
        [JsonProperty("lng")]
        public double Longitude;
    }
    #endregion

}
