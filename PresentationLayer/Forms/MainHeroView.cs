using Superhero_Mangement_System.DataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Superhero_Mangement_System.PresentationLayer.Forms
{
    public partial class MainHeroView : Form
    {
        string change;
        public MainHeroView()
        {
            InitializeComponent();
        }

        

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void MainHeroView_Load(object sender, EventArgs e)
        {
            FileHandler.CreateTextFile();
            
            DisableAllFields();


        }
        private void RefreshHeroesDisplay()
        {
            Display.DisplayAllHeroes(dgvHeroes);
        }
        private void DisableAllFields()
        {
            RefreshHeroesDisplay();
            //Disable input fields
            txtName.Enabled = false;
            txtSuperpower.Enabled = false;
            numAge.Enabled = false;
            numExamScore.Enabled = false;
            lblChange.Visible = false;
            btnSave.Visible = false;
            btnCancel.Visible = false;
            //Enable buttons
            btnAddHero.Enabled = true;
            btnUpdateHero.Enabled = true;
            btnDeleteHero.Enabled = true;
            btnSummaryReport.Enabled = true;
            dgvHeroes.Enabled = true;

            if (FileHandler.GetAllHeroes().Count == 0)
            {
                btnUpdateHero.Enabled = false;
                btnDeleteHero.Enabled = false;
            }

        }

        private void dgvHeroes_SelectionChanged(object sender, EventArgs e)
        {
            // Check if DataGridView itself is ready
            if (dgvHeroes == null) return;

            // Check if CurrentRow exists and is valid
            if (dgvHeroes.CurrentRow == null) return;

            // Check if the row is not a new row and has valid index
            if (dgvHeroes.CurrentRow.IsNewRow) return;

            // Finally safe to access Index
            if (dgvHeroes.CurrentRow.Index >= 0)
            {
                updateInputs();
            }
        }
           

        private void updateInputs()
        {
            DataGridViewRow selectedRow = dgvHeroes.CurrentRow;


            txtName.Text = selectedRow.Cells["Name"].Value.ToString();
            txtSuperpower.Text = selectedRow.Cells["Superpower"].Value.ToString();
            if (int.TryParse(selectedRow.Cells["Age"].Value.ToString(), out int age))
                numAge.Value = age;
            if (int.TryParse(selectedRow.Cells["Exam Score"].Value.ToString(), out int score))
                numExamScore.Value = score;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void numAge_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DisableAllFields();
        }

        private void btnUpdateHero_Click(object sender, EventArgs e)
        {
            ChangeToRecords("Update");
            GetID getNewID = new GetID();
            DialogResult result= getNewID.ShowDialog();
            if (result == DialogResult.OK) 
            {
                SelectRowByHeroId(getNewID.EnteredID);
                updateInputs();
            }
            else
            {
                DisableAllFields() ;
            }

        }
        private void SelectRowByHeroId(int heroId)
        {
            // Clear any existing selection
            dgvHeroes.ClearSelection();

            // Search through all rows to find the one with matching ID
            foreach (DataGridViewRow row in dgvHeroes.Rows)
            {
                if (row.Cells["ID"].Value != null &&
                    Convert.ToInt32(row.Cells["ID"].Value) == heroId)
                {
                    // Found the row - select it
                    row.Selected = true;
                    dgvHeroes.CurrentCell = row.Cells[0];
                    dgvHeroes.FirstDisplayedScrollingRowIndex = row.Index; // Scroll to it

                    // This will automatically trigger your SelectionChanged event
                    // and populate the textboxes with this hero's data
                    break;
                }
            }
        }

        private void btnAddHero_Click(object sender, EventArgs e)
        {
            
            ChangeToRecords("Add");
        }

        private void ChangeToRecords(string changeType)
        {
            if (FileHandler.GetAllHeroes().Count == 0)
            {
                btnUpdateHero.Enabled = false;
                btnDeleteHero.Enabled = false;
            }
            //Disable all buttons
            dgvHeroes.Enabled = false;
            btnAddHero.Enabled = false;
            btnUpdateHero.Enabled = false;
            btnSummaryReport.Enabled = false;
            btnDeleteHero.Enabled = false;

            change = changeType;

            //Show shared buttons and label
            btnSave.Visible = true;
            btnCancel.Visible = true;
            lblChange.Visible = true;
            //Enable input fields
            txtName.Enabled = true;
            txtSuperpower.Enabled = true;
            numAge.Enabled = true;
            numExamScore.Enabled = true;

            if (changeType == "Add")
            {
                //Default input values
                txtName.Text = "";
                txtSuperpower.Text = "";
                numAge.Value = 17;
                numExamScore.Value = 0;

                //Change shared buttons and label
                btnSave.Text = "Add";
                lblChange.Text = "Add New Hero:";
            }
            else if (changeType == "Update")
            {
                //Change shared buttons and label
                btnSave.Text = "Update";
                lblChange.Text = "Update Hero";
            }
            else if (changeType == "Delete")
            {
                btnSave.Text = "Delete";
                lblChange.Text = "Delete Selected Record?:";
            }
        }
        

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnDeleteHero_Click(object sender, EventArgs e)
        {
            ChangeToRecords("Delete");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (change == "Add")
            {
                string[] heroFields = {txtName.Text.Trim(),numAge.Value.ToString(),txtSuperpower.Text.Trim(),numExamScore.Value.ToString() };
                FileHandler.AddNewHero(heroFields);
            } else if (change == "Update")
            {
                string[] heroFields = { txtName.Text.Trim(), numAge.Value.ToString(), txtSuperpower.Text.Trim(), numExamScore.Value.ToString() };
                int.TryParse(dgvHeroes.CurrentRow.Cells["ID"].Value.ToString(), out int ID);
                FileHandler.UpdateHero(ID, heroFields);
            } else if (change == "Delete")
            {
                int.TryParse(dgvHeroes.CurrentRow.Cells["ID"].Value.ToString(), out int ID);
                FileHandler.DeleteHero(ID);
            }
            DisableAllFields();
        }

        private void dgvHeroes_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgvHeroes.ClearSelection();
        }
    }
}
