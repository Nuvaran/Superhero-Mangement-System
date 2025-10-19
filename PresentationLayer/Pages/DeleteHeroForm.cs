using SiticoneNetFrameworkUI;
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
        private FileHandler fileHandler;
        private List<Dictionary<string, string>> heroesData;

        public DeleteHeroForm()
        {
            InitializeComponent();
            fileHandler = new FileHandler();
            heroesData = new List<Dictionary<string, string>>();
        }

        private void DeleteHeroForm_Load(object sender, EventArgs e)
        {
            SetupForm();
            InitializeHeader();
            InitializeDataGridView();
            InitializeButtons();
            LoadHeroesFromFile();
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
            SiticoneLabel titleLabel = new SiticoneLabel
            {
                Text = "⚡ Delete Hero ⚡",
                Location = new Point(30, 20),
                Font = new Font("Orbitron", 18, FontStyle.Bold),
                ForeColor = accentGold,
                AutoSize = true
            };
            this.Controls.Add(titleLabel);

            SiticoneLabel subtitleLabel = new SiticoneLabel
            {
                Text = "Select a hero from the list below and click Delete to remove them from the academy",
                Location = new Point(30, 50),
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.LightGray,
                AutoSize = true
            };
            this.Controls.Add(subtitleLabel);

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
            SiticonePanel gridPanel = new SiticonePanel
            {
                Size = new Size(940, 380),
                Location = new Point(30, 105),
                FillColor = darkSecondary,
                BorderThickness = 1
            };

            SiticoneLabel gridTitle = new SiticoneLabel
            {
                Text = "📋 Heroes Registry",
                Location = new Point(15, 15),
                Font = new Font("Orbitron", 12, FontStyle.Bold),
                ForeColor = accentBlue,
                AutoSize = true
            };
            gridPanel.Controls.Add(gridTitle);

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

            heroesGrid.Columns.Add("HeroID", "Hero ID");
            heroesGrid.Columns.Add("Name", "Name");
            heroesGrid.Columns.Add("Age", "Age");
            heroesGrid.Columns.Add("Superpower", "Superpower");
            heroesGrid.Columns.Add("ExamScore", "Exam Score");
            heroesGrid.Columns.Add("Rank", "Rank");
            heroesGrid.Columns.Add("ThreatLevel", "Threat Level");

            heroesGrid.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = darkSecondary,
                ForeColor = accentBlue,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };

            heroesGrid.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = darkBg,
                ForeColor = Color.White,
                SelectionBackColor = Color.FromArgb(50, 50, 100),
                SelectionForeColor = accentGold,
                Font = new Font("Segoe UI", 10),
                Padding = new Padding(5)
            };

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
            SiticonePanel buttonPanel = new SiticonePanel
            {
                Size = new Size(940, 70),
                Location = new Point(30, 550),
                FillColor = darkSecondary,
                BorderThickness = 1
            };

            SiticoneLabel infoLabel = new SiticoneLabel
            {
                Text = "Selected Hero: ",
                Location = new Point(20, 18),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = accentBlue,
                AutoSize = true
            };
            buttonPanel.Controls.Add(infoLabel);
            this.Tag = infoLabel;

            SiticoneLabel instructionLabel = new SiticoneLabel
            {
                Text = "Select a hero and press Backspace to delete",
                Location = new Point(20, 45),
                Font = new Font("Segoe UI", 9),
                ForeColor = accentGray,
                AutoSize = true
            };
            buttonPanel.Controls.Add(instructionLabel);

            this.Controls.Add(buttonPanel);

            heroesGrid.SelectionChanged += (s, e) => UpdateSelectedHeroInfo(infoLabel);
            heroesGrid.KeyDown += (s, e) => OnGridKeyDown(e);
        }

        private void OnGridKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                e.Handled = true;
                OnDeleteHeroClicked();
            }
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
            try
            {
                if (heroesGrid.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a hero to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataGridViewRow selectedRow = heroesGrid.SelectedRows[0];
                string heroName = selectedRow.Cells["Name"].Value?.ToString() ?? "Unknown";
                string heroID = selectedRow.Cells["HeroID"].Value?.ToString() ?? "";

                DialogResult result = MessageBox.Show(
                    $"Are you sure you want to delete '{heroName}' (ID: {heroID})?\n\nThis action cannot be undone!",
                    "Confirm Deletion",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    var heroToDelete = heroesData.FirstOrDefault(h => h["HeroID"] == heroID);

                    if (heroToDelete != null)
                    {
                        heroesData.Remove(heroToDelete);
                        SaveHeroesToFile();
                        RefreshGrid();

                        MessageBox.Show($"Hero '{heroName}' has been successfully deleted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        SiticoneLabel infoLabel = (SiticoneLabel)this.Tag;
                        infoLabel.Text = "Selected Hero: None";
                        infoLabel.ForeColor = accentGray;
                    }
                    else
                    {
                        MessageBox.Show("Hero not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting hero: {ex.Message}\n\n{ex.StackTrace}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearSelection()
        {
            heroesGrid.ClearSelection();
            SiticoneLabel infoLabel = (SiticoneLabel)this.Tag;
            infoLabel.Text = "Selected Hero: None";
            infoLabel.ForeColor = accentGray;
        }

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
                heroesGrid.Rows[i].Cells["Rank"].Style.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            }
        }

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
