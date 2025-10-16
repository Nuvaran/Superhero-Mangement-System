using Superhero_Mangement_System.BusinessLogicLayer;
using Superhero_Mangement_System.DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Superhero_Mangement_System.PresentationLayer
{
    internal class Display
    {
        internal static void DisplayAllHeroes(DataGridView dataGridView)
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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading heroes: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void DisplaySummaryInDataGridView(DataGridView dataGridView)
        {
            try
            {
                // Generate summary
                SummaryReport report = CalculationsAndConversions.GenerateSummaryReport();
                FileHandler.SaveSummaryToFile(report);
                DataTable summaryTable = CalculationsAndConversions.ConvertSummaryToDataTable(report);

                // Create a NEW form with a NEW DataGridView
                Form summaryForm = new Form();
                summaryForm.Text = "Superhero Academy - Summary Report";
                summaryForm.Size = new Size(500, 400);
                summaryForm.StartPosition = FormStartPosition.CenterParent;
                summaryForm.FormBorderStyle = FormBorderStyle.FixedDialog; // Not resizable
                summaryForm.MaximizeBox = false; // Remove maximize button
                summaryForm.MinimizeBox = false; // Remove minimize button

                // Create Close button
                Button btnClose = new Button();
                btnClose.Text = "Close";
                btnClose.Size = new Size(80, 30);
                btnClose.Location = new Point(205, 320); // Centered
                btnClose.Anchor = AnchorStyles.Bottom;
                btnClose.Click += (s, e) => summaryForm.Close();

                // Create DataGridView
                DataGridView summaryDGV = new DataGridView();
                summaryDGV.Location = new Point(10, 10);
                summaryDGV.Size = new Size(464, 300); // Leave space for button
                summaryDGV.DataSource = summaryTable;
                summaryDGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                summaryDGV.ReadOnly = true; // Make it read-only
                summaryDGV.AllowUserToAddRows = false;
                summaryDGV.AllowUserToDeleteRows = false;
                summaryDGV.RowHeadersVisible = false; // Cleaner look

                // Add controls to form
                summaryForm.Controls.Add(summaryDGV);
                summaryForm.Controls.Add(btnClose);

                // Set close button as default (Enter key closes)
                summaryForm.AcceptButton = btnClose;

                summaryForm.ShowDialog();

                MessageBox.Show("Summary report generated and saved to summary.txt!", "Success",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating summary: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
