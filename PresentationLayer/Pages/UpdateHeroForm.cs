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
    public partial class UpdateHeroForm : Form
    {
        private Color accentGold = Color.FromArgb(255, 215, 0);
        private Color accentBlue = Color.FromArgb(0, 191, 255);
        private Color accentGreen = Color.FromArgb(50, 205, 50);
        private Color accentGray = Color.FromArgb(169, 169, 169);
        private Color darkBg = Color.FromArgb(26, 26, 46);
        private Color darkSecondary = Color.FromArgb(18, 18, 43);

        private TextBox txtHeroID, txtName, txtAge, txtSuperpower, txtExamScore;
        private DataGridView heroesGrid;
        private FileHandler fileHandler;
        private Calculations calculations;
        private Validations validations;
        private List<Dictionary<string, string>> heroesData;

        public UpdateHeroForm()
        {
            InitializeComponent();
            fileHandler = new FileHandler();
            calculations = new Calculations();
            validations = new Validations();
            heroesData = new List<Dictionary<string, string>>();
        }

        /**Event handler for form load event.
        *  Initializes the form components and loads hero data.
        */
        private void UpdateHeroForm_Load(object sender, EventArgs e)
        {
            SetupForm();
            InitializeHeader();
            InitializeHeroList();
            InitializeEditPanel();
            LoadHeroesFromFile();
        }

        /**
         * Configures general form properties including size, colors, and layout behavior.
         */
        private void SetupForm()
        {
            this.Text = "Update Hero Information";
            this.Size = new Size(1000, 600);
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = darkBg;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.DoubleBuffered = true;
            this.MinimumSize = new Size(800, 500);
        }

        /**
         * Initializes the header section of the form with title and subtitle labels.
         */
        private void InitializeHeader()
        {
            SiticoneLabel titleLabel = new SiticoneLabel
            {
                Text = "⚡ Update Hero Information ⚡",
                Location = new Point(30, 20),
                Font = new Font("Orbitron", 18, FontStyle.Bold),
                ForeColor = accentGold,
                AutoSize = true
            };
            this.Controls.Add(titleLabel);

            SiticoneLabel subtitleLabel = new SiticoneLabel
            {
                Text = "Select a hero from the list on the left to edit their information",
                Location = new Point(30, 50),
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.LightGray,
                AutoSize = true
            };
            this.Controls.Add(subtitleLabel);
        }

        /**
         * Initializes the hero list panel with a DataGridView to display existing heroes from the superheroes.txt file.
         */
        private void InitializeHeroList()
        {
            SiticonePanel listPanel = new SiticonePanel
            {
                Size = new Size(500, 420),
                Location = new Point(30, 80),
                FillColor = darkSecondary,
                BorderThickness = 1
            };

            SiticoneLabel listTitle = new SiticoneLabel
            {
                Text = "📋 Heroes List",
                Location = new Point(15, 15),
                Font = new Font("Orbitron", 12, FontStyle.Bold),
                ForeColor = accentBlue,
                AutoSize = true
            };
            listPanel.Controls.Add(listTitle);

            heroesGrid = new DataGridView
            {
                Size = new Size(470, 357),
                Location = new Point(15, 45),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                ColumnHeadersHeight = 30,
                RowTemplate = { Height = 28 },
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            heroesGrid.BackgroundColor = darkBg;
            heroesGrid.ForeColor = Color.White;
            heroesGrid.Font = new Font("Segoe UI", 9);
            heroesGrid.GridColor = Color.FromArgb(50, 50, 80);
            heroesGrid.BorderStyle = BorderStyle.None;

            heroesGrid.Columns.Add("HeroID", "Hero ID");
            heroesGrid.Columns.Add("Name", "Name");
            heroesGrid.Columns.Add("Age", "Age");
            heroesGrid.Columns.Add("Superpower", "Superpower");
            heroesGrid.Columns.Add("ExamScore", "Exam Score");
            heroesGrid.Columns.Add("Rank", "Rank");
            heroesGrid.Columns.Add("ThreatLevel", "Threat Level");

            heroesGrid.Columns["HeroID"].Width = 60;
            heroesGrid.Columns["Name"].Width = 70;
            heroesGrid.Columns["Age"].Width = 45;
            heroesGrid.Columns["Superpower"].Width = 90;
            heroesGrid.Columns["ExamScore"].Width = 70;
            heroesGrid.Columns["Rank"].Width = 55;
            heroesGrid.Columns["ThreatLevel"].Width = 60;

            heroesGrid.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = darkSecondary,
                ForeColor = accentBlue,
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };

            heroesGrid.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = darkBg,
                ForeColor = Color.White,
                SelectionBackColor = Color.FromArgb(50, 50, 100),
                SelectionForeColor = accentGold,
                Font = new Font("Segoe UI", 8)
            };

            heroesGrid.SelectionChanged += (s, e) => OnHeroSelected();

            listPanel.Controls.Add(heroesGrid);
            this.Controls.Add(listPanel);
        }

        /**
         * Initializes the edit panel with input fields and buttons for updating hero details.
         */
        private void InitializeEditPanel()
        {
            SiticonePanel editPanel = new SiticonePanel
            {
                Size = new Size(435, 420),
                Location = new Point(540, 80),
                FillColor = darkSecondary,
                BorderThickness = 1
            };

            SiticoneLabel editTitle = new SiticoneLabel
            {
                Text = "✏️ Edit Hero Details",
                Location = new Point(20, 15),
                Font = new Font("Orbitron", 14, FontStyle.Bold),
                ForeColor = accentGold,
                AutoSize = true
            };
            editPanel.Controls.Add(editTitle);

            int yPos = 50;

            CreateEditField(editPanel, "Hero ID:", out txtHeroID, 20, yPos, true);
            yPos += 60;

            CreateEditField(editPanel, "Name:", out txtName, 20, yPos, false);
            yPos += 60;

            CreateEditField(editPanel, "Age:", out txtAge, 20, yPos, false);
            yPos += 60;

            CreateEditField(editPanel, "Superpower:", out txtSuperpower, 20, yPos, false);
            yPos += 60;

            CreateEditField(editPanel, "Exam Score:", out txtExamScore, 20, yPos, false);
            yPos += 70;

            SiticoneButton updateBtn = new SiticoneButton
            {
                Text = "Update Hero",
                Size = new Size(140, 45),
                Location = new Point(20, yPos),
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };
            updateBtn.BackColor = accentGold;
            updateBtn.MouseEnter += (s, e) => updateBtn.BackColor = Color.FromArgb(255, 230, 0);
            updateBtn.MouseLeave += (s, e) => updateBtn.BackColor = accentGold;
            updateBtn.Click += (s, e) => OnUpdateHeroClicked();
            editPanel.Controls.Add(updateBtn);

            SiticoneButton cancelBtn = new SiticoneButton
            {
                Text = "❌ Clear",
                Size = new Size(140, 45),
                Location = new Point(170, yPos),
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };
            cancelBtn.BackColor = accentGray;
            cancelBtn.MouseEnter += (s, e) => cancelBtn.BackColor = Color.FromArgb(189, 189, 189);
            cancelBtn.MouseLeave += (s, e) => cancelBtn.BackColor = accentGray;
            cancelBtn.Click += (s, e) => ClearEditFields();
            editPanel.Controls.Add(cancelBtn);

            this.Controls.Add(editPanel);
        }

        /**
         * Creates a labeled TextBox for editing hero details.
         */
        private void CreateEditField(SiticonePanel parent, string labelText, out TextBox textBox, int x, int y, bool isReadOnly)
        {
            SiticoneLabel label = new SiticoneLabel
            {
                Text = labelText,
                Location = new Point(x, y),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = accentBlue,
                AutoSize = true
            };
            parent.Controls.Add(label);

            textBox = new TextBox
            {
                Size = new Size(300, 35),
                Location = new Point(x, y + 25),
                Font = new Font("Segoe UI", 11),
                BackColor = isReadOnly ? Color.FromArgb(70, 70, 90) : Color.White,
                ForeColor = isReadOnly ? accentGray : Color.Black,
                BorderStyle = BorderStyle.Fixed3D,
                ReadOnly = isReadOnly
            };
            parent.Controls.Add(textBox);
        }

        /**
         * Event handler for when a hero is selected from the DataGridView.
         * Populates the edit fields with the selected hero's details.
         */
        private void OnHeroSelected()
        {
            if (heroesGrid.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = heroesGrid.SelectedRows[0];
                string heroID = selectedRow.Cells["HeroID"].Value?.ToString() ?? "";

                var hero = heroesData.FirstOrDefault(h => h["HeroID"] == heroID);
                if (hero != null)
                {
                    txtHeroID.Text = hero["HeroID"];
                    txtName.Text = hero["Name"];
                    txtAge.Text = hero["Age"];
                    txtSuperpower.Text = hero["Superpower"];
                    txtExamScore.Text = hero["ExamScore"];
                }
            }
        }

        /**
         * Event handler for the Update Hero button click.
         * Validates inputs, updates hero data, and saves changes to the superheroes.txt file.
         */
        private void OnUpdateHeroClicked()
        {
            if (!validations.ValidateHeroInputs(txtHeroID, txtName, txtAge, txtSuperpower, txtExamScore))
                return;

            if (!int.TryParse(txtExamScore.Text, out int examScore))
            {
                MessageBox.Show("Exam Score must be a valid number!", "Validation Error");
                return;
            }

            if (examScore < 0 || examScore > 100)
            {
                MessageBox.Show("Exam Score must be between 0 and 100!", "Validation Error");
                return;
            }

            string heroID = txtHeroID.Text;
            var hero = heroesData.FirstOrDefault(h => h["HeroID"] == heroID);

            if (hero != null)
            {
                hero["Name"] = txtName.Text;
                hero["Age"] = txtAge.Text;
                hero["Superpower"] = txtSuperpower.Text;
                hero["ExamScore"] = txtExamScore.Text;

                var (rank, threatLevel) = calculations.DetermineRankAndThreat(examScore);
                hero["Rank"] = rank + "-Rank";
                hero["ThreatLevel"] = threatLevel;

                SaveHeroesToFile();
                RefreshGrid();
                ClearEditFields();

                MessageBox.Show($"Hero '{txtName.Text}' updated successfully!", "Success");
            }
            else
            {
                MessageBox.Show("Hero not found!", "Error");
            }
        }

        /**
         * Clears all edit fields and resets the selection in the DataGridView.
         */
        private void ClearEditFields()
        {
            txtHeroID.Text = "";
            txtName.Text = "";
            txtAge.Text = "";
            txtSuperpower.Text = "";
            txtExamScore.Text = "";
            heroesGrid.ClearSelection();
        }

        /**
         * Loads hero data from the superheroes.txt file into the heroesData list and refreshes the DataGridView.
         */
        private void LoadHeroesFromFile()
        {
            heroesData.Clear();
            var lines = fileHandler.ReadAllHeroes();

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var hero = ParseHeroRecord(line);
                if (hero != null)
                {
                    heroesData.Add(hero);
                }
            }

            RefreshGrid();
        }

        /**
         * Parses a single line from the superheroes.txt file into a dictionary representing a hero.
         */
        private Dictionary<string, string> ParseHeroRecord(string line)
        {
            var parts = line.Split('|');
            if (parts.Length < 7)
                return null;

            return new Dictionary<string, string>
            {
                { "HeroID", parts[0].Trim() },
                { "Name", parts[1].Trim() },
                { "Age", parts[2].Trim() },
                { "Superpower", parts[3].Trim() },
                { "ExamScore", parts[4].Trim() },
                { "Rank", parts[5].Trim() },
                { "ThreatLevel", parts[6].Trim() }
            };
        }

        /**
         * Refreshes the DataGridView to display the current heroesData list.
         */
        private void RefreshGrid()
        {
            heroesGrid.Rows.Clear();

            foreach (var hero in heroesData)
            {
                heroesGrid.Rows.Add(
                    hero["HeroID"],
                    hero["Name"],
                    hero["Age"],
                    hero["Superpower"],
                    hero["ExamScore"],
                    hero["Rank"],
                    hero["ThreatLevel"]
                );
            }

            for (int i = 0; i < heroesGrid.Rows.Count; i++)
            {
                string rank = heroesGrid.Rows[i].Cells["Rank"].Value.ToString();
                Color rankColor = GetRankColor(rank);
                heroesGrid.Rows[i].Cells["Rank"].Style.ForeColor = rankColor;
                heroesGrid.Rows[i].Cells["Rank"].Style.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            }
        }

        /**
         * Saves the current heroesData list back to the superheroes.txt file.
         */
        private void SaveHeroesToFile()
        {
            List<string> heroRecords = new List<string>();

            foreach (var hero in heroesData)
            {
                string record = $"{hero["HeroID"]}|{hero["Name"]}|{hero["Age"]}|{hero["Superpower"]}|{hero["ExamScore"]}|{hero["Rank"]}|{hero["ThreatLevel"]}";
                heroRecords.Add(record);
            }

            fileHandler.OverwriteAllHeroes(heroRecords);
        }

        /**
         * Returns the color associated with a given rank.
         */
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
