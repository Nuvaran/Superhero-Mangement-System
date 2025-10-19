using SiticoneNetFrameworkUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Superhero_Mangement_System.PresentationLayer.Pages
{
    public partial class DeleteHeroForm : Form
    {
        private Color accentGold = Color.FromArgb(255, 215, 0);
        private Color accentBlue = Color.FromArgb(0, 191, 255);
        private Color accentGreen = Color.FromArgb(50, 205, 50);
        private Color accentRed = Color.FromArgb(220, 53, 69);
        private Color accentGray = Color.FromArgb(169, 169, 169);
        private Color darkBg = Color.FromArgb(26, 26, 46);
        private Color darkSecondary = Color.FromArgb(18, 18, 43);

        private DataGridView heroesGrid;

        public DeleteHeroForm()
        {
            InitializeComponent();
        }

        private void DeleteHeroForm_Load(object sender, EventArgs e)
        {
            SetupForm();
            InitializeHeader();
            InitializeDataGridView();
            LoadSampleData();
        }

        private void SetupForm()
        {
            this.Text = "Delete Hero";
            this.Size = new Size(1000, 700);
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = darkBg;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.DoubleBuffered = true;
            this.MinimumSize = new Size(800, 500);
        }

        private void InitializeHeader()
        {
            // Title Label
            SiticoneLabel titleLabel = new SiticoneLabel
            {
                Text = "⚡ Delete Hero ⚡",
                Location = new Point(30, 20),
                Font = new Font("Orbitron", 18, FontStyle.Bold),
                ForeColor = accentGold,
                AutoSize = true
            };
            this.Controls.Add(titleLabel);

            // Subtitle
            SiticoneLabel subtitleLabel = new SiticoneLabel
            {
                Text = "Select a hero from the list below and click Delete to remove them from the academy",
                Location = new Point(30, 50),
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.LightGray,
                AutoSize = true
            };
            this.Controls.Add(subtitleLabel);

            // Warning Label
            SiticoneLabel warningLabel = new SiticoneLabel
            {
                Text = "⚠️ Warning: This action cannot be undone!",
                Location = new Point(30, 75),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = accentRed,
                AutoSize = true
            };
            this.Controls.Add(warningLabel);
        }

        private void InitializeDataGridView()
        {
            // DataGridView Panel
            SiticonePanel gridPanel = new SiticonePanel
            {
                Size = new Size(940, 380),
                Location = new Point(30, 105),
                FillColor = darkSecondary,
                BorderThickness = 1
            };

            // DataGridView Title
            SiticoneLabel gridTitle = new SiticoneLabel
            {
                Text = "📋 Heroes Registry",
                Location = new Point(15, 15),
                Font = new Font("Orbitron", 12, FontStyle.Bold),
                ForeColor = accentBlue,
                AutoSize = true
            };
            gridPanel.Controls.Add(gridTitle);

            // DataGridView
            heroesGrid = new DataGridView
            {
                Size = new Size(910, 317),
                Location = new Point(15, 45),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                ColumnHeadersHeight = 35,
                RowTemplate = { Height = 30 },
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };

            heroesGrid.BackgroundColor = darkBg;
            heroesGrid.ForeColor = Color.White;
            heroesGrid.Font = new Font("Segoe UI", 10);
            heroesGrid.GridColor = Color.FromArgb(50, 50, 80);
            heroesGrid.BorderStyle = BorderStyle.None;

            // Setup columns
            heroesGrid.Columns.Add("HeroID", "Hero ID");
            heroesGrid.Columns.Add("Name", "Name");
            heroesGrid.Columns.Add("Age", "Age");
            heroesGrid.Columns.Add("Superpower", "Superpower");
            heroesGrid.Columns.Add("ExamScore", "Exam Score");
            heroesGrid.Columns.Add("Rank", "Rank");
            heroesGrid.Columns.Add("ThreatLevel", "Threat Level");

            // Style headers
            heroesGrid.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = darkSecondary,
                ForeColor = accentBlue,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };

            // Style cells
            heroesGrid.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = darkBg,
                ForeColor = Color.White,
                SelectionBackColor = Color.FromArgb(50, 50, 100),
                SelectionForeColor = accentGold,
                Font = new Font("Segoe UI", 10),
                Padding = new Padding(5)
            };

            // Style alternating rows
            DataGridViewCellStyle alternatingStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(32, 32, 60),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10)
            };
            heroesGrid.AlternatingRowsDefaultCellStyle = alternatingStyle;

            gridPanel.Controls.Add(heroesGrid);
            this.Controls.Add(gridPanel);
        }

        private void InitializeButtons()
        {
            // Button Panel
            SiticonePanel buttonPanel = new SiticonePanel
            {
                Size = new Size(940, 70),
                Location = new Point(30, 550),
                FillColor = darkSecondary,
                BorderThickness = 1
            };

            // Selected Hero Info Label
            SiticoneLabel infoLabel = new SiticoneLabel
            {
                Text = "Selected Hero: ",
                Location = new Point(20, 18),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = accentBlue,
                AutoSize = true
            };
            buttonPanel.Controls.Add(infoLabel);
            this.Tag = infoLabel; // Store reference to update later

            // Delete Button
            SiticoneButton deleteBtn = new SiticoneButton
            {
                Text = "🗑️ Delete Hero",
                Size = new Size(150, 45),
                Location = new Point(700, 12),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };
            deleteBtn.BackColor = accentRed;
            deleteBtn.MouseEnter += (s, e) => deleteBtn.BackColor = Color.FromArgb(200, 30, 50);
            deleteBtn.MouseLeave += (s, e) => deleteBtn.BackColor = accentRed;
            deleteBtn.Click += (s, e) => OnDeleteHeroClicked();
            buttonPanel.Controls.Add(deleteBtn);

            // Cancel Button
            SiticoneButton cancelBtn = new SiticoneButton
            {
                Text = "❌ Cancel",
                Size = new Size(150, 45),
                Location = new Point(770, 12),
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };
            cancelBtn.BackColor = accentGray;
            cancelBtn.MouseEnter += (s, e) => cancelBtn.BackColor = Color.FromArgb(189, 189, 189);
            cancelBtn.MouseLeave += (s, e) => cancelBtn.BackColor = accentGray;
            cancelBtn.Click += (s, e) => ClearSelection();
            buttonPanel.Controls.Add(cancelBtn);

            this.Controls.Add(buttonPanel);

            // Update hero info when selection changes
            heroesGrid.SelectionChanged += (s, e) => UpdateSelectedHeroInfo(infoLabel);
        }

        private void UpdateSelectedHeroInfo(SiticoneLabel infoLabel)
        {
            if (heroesGrid.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = heroesGrid.SelectedRows[0];
                string heroName = selectedRow.Cells["Name"].Value?.ToString() ?? "Unknown";
                infoLabel.Text = $"Selected Hero: {heroName}";
                infoLabel.ForeColor = accentGold;
            }
            else
            {
                infoLabel.Text = "Selected Hero: None";
                infoLabel.ForeColor = accentGray;
            }
        }

        private void OnDeleteHeroClicked()
        {
            if (heroesGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a hero to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = heroesGrid.SelectedRows[0];
            string heroName = selectedRow.Cells["Name"].Value?.ToString() ?? "Unknown";
            string heroID = selectedRow.Cells["HeroID"].Value?.ToString() ?? "";

            // Confirmation dialog
            DialogResult result = MessageBox.Show(
                $"Are you sure you want to delete '{heroName}' (ID: {heroID})?\n\nThis action cannot be undone!",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                // Delete the row
                heroesGrid.Rows.Remove(selectedRow);
                MessageBox.Show($"Hero '{heroName}' has been successfully deleted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear selection info
                SiticoneLabel infoLabel = (SiticoneLabel)this.Tag;
                infoLabel.Text = "Selected Hero: None";
                infoLabel.ForeColor = accentGray;
            }
        }

        private void ClearSelection()
        {
            heroesGrid.ClearSelection();
            SiticoneLabel infoLabel = (SiticoneLabel)this.Tag;
            infoLabel.Text = "Selected Hero: None";
            infoLabel.ForeColor = accentGray;
        }

        private void LoadSampleData()
        {
            // Sample data - Replace with actual data from file/database
            heroesGrid.Rows.Add("H001", "Saitama", 25, "One Punch", 98, "S-Rank", "Dragon");
            heroesGrid.Rows.Add("H002", "Genos", 18, "Incinerate", 92, "S-Rank", "Dragon");
            heroesGrid.Rows.Add("H003", "Mumen Rider", 28, "Justice Crash", 78, "A-Rank", "Tiger");
            heroesGrid.Rows.Add("H004", "Tatsumaki", 28, "Telekinesis", 95, "S-Rank", "Dragon");
            heroesGrid.Rows.Add("H005", "Sonic", 25, "Speed", 88, "A-Rank", "Tiger");
            heroesGrid.Rows.Add("H006", "King", 30, "Popularity", 72, "B-Rank", "Wolf");
            heroesGrid.Rows.Add("H007", "Puri Puri", 26, "Beast", 85, "A-Rank", "Tiger");
            heroesGrid.Rows.Add("H008", "Zombieman", 40, "Regeneration", 80, "A-Rank", "Tiger");

            // Color code the Rank column
            for (int i = 0; i < heroesGrid.Rows.Count; i++)
            {
                string rank = heroesGrid.Rows[i].Cells["Rank"].Value.ToString();
                Color rankColor = GetRankColor(rank);
                heroesGrid.Rows[i].Cells["Rank"].Style.ForeColor = rankColor;
                heroesGrid.Rows[i].Cells["Rank"].Style.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            }

            // Initialize buttons after data is loaded
            InitializeButtons();
        }

        private Color GetRankColor(string rank)
        {
            switch (rank)
            {
                case "S-Rank":
                    return accentGold;
                case "A-Rank":
                    return accentBlue;
                case "B-Rank":
                    return accentGreen;
                case "C-Rank":
                    return accentGray;
                default:
                    return Color.White;
            }
        }
    }
}
