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

namespace Superhero_Mangement_System.PresentationLayer
{
    public partial class ViewAllHeroesForm : Form
    {
        private Color accentGold = Color.FromArgb(255, 215, 0);
        private Color accentBlue = Color.FromArgb(0, 191, 255);
        private Color accentGreen = Color.FromArgb(50, 205, 50);
        private Color accentGray = Color.FromArgb(169, 169, 169);
        private Color darkBg = Color.FromArgb(26, 26, 46);
        private Color darkSecondary = Color.FromArgb(18, 18, 43);

        private DataGridView heroesGrid;
        private TextBox searchBox;
        private ComboBox rankCombo;
        private List<string> allHeroes;

        public ViewAllHeroesForm()
        {
            InitializeComponent();
        }

        private void ViewAllHeroesForm_Load(object sender, EventArgs e)
        {
            SetupForm();
            InitializeHeader();
            InitializeFilterPanel();
            InitializeDataGridView();
            LoadAllHeroes();
        }

        private void SetupForm()
        {
            this.Text = "View All Heroes";
            this.Size = new Size(1000, 650);
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
                Text = "⚡ All Heroes Registry ⚡",
                Location = new Point(30, 20),
                Font = new Font("Orbitron", 18, FontStyle.Bold),
                ForeColor = accentGold,
                AutoSize = true
            };
            this.Controls.Add(titleLabel);

            // Subtitle
            SiticoneLabel subtitleLabel = new SiticoneLabel
            {
                Text = "Complete database of registered superheroes",
                Location = new Point(30, 50),
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.LightGray,
                AutoSize = true
            };
            this.Controls.Add(subtitleLabel);
        }

        private void InitializeFilterPanel()
        {
            // Filter Panel
            SiticonePanel filterPanel = new SiticonePanel
            {
                Size = new Size(940, 70),
                Location = new Point(30, 80),
                FillColor = darkSecondary,
                BorderThickness = 1
            };

            // Search Label
            SiticoneLabel searchLabel = new SiticoneLabel
            {
                Text = "Search by Name:",
                Location = new Point(20, 15),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = accentBlue,
                AutoSize = true
            };
            filterPanel.Controls.Add(searchLabel);

            // Search TextBox
            searchBox = new TextBox
            {
                Size = new Size(200, 30),
                Location = new Point(20, 35),
                Font = new Font("Segoe UI", 10),
                BackColor = Color.White,
                ForeColor = Color.Black,
                BorderStyle = BorderStyle.Fixed3D
            };
            searchBox.TextChanged += (s, e) => ApplyFilters();
            filterPanel.Controls.Add(searchBox);

            // Filter by Rank Label
            SiticoneLabel rankLabel = new SiticoneLabel
            {
                Text = "Filter by Rank:",
                Location = new Point(250, 15),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = accentBlue,
                AutoSize = true
            };
            filterPanel.Controls.Add(rankLabel);

            // Rank ComboBox
            rankCombo = new ComboBox
            {
                Size = new Size(120, 30),
                Location = new Point(250, 35),
                Font = new Font("Segoe UI", 10),
                BackColor = Color.White,
                ForeColor = Color.Black
            };
            rankCombo.Items.AddRange(new string[] { "All", "S-Rank", "A-Rank", "B-Rank", "C-Rank" });
            rankCombo.SelectedIndex = 0;
            rankCombo.SelectedIndexChanged += (s, e) => ApplyFilters();
            filterPanel.Controls.Add(rankCombo);

            // Refresh Button
            SiticoneButton refreshBtn = new SiticoneButton
            {
                Text = "🔄 Refresh",
                Size = new Size(120, 40),
                Location = new Point(800, 15),
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            refreshBtn.BackColor = accentGreen;
            refreshBtn.MouseEnter += (s, e) => refreshBtn.BackColor = Color.FromArgb(70, 225, 70);
            refreshBtn.MouseLeave += (s, e) => refreshBtn.BackColor = accentGreen;
            refreshBtn.Click += (s, e) => RefreshData();
            filterPanel.Controls.Add(refreshBtn);

            this.Controls.Add(filterPanel);
        }

        private void InitializeDataGridView()
        {
            // DataGridView Panel
            SiticonePanel gridPanel = new SiticonePanel
            {
                Size = new Size(940, 430),
                Location = new Point(30, 165),
                FillColor = darkSecondary,
                BorderThickness = 1
            };

            // DataGridView
            heroesGrid = new DataGridView
            {
                Size = new Size(920, 410),
                Location = new Point(10, 10),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                ColumnHeadersHeight = 35,
                RowTemplate = { Height = 30 },
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
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

        private void LoadAllHeroes()
        {
            try
            {
                FileHandler fileHandler = new FileHandler();
                allHeroes = fileHandler.ReadAllHeroes();
                DisplayHeroes(allHeroes);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading heroes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshData()
        {
            try
            {
                LoadAllHeroes();
                searchBox.Text = "";
                rankCombo.SelectedIndex = 0;
                MessageBox.Show("Data refreshed successfully!", "Refresh", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error refreshing data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyFilters()
        {
            if (allHeroes == null || allHeroes.Count == 0)
                return;

            string searchText = searchBox.Text.ToLower();
            string selectedRank = rankCombo.SelectedItem?.ToString() ?? "All";

            List<string> filteredHeroes = new List<string>();

            foreach (string hero in allHeroes)
            {
                if (string.IsNullOrWhiteSpace(hero)) continue;

                string[] parts = hero.Split('|');
                if (parts.Length < 7) continue;

                string heroName = parts[1].Trim().ToLower();
                string heroRank = parts[5].Trim();

                bool nameMatch = string.IsNullOrEmpty(searchText) || heroName.Contains(searchText);
                bool rankMatch = selectedRank == "All" || heroRank.Equals(selectedRank, StringComparison.OrdinalIgnoreCase);

                if (nameMatch && rankMatch)
                {
                    filteredHeroes.Add(hero);
                }
            }

            DisplayHeroes(filteredHeroes);
        }

        private void DisplayHeroes(List<string> heroes)
        {
            heroesGrid.Rows.Clear();

            foreach (string line in heroes)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                string[] parts = line.Split('|');
                if (parts.Length >= 7)
                {
                    heroesGrid.Rows.Add(
                        parts[0].Trim(),
                        parts[1].Trim(),
                        parts[2].Trim(),
                        parts[3].Trim(),
                        parts[4].Trim(),
                        parts[5].Trim(),
                        parts[6].Trim()
                    );

                    // Color code the Rank column
                    int lastRowIndex = heroesGrid.Rows.Count - 1;
                    string rank = parts[5].Trim();
                    Color rankColor = GetRankColor(rank);
                    heroesGrid.Rows[lastRowIndex].Cells["Rank"].Style.ForeColor = rankColor;
                    heroesGrid.Rows[lastRowIndex].Cells["Rank"].Style.Font = new Font("Segoe UI", 10, FontStyle.Bold);
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
