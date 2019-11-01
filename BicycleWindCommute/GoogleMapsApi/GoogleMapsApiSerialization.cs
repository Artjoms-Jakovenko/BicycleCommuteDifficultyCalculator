using Newtonsoft.Json;
using System.Collections.Generic;

namespace BicycleWindCommute.GoogleMapsSerialization
{
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
        [JsonProperty("distance")]
        public Distance DistanceMeters;
        [JsonProperty("start_location")]
        public Location StartLocation;
        [JsonProperty("end_location")]
        public Location EndLocation;
        [JsonProperty("steps")]
        public List<Step> Steps;
    }

    public class Location
    {
        [JsonProperty("lat")]
        public double Latitude;
        [JsonProperty("lng")]
        public double Longitude;
    }

    public class Distance
    {
        [JsonProperty("value")]
        public int distance;
    }

    public class Step
    {
        [JsonProperty("distance")]
        public Distance DistanceMeters;
        [JsonProperty("start_location")]
        public Location StartLocation;
        [JsonProperty("end_location")]
        public Location EndLocation;
    }
    #endregion
}
