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
            { 0, "Terrible - You will cycle against a hurricane" },
            { 1, "Awful - You will cycle against very strong wind" },
            { 2, "Bad - You will cycle against strong wind" },
            { 3, "Poor - Wind is moderately opposing you" },
            { 4, "Suboptimal - Wind is slightly opposing you" },
            { 5, "Mediocre - Wind does not significantly affect your commute" },
            { 6, "Decent - Wind slightly helps" },
            { 7, "Good - Wind is moderately helping" },
            { 8, "Great - Strong wind helps" },
            { 9, "Excellent - Very strong wind helps" },
            { 10, "Incredible - Hurricane will push you to your destination" },
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
