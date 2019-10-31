using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicycleWindCommute
{
    public static class CommuteQualityPresentation
    {
        private static Dictionary<int, string> CommuteRatingDescription = new Dictionary<int, string>()
        {
            { 0, "You will cycle against a hurricane" },
            { 1, "You will cycle against very strong wind" },
            { 2, "You will cycle against strong wind" },
            { 3, "Wind is moderately oppsoing you" },
            { 4, "Wind is slightly opposing you" },
            { 5, "Wind does not significantly affect your commute" },
            { 6, "Wind slightly helps" },
            { 7, "Wind is moderately helping" },
            { 8, "Strong wind helps" },
            { 9, "Very strong wind helps" },
            { 10, "Hurricane will push you to your destination" },
        };

        public static string GetCommuteRatingDescription(int rating)
        {
            if (CommuteRatingDescription.ContainsKey(rating))
            {
                return CommuteRatingDescription[rating];
            }

            Console.WriteLine("Rating out of known bounds: " + rating);
            return null;
        }
    }
}
