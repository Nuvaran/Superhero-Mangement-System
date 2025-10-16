using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Superhero_Mangement_System.BusinessLogicLayer
{
    internal class Calculations
    {
    }
    public static class SuperheroRankCalculations
    {
        public static string CalculateRank(int examScore)
        {
            switch (examScore)
            {
                case int n when (n >= 81 && n <= 100):
                    return "S-Rank";
                case int n when (n >= 61 && n <= 80):
                    return "A-Rank";
                case int n when (n >= 41 && n <= 60):
                    return "B-Rank";
                case int n when (n >= 0 && n <= 40):
                    return "C-Rank";
                default:
                    return "Invalid Score";
            }
        }

        public static string CalculateThreatLevel(string rank)
        {
            switch (rank)
            {
                case "S-Rank":
                    return "Finals Week";
                case "A-Rank":
                    return "Midterm Madness";
                case "B-Rank":
                    return "Group Project Gone Wrong";
                case "C-Rank":
                    return "Pop Quiz";
                default:
                    return "Unknown Threat Level";
            }
        }

        public static string CalculateThreatLevelByScore(int examScore)
        {
            string rank = CalculateRank(examScore);
            return CalculateThreatLevel(rank);
        }
    }
}
