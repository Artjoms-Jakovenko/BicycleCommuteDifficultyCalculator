using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicycleWindCommute
{
    public static class CommuteDifficultyCalculator
    {
        public static int GetCommuteQualityRating(string startAddress, string destinationAddress, DateTime forecastTime)
        {
            GoogleMapsAPI googleMapsAPI = new GoogleMapsAPI(startAddress, destinationAddress);
            if(googleMapsAPI.GetPathDistance() > 100000) // > 100 km
            {
                Console.WriteLine("Distance between addresses is too long. Results may be inaccurate.");
            }
            double pathDirection = googleMapsAPI.GetPathDirection();
            WindForecastInstance windForecastInstance = WindForecastAPI.GetWindForecast(googleMapsAPI.GetMedianLatitude(), googleMapsAPI.GetMedianLongitude(), forecastTime);

            // Wind direction is the direction wind comes from, therefore before doing vector calculations it needs to be rotated 180 degrees.
            double forecastWindVectorDirection = TrigonometryUtility.RotateAngle180(windForecastInstance.WindDirectionDegrees);

            #region TOREWRITE

            int positiveWorkLength = 0;
            int negativeWorkLength = 0;

            foreach (Step step in googleMapsAPI.GetSteps())
            {
                double windStepAngleDifference = TrigonometryUtility.GetDegreeBetweenVectors(googleMapsAPI.GetStepDirection(step), forecastWindVectorDirection);
                if(Math.Cos(TrigonometryUtility.ToRadians(windStepAngleDifference)) > 0)
                {
                    positiveWorkLength += step.distance;
                }
                else
                {
                    negativeWorkLength += step.distance;
                }
            }

            double positiveWindWorkPercentage = (double)positiveWorkLength / (positiveWorkLength + negativeWorkLength);

            #endregion

            double windAngleDifference = TrigonometryUtility.GetDegreeBetweenVectors(pathDirection, forecastWindVectorDirection);
            double windHelpMagnitude = TrigonometryUtility.DotProductWithUnitVector(windForecastInstance.windStrengthMps, windAngleDifference);

            return CalculateHelpIndex(windHelpMagnitude);
        }

        /*public static double GetPositiveWindWorkPercentage(List<Step> steps, double forecastWindVectorDirection)
        {
            
        }*/

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
