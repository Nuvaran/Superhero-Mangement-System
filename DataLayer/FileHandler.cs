using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Superhero_Mangement_System.DataLayer
{
    /**  
     * FileHandler class manages reading from and writing to text files
     * for storing superhero data and summary reports.
    */
    public class FileHandler
    {
        private readonly string heroFile;
        private readonly string reportFile;

        /** 
         * Constructor initializes file paths for superhero data and summary report.
        */
        public FileHandler()
        {
            // Path to the DataLayer folder
            string dataLayerPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\DataLayer");

            Directory.CreateDirectory(dataLayerPath);

            // Define file paths
            heroFile = Path.Combine(dataLayerPath, "superheroes.txt");
            reportFile = Path.Combine(dataLayerPath, "summary.txt");
        }

        /** 
         * Reads all superhero records from the text file.
         * returns List of superhero records as strings.
        */
        public List<string> ReadAllHeroes()
        {
            if (!File.Exists(heroFile))
                return new List<string>();

            return new List<string>(File.ReadAllLines(heroFile));
        }

        /** 
         * Appends a new superhero record to the text file.
         * heroRecord: The superhero record to be saved.
        */
        public void SaveHero(string heroRecord)
        {
            using (StreamWriter sw = new StreamWriter(heroFile, true))
            {
                sw.WriteLine(heroRecord);
            }
        }

        /** 
         * Overwrites all superhero records in the text file with the provided list.
         * heroes: List of superhero records to be saved.
        */
        public void OverwriteAllHeroes(List<string> heroes)
        {
            File.WriteAllLines(heroFile, heroes);
        }

        /** 
         * Saves the summary report to the text file.
         * summary: The summary report content to be saved.
        */
        public void SaveReport(string summary)
        {
            File.WriteAllText(reportFile, summary);
        }
    }
}
