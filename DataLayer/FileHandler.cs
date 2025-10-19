using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Superhero_Mangement_System.DataLayer
{
    public class FileHandler
    {
        private readonly string heroFile;
        private readonly string reportFile;

        public FileHandler()
        {
            // Path to the DataLayer folder
            string dataLayerPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\DataLayer");

            Directory.CreateDirectory(dataLayerPath);

            // Define file paths
            heroFile = Path.Combine(dataLayerPath, "superheroes.txt");
            reportFile = Path.Combine(dataLayerPath, "summary.txt");
        }

        public List<string> ReadAllHeroes()
        {
            if (!File.Exists(heroFile))
                return new List<string>();

            return new List<string>(File.ReadAllLines(heroFile));
        }

        public void SaveHero(string heroRecord)
        {
            using (StreamWriter sw = new StreamWriter(heroFile, true))
            {
                sw.WriteLine(heroRecord);
            }
        }

        public void OverwriteAllHeroes(List<string> heroes)
        {
            File.WriteAllLines(heroFile, heroes);
        }

        public void SaveReport(string summary)
        {
            File.WriteAllText(reportFile, summary);
        }
    }
}
