using SiticoneNetFrameworkUI;
using Superhero_Mangement_System.BusinessLogicLayer;
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

namespace Superhero_Mangement_System.PresentationLayer.Pages
{
    public partial class AddHeroForm : Form
    {
        private Color accentGold = Color.FromArgb(255, 215, 0);
        private Color accentBlue = Color.FromArgb(0, 191, 255);
        private Color accentGreen = Color.FromArgb(50, 205, 50);
        private Color accentGray = Color.FromArgb(169, 169, 169);
        private Color darkBg = Color.FromArgb(26, 26, 46);
        private Color darkSecondary = Color.FromArgb(18, 18, 43);

        private DataGridView heroDataGridView;

        public AddHeroForm()
        {
            InitializeComponent();
        }

        private void AddHeroForm_Load(object sender, EventArgs e)
        {
            SetupForm();
            InitializeInputPanel();
            InitializeDataGridView();
        }

        private void SetupForm()
        {
            this.Text = "Add New Hero";
            this.Size = new Size(1000, 700);
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = darkBg;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.DoubleBuffered = true;
            this.MinimumSize = new Size(800, 600);
        }

        private void InitializeInputPanel()
        {
            // Title Label
            SiticoneLabel titleLabel = new SiticoneLabel
            {
                Text = "⚡ Add New Hero ⚡",
                Location = new Point(30, 20),
                Font = new Font("Orbitron", 18, FontStyle.Bold),
                ForeColor = accentGold,
                AutoSize = true
            };
            this.Controls.Add(titleLabel);

            // Main Input Panel
            SiticonePanel inputPanel = new SiticonePanel
            {
                Size = new Size(940, 180),
                Location = new Point(30, 70),
                FillColor = darkSecondary,
                BorderThickness = 1,
                Parent = this
            };

            int xPos = 20;
            int yPos = 20;

            // Hero ID Input
            CreateInputField(inputPanel, "Hero ID:", "HeroIDTextBox", xPos, yPos);
            xPos += 180;

            // Hero Name Input
            CreateInputField(inputPanel, "Name:", "NameTextBox", xPos, yPos);
            xPos += 180;

            // Hero Age Input
            CreateInputField(inputPanel, "Age:", "AgeTextBox", xPos, yPos);
            xPos += 180;

            // Hero Superpower Input
            CreateInputField(inputPanel, "Superpower:", "SuperpowerTextBox", xPos, yPos);

            // Reset for second row
            xPos = 20;
            yPos = 95;

            // Hero Exam Score Input
            CreateInputField(inputPanel, "Exam Score (0-100):", "ExamScoreTextBox", xPos, yPos);

            // Save Button
            SiticoneButton saveBtn = new SiticoneButton
            {
                Text = "➕ Add Hero",
                Size = new Size(150, 50),
                Location = new Point(200, 95),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };
            saveBtn.BackColor = accentGold;
            saveBtn.ForeColor = Color.Black;
            saveBtn.MouseEnter += (s, e) => saveBtn.BackColor = Color.FromArgb(255, 230, 0);
            saveBtn.MouseLeave += (s, e) => saveBtn.BackColor = accentGold;
            saveBtn.Click += (s, e) => OnSaveHeroClicked();
            inputPanel.Controls.Add(saveBtn);

            // Clear Button
            SiticoneButton clearBtn = new SiticoneButton
            {
                Text = "❌ Clear",
                Size = new Size(130, 50),
                Location = new Point(370, 95),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };
            clearBtn.BackColor = accentGray;
            clearBtn.ForeColor = Color.Black;
            clearBtn.MouseEnter += (s, e) => clearBtn.BackColor = Color.FromArgb(189, 189, 189);
            clearBtn.MouseLeave += (s, e) => clearBtn.BackColor = accentGray;
            clearBtn.Click += (s, e) => ClearInputFieldsDashboard(inputPanel);
            inputPanel.Controls.Add(clearBtn);

            this.Controls.Add(inputPanel);
        }

        private void CreateInputField(SiticonePanel parent, string labelText, string textBoxName, int x, int y)
        {
            // Label
            SiticoneLabel label = new SiticoneLabel
            {
                Text = labelText,
                Location = new Point(x, y),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = accentBlue,
                AutoSize = true
            };
            parent.Controls.Add(label);

            // TextBox
            TextBox textBox = new TextBox
            {
                Name = textBoxName,
                Size = new Size(140, 30),
                Location = new Point(x, y + 22),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = darkBg
            };
            parent.Controls.Add(textBox);
        }

        private void InitializeDataGridView()
        {
            // DataGridView Title
            SiticoneLabel gridTitleLabel = new SiticoneLabel
            {
                Text = "📋 Heroes Database",
                Location = new Point(30, 230),
                Font = new Font("Orbitron", 14, FontStyle.Bold),
                ForeColor = accentGold,
                AutoSize = true
            };
            this.Controls.Add(gridTitleLabel);

            // DataGridView Panel
            SiticonePanel gridPanel = new SiticonePanel
            {
                Size = new Size(940, 230),
                Location = new Point(30, 265),
                FillColor = darkSecondary,
                BorderThickness = 1
            };

            // DataGridView
            heroDataGridView = new DataGridView
            {
                Size = new Size(920, 207),
                Location = new Point(10, 10),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                ColumnHeadersHeight = 35,
                RowTemplate = { Height = 28 }
            };

            heroDataGridView.BackgroundColor = darkBg;
            heroDataGridView.ForeColor = Color.White;
            heroDataGridView.Font = new Font("Segoe UI", 10);
            heroDataGridView.GridColor = Color.FromArgb(50, 50, 80);
            heroDataGridView.BorderStyle = BorderStyle.None;

            // Setup columns
            heroDataGridView.Columns.Add("HeroID", "Hero ID");
            heroDataGridView.Columns.Add("Name", "Name");
            heroDataGridView.Columns.Add("Age", "Age");
            heroDataGridView.Columns.Add("Superpower", "Superpower");
            heroDataGridView.Columns.Add("ExamScore", "Exam Score");
            heroDataGridView.Columns.Add("Rank", "Rank");
            heroDataGridView.Columns.Add("ThreatLevel", "Threat Level");

            // Set column widths
            heroDataGridView.Columns["HeroID"].Width = 70;
            heroDataGridView.Columns["Name"].Width = 100;
            heroDataGridView.Columns["Age"].Width = 60;
            heroDataGridView.Columns["Superpower"].Width = 130;
            heroDataGridView.Columns["ExamScore"].Width = 90;
            heroDataGridView.Columns["Rank"].Width = 70;
            heroDataGridView.Columns["ThreatLevel"].Width = 80;

            // Style headers
            heroDataGridView.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = darkSecondary,
                ForeColor = accentBlue,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };

            // Style cells
            heroDataGridView.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = darkBg,
                ForeColor = Color.White,
                SelectionBackColor = Color.FromArgb(50, 50, 100),
                SelectionForeColor = accentGold,
                Font = new Font("Segoe UI", 10)
            };

            gridPanel.Controls.Add(heroDataGridView);
            this.Controls.Add(gridPanel);
        }

        private void ClearInputFieldsDashboard(SiticonePanel parent)
        {
            try
            {
                // Find all textboxes in the entire form and clear them
                foreach (Control ctrl in this.Controls)
                {
                    if (ctrl is SiticonePanel panel)
                    {
                        foreach (Control child in panel.Controls)
                        {
                            if (child is TextBox textBox)
                            {
                                textBox.Text = "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error clearing fields: {ex.Message}");
            }
        }

        private bool HeroNameExists(string heroName)
        {
            FileHandler fileHandler = new FileHandler();
            List<string> heroLines = fileHandler.ReadAllHeroes();

            foreach (string line in heroLines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                string[] parts = line.Split('|');
                if (parts.Length >= 2)
                {
                    string existingName = parts[1].Trim();
                    if (existingName.Equals(heroName, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void OnSaveHeroClicked()
        {
            // Get references to input textboxes from the formPanel
            TextBox txtHeroID = null;
            TextBox txtName = null;
            TextBox txtAge = null;
            TextBox txtSuperpower = null;
            TextBox txtExamScore = null;

            // Find textboxes in the form panel
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is SiticonePanel panel)
                {
                    foreach (Control child in panel.Controls)
                    {
                        if (child is TextBox tb)
                        {
                            if (tb.Name == "HeroIDTextBox") txtHeroID = tb;
                            else if (tb.Name == "NameTextBox") txtName = tb;
                            else if (tb.Name == "AgeTextBox") txtAge = tb;
                            else if (tb.Name == "SuperpowerTextBox") txtSuperpower = tb;
                            else if (tb.Name == "ExamScoreTextBox") txtExamScore = tb;
                        }
                    }
                }
            }

            // Validate inputs
            Validations validator = new Validations();
            if (!validator.ValidateHeroInputs(txtHeroID, txtName, txtAge, txtSuperpower, txtExamScore))
                return;

            // Check for duplicate hero name
            if (HeroNameExists(txtName.Text))
            {
                MessageBox.Show($"A hero with the name '{txtName.Text}' already exists! Please use a different name.", "Duplicate Hero", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Calculate rank and threat level
            int score = int.Parse(txtExamScore.Text);
            Calculations calculator = new Calculations();
            var (rank, threatLevel) = calculator.DetermineRankAndThreat(score);

            // Create hero record string
            string heroRecord = $"{txtHeroID.Text}|{txtName.Text}|{txtAge.Text}|{txtSuperpower.Text}|{score}|{rank}-Rank|{threatLevel}";

            // Save to file
            try
            {
                FileHandler fileHandler = new FileHandler();
                fileHandler.SaveHero(heroRecord);

                MessageBox.Show($"Hero '{txtName.Text}' has been successfully added!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear fields
                ClearInputFieldsDashboard(null);

                // Refresh grid
                RefreshHeroesGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving hero: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshHeroesGrid()
        {
            if (heroDataGridView == null) return;

            // Clear existing rows
            heroDataGridView.Rows.Clear();

            // Read heroes from file
            FileHandler fileHandler = new FileHandler();
            List<string> heroLines = fileHandler.ReadAllHeroes();

            // Parse and display heroes
            foreach (string line in heroLines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                string[] parts = line.Split('|');
                if (parts.Length >= 7)
                {
                    heroDataGridView.Rows.Add(
                        parts[0].Trim(),
                        parts[1].Trim(),
                        parts[2].Trim(),
                        parts[3].Trim(),
                        parts[4].Trim(),
                        parts[5].Trim(),
                        parts[6].Trim()
                    );

                    // Color code the Rank column
                    int lastRowIndex = heroDataGridView.Rows.Count - 1;
                    string rank = parts[5].Trim();
                    Color rankColor = GetRankColor(rank);
                    heroDataGridView.Rows[lastRowIndex].Cells["Rank"].Style.ForeColor = rankColor;
                    heroDataGridView.Rows[lastRowIndex].Cells["Rank"].Style.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                }
            }
        }

        private Color GetRankColor(string rank)
        {
            switch (rank.ToUpper())
            {
                case "S-RANK":
                    return accentGold;
                case "A-RANK":
                    return accentBlue;
                case "B-RANK":
                    return accentGreen;
                case "C-RANK":
                    return accentGray;
                default:
                    return Color.White;
            }
        }
    }
}
