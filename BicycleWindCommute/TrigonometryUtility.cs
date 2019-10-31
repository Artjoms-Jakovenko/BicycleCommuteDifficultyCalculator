using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicycleWindCommute
{
    public static class TrigonometryUtility
    {
        public static double GetDegreeBearing(double lat1, double lon1, double lat2, double lon2)
        {
            var dLon = ToRadians(lon2 - lon1);
            var dPhi = Math.Log(
                Math.Tan(ToRadians(lat2) / 2 + Math.PI / 4) / Math.Tan(ToRadians(lat1) / 2 + Math.PI / 4));
            if (Math.Abs(dLon) > Math.PI)
                dLon = dLon > 0 ? -(2 * Math.PI - dLon) : (2 * Math.PI + dLon);
            return ToBearing(Math.Atan2(dLon, dPhi));
        }

        public static double GetDegreeBetweenVectors(double vectorAngle1, double vectorAngle2)
        {
            double vectorAngleDifference = Math.Abs(vectorAngle1 - vectorAngle2);

            return vectorAngleDifference;
        }

        public static double DotProductWithUnitVector(double magnitude, double vectorAngle)
        {
            return magnitude * Math.Cos(TrigonometryUtility.ToRadians(vectorAngle));
        }

        public static double ToRadians(double degrees)
        {
            return degrees * (Math.PI / 180);
        }

        public static double RotateAngle180(double degrees)
        {
            degrees += 180;
            if (degrees > 360)
            {
                degrees %= 360;
            }

            return degrees;
        }

        public static double ToDegrees(double radians)
        {
            return radians * 180 / Math.PI;
        }

        public static double ToBearing(double radians)
        {
            return (ToDegrees(radians) + 360) % 360;
        }
    }
}
