using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BicycleWindCommute
{
    class Program
    {
        static void Main(string[] args)
        {
            int rating = CommuteDifficultyCalculator.GetCommuteQualityRating("Richard Mortensens vej 54", "Frederiksberg", DateTime.Now.AddHours(74));
            string ratingDescription = CommuteQualityPresentation.GetCommuteRatingDescription(rating);
        }
    }
}
