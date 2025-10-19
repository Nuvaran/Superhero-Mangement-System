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
            LoadSampleData();
        }

        private void SetupForm()
        {
            this.Text = "View All Heroes";
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
            TextBox searchBox = new TextBox
            {
                Size = new Size(200, 30),
                Location = new Point(20, 35),
                Font = new Font("Segoe UI", 10),
                BackColor = Color.White,
                ForeColor = Color.Black,
                BorderStyle = BorderStyle.Fixed3D
            };
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
            ComboBox rankCombo = new ComboBox
            {
                Size = new Size(120, 30),
                Location = new Point(250, 35),
                Font = new Font("Segoe UI", 10),
                BackColor = Color.White,
                ForeColor = Color.Black
            };
            rankCombo.Items.AddRange(new string[] { "All", "S-Rank", "A-Rank", "B-Rank", "C-Rank" });
            rankCombo.SelectedIndex = 0;
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
            refreshBtn.Click += (s, e) => MessageBox.Show("Refresh clicked! Logic to be implemented.", "Info");
            filterPanel.Controls.Add(refreshBtn);

            this.Controls.Add(filterPanel);
        }

        private void InitializeDataGridView()
        {
            // DataGridView Panel
            SiticonePanel gridPanel = new SiticonePanel
            {
                Size = new Size(940, 300),
                Location = new Point(30, 165),
                FillColor = darkSecondary,
                BorderThickness = 1
            };

            // DataGridView
            DataGridView dataGridView = new DataGridView
            {
                Size = new Size(920, 276),
                Location = new Point(10, 10),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                ColumnHeadersHeight = 35,
                RowTemplate = { Height = 30 },
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            dataGridView.BackgroundColor = darkBg;
            dataGridView.ForeColor = Color.White;
            dataGridView.Font = new Font("Segoe UI", 10);
            dataGridView.GridColor = Color.FromArgb(50, 50, 80);
            dataGridView.BorderStyle = BorderStyle.None;

            // Setup columns
            dataGridView.Columns.Add("HeroID", "Hero ID");
            dataGridView.Columns.Add("Name", "Name");
            dataGridView.Columns.Add("Age", "Age");
            dataGridView.Columns.Add("Superpower", "Superpower");
            dataGridView.Columns.Add("ExamScore", "Exam Score");
            dataGridView.Columns.Add("Rank", "Rank");
            dataGridView.Columns.Add("ThreatLevel", "Threat Level");

            // Style headers
            dataGridView.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = darkSecondary,
                ForeColor = accentBlue,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };

            // Style cells
            dataGridView.DefaultCellStyle = new DataGridViewCellStyle
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
            dataGridView.AlternatingRowsDefaultCellStyle = alternatingStyle;

            gridPanel.Controls.Add(dataGridView);
            this.Controls.Add(gridPanel);

            // Store reference to dataGridView for later use
            this.Tag = dataGridView;
        }

        private void LoadSampleData()
        {
            DataGridView dataGridView = (DataGridView)this.Tag;

            // Sample data - Replace with actual data from file/database
            dataGridView.Rows.Add("H001", "Saitama", 25, "One Punch", 98, "S-Rank", "Dragon");
            dataGridView.Rows.Add("H002", "Genos", 18, "Incinerate", 92, "S-Rank", "Dragon");
            dataGridView.Rows.Add("H003", "Mumen Rider", 28, "Justice Crash", 78, "A-Rank", "Tiger");
            dataGridView.Rows.Add("H004", "Tatsumaki", 28, "Telekinesis", 95, "S-Rank", "Dragon");
            dataGridView.Rows.Add("H005", "Sonic", 25, "Speed", 88, "A-Rank", "Tiger");
            dataGridView.Rows.Add("H006", "King", 30, "Popularity", 72, "B-Rank", "Wolf");
            dataGridView.Rows.Add("H007", "Puri Puri", 26, "Beast", 85, "A-Rank", "Tiger");
            dataGridView.Rows.Add("H008", "Zombieman", 40, "Regeneration", 80, "A-Rank", "Tiger");

            // Color code the Rank column
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                string rank = dataGridView.Rows[i].Cells["Rank"].Value.ToString();
                Color rankColor = GetRankColor(rank);
                dataGridView.Rows[i].Cells["Rank"].Style.ForeColor = rankColor;
                dataGridView.Rows[i].Cells["Rank"].Style.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            }
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
