using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicycleWindCommute
{
    public static class CommuteDifficultyCalculator
    {
        public static int GetCommuteQualityRating()
        {
            double pathDirection = GoogleMapsAPI.GetPathDirection();
            WindForecastInstance windForecastInstance = WindForecastAPI.GetWindForecast(DateTime.Now.AddHours(46));

            // Wind direction is the direction wind comes from, therefore before doing vector calculations it needs to be rotated 180 degrees.
            double windAngleDifference = TrigonometryUtility.GetDegreeBetweenVectors(pathDirection, TrigonometryUtility.RotateAngle180(windForecastInstance.WindDirectionDegrees));
            double windHelpMagnitude = TrigonometryUtility.DotProductWithUnitVector(windForecastInstance.windStrengthMps, windAngleDifference);

            return CalculateHelpIndex(windHelpMagnitude);
        }

        public static int CalculateHelpIndex(double windHelpMagnitude)
        {
            return (int)Math.Round(Sigmoid(windHelpMagnitude / 5) * 10);
        }

        public static double Sigmoid(double value)
        {
            return 1.0f / (1.0f + Math.Exp(-value));
        }

    }
}
