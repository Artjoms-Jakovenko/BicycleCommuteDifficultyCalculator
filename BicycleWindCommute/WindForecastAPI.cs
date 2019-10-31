using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicycleWindCommute
{
    public static class WindForecastAPI
    {

        public static List<WindForecastInstance> GetWindForecastInstances5Days3HourInterval()
        {
            string jsonResponse = HttpRequestsUtility.Get("http://api.openweathermap.org/data/2.5/forecast?id=2618425&APPID=9fbb2cbde0201181c427ff24405bfdeb&units=metric"); // TODO changeable Location
            JsonRoot forecast = JsonConvert.DeserializeObject<JsonRoot>(jsonResponse);

            if(forecast.httpReponseCode != "200")
            {
                return null;
            }

            List<WindForecastInstance> windForecastInstances = new List<WindForecastInstance>();

            foreach (var forecastInstance in forecast.forecastInstances)
            {
                WindForecastInstance windForecastInstance = new WindForecastInstance();
                windForecastInstance.forecastTime = DateTimeUtility.UnixTimeStampToDateTime(forecastInstance.forecastTime);
                windForecastInstance.windStrengthMps = forecastInstance.wind.windStrengthMps;
                windForecastInstance.WindDirectionDegrees = forecastInstance.wind.windDirectionDegrees;

                windForecastInstances.Add(windForecastInstance);
            }

            return windForecastInstances;
        }

        public static WindForecastInstance GetWindForecast(DateTime forecastTime)
        {
            if(forecastTime < DateTime.Now.AddHours(-1) || forecastTime > DateTime.Now.AddDays(5))
            {
                Console.WriteLine("Cannot get forecast for " + forecastTime.ToString());
                return null;
            }

            var windForecastInstances = GetWindForecastInstances5Days3HourInterval();
            if(windForecastInstances == null)
            {
                Console.WriteLine("GetWindForecastInstances5Days3HourInterval returned null");
                return null;
            }

            WindForecastInstance clothestWindForecastInstance = GetClothestForecastInstance(windForecastInstances, forecastTime);

            return clothestWindForecastInstance;
        }

        private static WindForecastInstance GetClothestForecastInstance(List<WindForecastInstance> windForecastInstances, DateTime forecastTime)
        {
            TimeSpan shortestInstanceTimeDifference = TimeSpan.MaxValue;
            WindForecastInstance clothestWindForecastInstance = null;

            foreach (var windForecastInstance in windForecastInstances)
            {
                TimeSpan instanceTimeDifference = (windForecastInstance.forecastTime - forecastTime).Duration();
                if (instanceTimeDifference < shortestInstanceTimeDifference)
                {
                    shortestInstanceTimeDifference = instanceTimeDifference;
                    clothestWindForecastInstance = windForecastInstance;
                }
            }

            return clothestWindForecastInstance;
        }
    }

    public class WindForecastInstance
    {
        public DateTime forecastTime;
        public double windStrengthMps;
        public double WindDirectionDegrees;
    }

    #region JsonSerializationClasses
    public class JsonRoot
    {
        [JsonProperty("cod")]
        public string httpReponseCode;
        [JsonProperty("list")]
        public List<ForecastInstance> forecastInstances;
    }

    public class ForecastInstance
    {
        [JsonProperty("dt")]
        public string forecastTime;
        [JsonProperty("wind")]
        public Wind wind;
    }

    public class Wind
    {
        [JsonProperty("speed")]
        public double windStrengthMps;
        [JsonProperty("deg")]
        public double windDirectionDegrees;
    }
    #endregion
}
