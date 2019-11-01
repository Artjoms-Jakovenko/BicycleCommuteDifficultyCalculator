using BicycleWindCommute.GoogleMapsSerialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicycleWindCommute
{
    public class GoogleMapsAPI
    {
        Leg pathLeg { set; get; }

        public GoogleMapsAPI(string origin, string destination)
        {
            pathLeg = GetFirstLeg(origin, destination);
        }

        private Leg GetFirstLeg(string origin, string destination)
        {
            StringBuilder urlRequest = new StringBuilder();
            urlRequest.Append("https://maps.googleapis.com/maps/api/directions/json?");
            urlRequest.Append("origin=" + origin.Trim().Replace(" ", "+"));
            urlRequest.Append("&destination=" + destination.Trim().Replace(" ", "+"));
            urlRequest.Append("&mode=bicycling&key=AIzaSyByjhJ5jaYEGtO3SrusXXyIkPk0fnuIyis");

            string jsonResponse = HttpRequestsUtility.Get(urlRequest.ToString());
            MapsJsonRoot path = JsonConvert.DeserializeObject<MapsJsonRoot>(jsonResponse);

            if(path.Status != "OK")
            {
                return null;
            }
            return path.Routes[0].Legs[0];
        }

        public double GetPathDirection()
        {
            return TrigonometryUtility.GetDegreeBearing(pathLeg.StartLocation.Latitude, pathLeg.StartLocation.Longitude, pathLeg.EndLocation.Latitude, pathLeg.EndLocation.Longitude); ;
        }

        public double GetStepDirection(Step step)
        {
            return TrigonometryUtility.GetDegreeBearing(step.StartLocation.Latitude, step.StartLocation.Longitude, step.EndLocation.Latitude, step.EndLocation.Longitude); ;
        }

        public int GetPathDistance()
        {
            return pathLeg.DistanceMeters.distance;
        }

        public double GetMedianLatitude()
        {
            return (pathLeg.StartLocation.Latitude + pathLeg.EndLocation.Latitude) / 2;
        }

        public double GetMedianLongitude()
        {
            return (pathLeg.StartLocation.Longitude + pathLeg.EndLocation.Longitude) / 2;
        }

        public List<Step> GetSteps()
        {
            List<Step> steps = new List<Step>();

            foreach (var pathStep in pathLeg.Steps)
            {
                Step step = new Step();
                step.StartLocation = new Location();
                step.EndLocation = new Location();

                step.distance = pathStep.DistanceMeters.distance;
                step.StartLocation.Latitude = pathStep.StartLocation.Latitude;
                step.StartLocation.Longitude = pathStep.StartLocation.Longitude;
                step.EndLocation.Latitude = pathStep.EndLocation.Latitude;
                step.EndLocation.Longitude = pathStep.EndLocation.Longitude;

                steps.Add(step);
            }

            return steps;
        }
    }

    public class Step
    {
        public Location StartLocation;
        public Location EndLocation;
        public int distance;
    }

    public class Location
    {
        public double Latitude;
        public double Longitude;
    }
}
