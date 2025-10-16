using Superhero_Mangement_System.BusinessLogicLayer;
using Superhero_Mangement_System.DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Superhero_Mangement_System.PresentationLayer
{
    internal class Display
    {
        public static void DisplayAllHeroes(DataGridView dataGridView)
        {
            try
            {
                // Get data from FileHandler
                List<string[]> heroes = FileHandler.GetAllHeroes();

                // Process through business logic
                DataTable heroTable = CalculationsAndConversions.ConvertToDataTable(heroes);

                // Display in DataGridView
                dataGridView.DataSource = heroTable;
                dataGridView.AutoResizeColumns();
                dataGridView.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading heroes: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public static void DisplaySummaryReport(TextBox textBoxDisplay = null)
        {
            try
            {
                // Generate report through business logic
                SummaryReport report = CalculationsAndConversions.GenerateSummaryReport();

                // Save to file
                FileHandler.SaveSummaryToFile(report);

                // Display in UI if textbox provided
                if (textBoxDisplay != null)
                {
                    textBoxDisplay.Text = FormatSummaryForDisplay(report);
                }

                MessageBox.Show("Summary report generated and saved to summary.txt!",
                              "Report Complete",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating summary: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private static string FormatSummaryForDisplay(SummaryReport report)
        {
            var sb = new StringBuilder();
            sb.AppendLine("=== SUMMARY REPORT ===");
            sb.AppendLine($"Total Heroes: {report.TotalHeroes}");
            sb.AppendLine($"Average Age: {report.AverageAge}");
            sb.AppendLine($"Average Exam Score: {report.AverageExamScore}");
            sb.AppendLine($"Age Range: {report.YoungestAge} - {report.OldestAge} years");
            sb.AppendLine($"Exam Score Range: {report.LowestExamScore} - {report.HighestExamScore}");
            sb.AppendLine();
            sb.AppendLine("=== HEROES PER RANK ===");

            foreach (var rank in report.HeroesPerRank)
            {
                sb.AppendLine($"{rank.Key}: {rank.Value} hero(es)");
            }

            return sb.ToString();
        }


    }
}
