using Superhero_Mangement_System.BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Superhero_Mangement_System.DataLayer
{
    internal static class FileHandler
    {
        private static string GetTextFilePath()
        {
            string HeroTextFilePath = Directory.GetCurrentDirectory();
            HeroTextFilePath = Directory.GetParent(HeroTextFilePath).FullName;
           return  HeroTextFilePath = Directory.GetParent(HeroTextFilePath).FullName + "\\DataLayer\\superheroes.txt";
        }
        internal static void CreateTextFile()
        {
            string path = GetTextFilePath();
            if (!File.Exists(path))
            {
                // Updated header to include Rank and Threat Level
                File.WriteAllText(path, "ID,Name,Age,Superpower,Exam Score,Rank,Threat Level\n");
            }
        }

        internal static void AddNewHero(string[] heroFields)
        {
            //Name, Age, Superpower, ExamScore
            if (heroFields.Length < 4)
            {
                throw new ArgumentException("Insufficient hero fields provided");
            }

            string path = GetTextFilePath();
            if (!File.Exists(path))
            {
                CreateTextFile();
            }

           
            if (int.TryParse(heroFields[3], out int examScore))
            {
                string rank = CalculationsAndConversions.CalculateRank(examScore);
                string threatLevel = CalculationsAndConversions.CalculateThreatLevel(rank);

                // Generate ID based on existing records
                int idCount = GetNextAvailableId();

                // ID,Name,Age,Superpower,ExamScore,Rank,ThreatLevel
                string newLine = $"{idCount},{string.Join(",", heroFields)},{rank},{threatLevel}\n";
                File.AppendAllText(path, newLine);
            }
            else
            {
                throw new ArgumentException("Invalid exam score format");
            }
        }
        internal static List<int> GetAllHeroIds()
        {
            List<int> ids = new List<int>();
            string path = GetTextFilePath();

            if (!File.Exists(path))
                return ids;

            var lines = File.ReadAllLines(path);

            // Skip header row 
            for (int i = 1; i < lines.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(lines[i]))
                {
                    string[] fields = lines[i].Split(',');
                    if (fields.Length > 0 && int.TryParse(fields[0], out int id))
                    {
                        ids.Add(id);
                    }
                }
            }

            return ids;
        }
        internal static int GetNextAvailableId()
        {
            List<int> existingIds = GetAllHeroIds();

            if (existingIds.Count == 0)
                return 1;

            return existingIds.Max() + 1;
        }

        internal static List<string[]> GetAllHeroes()
        {
            List<string[]> heroes = new List<string[]>();
            string path = GetTextFilePath();

            if (!File.Exists(path))
                return heroes;

            var lines = File.ReadAllLines(path);
            // Skip header row
            for (int i = 1; i < lines.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(lines[i]))
                {
                    string[] fields = lines[i].Split(',');
                    heroes.Add(fields);
                }
            }

            return heroes;
        }

        internal static string[] FindHeroById(int heroId)
        {
            string path = GetTextFilePath();
            if (!File.Exists(path))
                return null;

            var lines = File.ReadAllLines(path);
            // Skip header and search for ID
            for (int i = 1; i < lines.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(lines[i]))
                {
                    string[] fields = lines[i].Split(',');
                    if (fields.Length > 0 && int.TryParse(fields[0], out int currentId) && currentId == heroId)
                    {
                        return fields;
                    }
                }
            }

            return null; // Hero not found
        }

        internal static void UpdateHero(int heroId, string[] updatedHeroFields)
        {
            // Name, Age, Superpower, ExamScore
            if (updatedHeroFields.Length < 4)
            {
                throw new ArgumentException("Insufficient hero fields provided");
            }

            string path = GetTextFilePath();
            if (!File.Exists(path))
                throw new FileNotFoundException("Hero data file not found");

            var lines = File.ReadAllLines(path).ToList();
            bool heroFound = false;

           
            if (int.TryParse(updatedHeroFields[3], out int examScore))
            {
                string rank = CalculationsAndConversions.CalculateRank(examScore);
                string threatLevel = CalculationsAndConversions.CalculateThreatLevel(rank);

                for (int i = 1; i < lines.Count; i++) // Start from 1 to skip header
                {
                    if (!string.IsNullOrWhiteSpace(lines[i]))
                    {
                        string[] fields = lines[i].Split(',');
                        if (fields.Length > 0 && int.TryParse(fields[0], out int currentId) && currentId == heroId)
                        {
                            // Update the line with new data (keeping the same ID)
                            lines[i] = $"{heroId},{string.Join(",", updatedHeroFields)},{rank},{threatLevel}";
                            heroFound = true;
                            break;
                        }
                    }
                }

                if (heroFound)
                {
                    File.WriteAllLines(path, lines);
                }
                else
                {
                    throw new ArgumentException($"Hero with ID {heroId} not found");
                }
            }
            else
            {
                throw new ArgumentException("Invalid exam score format");
            }
        }
        internal static bool DeleteHero(int heroId)
        {
            string path = GetTextFilePath();
            if (!File.Exists(path))
                return false;

            var lines = File.ReadAllLines(path).ToList();
            bool heroFound = false;

            // Start from 1 to skip header row
            for (int i = 1; i < lines.Count; i++)
            {
                if (!string.IsNullOrWhiteSpace(lines[i]))
                {
                    string[] fields = lines[i].Split(',');
                    if (fields.Length > 0 && int.TryParse(fields[0], out int currentId) && currentId == heroId)
                    {
                        lines.RemoveAt(i);
                        heroFound = true;
                        break;
                    }
                }
            }

            if (heroFound)
            {
                File.WriteAllLines(path, lines);
                return true;
            }

            return false; // Hero not found
        }

        internal static void SaveSummaryToFile(SummaryReport report)
        {
            string summaryPath = GetSummaryFilePath();

            List<string> summaryLines = new List<string>();
            summaryLines.Add("=== SUPERHERO ACADEMY SUMMARY REPORT ===");
            summaryLines.Add($"Generated on: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            summaryLines.Add("");
            summaryLines.Add($"Total Heroes: {report.TotalHeroes}");
            summaryLines.Add($"Average Age: {report.AverageAge}");
            summaryLines.Add($"Average Exam Score: {report.AverageExamScore}");
            summaryLines.Add($"Youngest Hero: {report.YoungestAge} years");
            summaryLines.Add($"Oldest Hero: {report.OldestAge} years");
            summaryLines.Add($"Lowest Exam Score: {report.LowestExamScore}");
            summaryLines.Add($"Highest Exam Score: {report.HighestExamScore}");
            summaryLines.Add("");
            summaryLines.Add("=== HEROES PER RANK ===");

            foreach (var rank in report.HeroesPerRank)
            {
                summaryLines.Add($"{rank.Key}: {rank.Value} hero(es)");
            }

            File.WriteAllLines(summaryPath, summaryLines);
        }

        private static string GetSummaryFilePath()
        {
            string currentPath = Directory.GetCurrentDirectory();
            string parentPath = Directory.GetParent(currentPath).FullName;
            parentPath = Directory.GetParent(parentPath).FullName;
            return parentPath + "\\DataLayer\\summary.txt";
        }

       
    }
    
}
