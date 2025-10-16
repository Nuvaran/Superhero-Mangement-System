using Superhero_Mangement_System.DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Superhero_Mangement_System.BusinessLogicLayer
{
    internal class CalculationsAndConversions
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
        public static DataTable ConvertToDataTable(List<string[]> heroes)
        {
            DataTable dt = new DataTable();

            // Define columns matching your file structure
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Age", typeof(int));
            dt.Columns.Add("Superpower", typeof(string));
            dt.Columns.Add("Exam Score", typeof(int));
            dt.Columns.Add("Rank", typeof(string));
            dt.Columns.Add("Threat Level", typeof(string));

            foreach (string[] hero in heroes)
            {
                if (hero.Length >= 7) // Ensure we have all expected fields
                {
                    DataRow row = dt.NewRow();
                    row["ID"] = int.Parse(hero[0]);
                    row["Name"] = hero[1];
                    row["Age"] = int.Parse(hero[2]);
                    row["Superpower"] = hero[3];
                    row["Exam Score"] = int.Parse(hero[4]);
                    row["Rank"] = hero[5];
                    row["Threat Level"] = hero[6];
                    dt.Rows.Add(row);
                }
            }

            return dt;
        }

        public static bool DeleteHeroById(int heroId)
        {
            try
            {
                return FileHandler.DeleteHero(heroId);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static int GetSelectedHeroId(DataGridView dataGridView)
        {
            if (dataGridView.CurrentRow == null || dataGridView.CurrentRow.IsNewRow)
                return -1; // No selection

            return Convert.ToInt32(dataGridView.CurrentRow.Cells["ID"].Value);
        }

        public static string GetSelectedHeroName(DataGridView dataGridView)
        {
            if (dataGridView.CurrentRow == null || dataGridView.CurrentRow.IsNewRow)
                return string.Empty;

            return dataGridView.CurrentRow.Cells["Name"].Value.ToString();
        }

        public static SummaryReport GenerateSummaryReport()
        {
            List<string[]> heroes = FileHandler.GetAllHeroes();
            var report = new SummaryReport();

            if (heroes.Count == 0)
                return report;

            int totalAge = 0;
            int totalExamScore = 0;

            foreach (string[] hero in heroes)
            {
                if (hero.Length >= 7)
                {
                    report.TotalHeroes++;

                    // Calculate age statistics
                    int age = int.Parse(hero[2]);
                    totalAge += age;
                    if (age < report.YoungestAge) report.YoungestAge = age;
                    if (age > report.OldestAge) report.OldestAge = age;

                    // Calculate exam score statistics
                    int examScore = int.Parse(hero[4]);
                    totalExamScore += examScore;
                    if (examScore < report.LowestExamScore) report.LowestExamScore = examScore;
                    if (examScore > report.HighestExamScore) report.HighestExamScore = examScore;

                    // Count heroes per rank
                    string rank = hero[5];
                    if (report.HeroesPerRank.ContainsKey(rank))
                        report.HeroesPerRank[rank]++;
                    else
                        report.HeroesPerRank[rank] = 1;
                }
            }

            // Calculate averages
            report.AverageAge = Math.Round((double)totalAge / report.TotalHeroes, 2);
            report.AverageExamScore = Math.Round((double)totalExamScore / report.TotalHeroes, 2);

            return report;
        }
    }


    // Summary Report Model
    public class SummaryReport
    {
        public int TotalHeroes { get; set; }
        public double AverageAge { get; set; }
        public double AverageExamScore { get; set; }
        public int YoungestAge { get; set; } = int.MaxValue;
        public int OldestAge { get; set; } = int.MinValue;
        public int LowestExamScore { get; set; } = int.MaxValue;
        public int HighestExamScore { get; set; } = int.MinValue;
        public Dictionary<string, int> HeroesPerRank { get; set; } = new Dictionary<string, int>();
    }
}



