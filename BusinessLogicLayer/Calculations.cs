using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Superhero_Mangement_System.BusinessLogicLayer
{
    /**
     * Calculations class provides methods to determine the rank and threat level
    */
    public class Calculations
    {
        public (string Rank, string ThreatLevel) DetermineRankAndThreat(int score)
        {
            if (score >= 81)
                return ("S", "Finals Week (threat to the entire academy)");
            else if (score >= 61)
                return ("A", "Midterm Madness (threat to a department)");
            else if (score >= 41)
                return ("B", "Group Project Gone Wrong (threat to a study group)");
            else
                return ("C", "Pop Quiz (potential threat to an individual student)");
        }
    }
}
