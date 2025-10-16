using Superhero_Mangement_System.BusinessLogicLayer;
using Superhero_Mangement_System.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Superhero_Mangement_System.PresentationLayer
{
    internal class Inputs
    {
        public static class SuperheroInput
        {
            public static bool DeleteSelectedHero(DataGridView dataGridView)
            {
                // Check if a record is selected (Presentation concern)
                int selectedId = CalculationsAndConversions.GetSelectedHeroId(dataGridView);
                if (selectedId == -1)
                {
                    MessageBox.Show("Please select a hero to delete.", "No Selection",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                string heroName = CalculationsAndConversions.GetSelectedHeroName(dataGridView);

                // User confirmation (Presentation concern)
                DialogResult result = MessageBox.Show(
                    $"Are you sure you want to delete {heroName} (ID: {selectedId})?",
                    "Confirm Deletion",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Business logic handles the actual deletion
                    bool deleted = CalculationsAndConversions.DeleteHeroById(selectedId);

                    if (deleted)
                    {
                        MessageBox.Show($"Hero {heroName} deleted successfully!", "Success",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Hero not found or could not be deleted.", "Error",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

                return false; // User cancelled
            }
        }
    }
}
