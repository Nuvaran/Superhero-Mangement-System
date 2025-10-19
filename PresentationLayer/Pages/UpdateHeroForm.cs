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

        public UpdateHeroForm()
        {
            InitializeComponent();
        }

        private void UpdateHeroForm_Load(object sender, EventArgs e)
        {
            SetupForm();
            InitializeHeader();
            InitializeHeroList();
            InitializeEditPanel();
            LoadSampleData();
        }

        private void SetupForm()
        {
            this.Text = "Update Hero Information";
            this.Size = new Size(1000, 700);
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = darkBg;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.DoubleBuffered = true;
            this.MinimumSize = new Size(1000, 600);
        }

        private void InitializeHeader()
        {
            // Title Label
            SiticoneLabel titleLabel = new SiticoneLabel
            {
                Text = "⚡ Update Hero Information ⚡",
                Location = new Point(30, 20),
                Font = new Font("Orbitron", 18, FontStyle.Bold),
                ForeColor = accentGold,
                AutoSize = true
            };
            this.Controls.Add(titleLabel);

            // Subtitle
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

        private void InitializeHeroList()
        {
            // Heroes List Panel (Left side)
            SiticonePanel listPanel = new SiticonePanel
            {
                Size = new Size(500, 420),
                Location = new Point(30, 80),
                FillColor = darkSecondary,
                BorderThickness = 1
            };

            // List Title
            SiticoneLabel listTitle = new SiticoneLabel
            {
                Text = "📋 Heroes List",
                Location = new Point(15, 15),
                Font = new Font("Orbitron", 12, FontStyle.Bold),
                ForeColor = accentBlue,
                AutoSize = true
            };
            listPanel.Controls.Add(listTitle);

            // DataGridView for heroes list
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

            // Setup columns
            heroesGrid.Columns.Add("HeroID", "Hero ID");
            heroesGrid.Columns.Add("Name", "Name");
            heroesGrid.Columns.Add("Age", "Age");
            heroesGrid.Columns.Add("Superpower", "Superpower");
            heroesGrid.Columns.Add("ExamScore", "Exam Score");
            heroesGrid.Columns.Add("Rank", "Rank");
            heroesGrid.Columns.Add("ThreatLevel", "Threat Level");

            // Set column widths
            heroesGrid.Columns["HeroID"].Width = 60;
            heroesGrid.Columns["Name"].Width = 70;
            heroesGrid.Columns["Age"].Width = 45;
            heroesGrid.Columns["Superpower"].Width = 90;
            heroesGrid.Columns["ExamScore"].Width = 70;
            heroesGrid.Columns["Rank"].Width = 55;
            heroesGrid.Columns["ThreatLevel"].Width = 60;

            // Style headers
            heroesGrid.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = darkSecondary,
                ForeColor = accentBlue,
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };

            // Style cells
            heroesGrid.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = darkBg,
                ForeColor = Color.White,
                SelectionBackColor = Color.FromArgb(50, 50, 100),
                SelectionForeColor = accentGold,
                Font = new Font("Segoe UI", 8)
            };

            // Row selection event
            heroesGrid.SelectionChanged += (s, e) => OnHeroSelected();

            listPanel.Controls.Add(heroesGrid);
            this.Controls.Add(listPanel);
        }

        private void InitializeEditPanel()
        {
            // Edit Panel (Right side)
            SiticonePanel editPanel = new SiticonePanel
            {
                Size = new Size(435, 420),
                Location = new Point(540, 80),
                FillColor = darkSecondary,
                BorderThickness = 1
            };

            // Edit Title
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

            // Hero ID (Read-only)
            CreateEditField(editPanel, "Hero ID:", out txtHeroID, 20, yPos, true);
            yPos += 60;

            // Name
            CreateEditField(editPanel, "Name:", out txtName, 20, yPos, false);
            yPos += 60;

            // Age
            CreateEditField(editPanel, "Age:", out txtAge, 20, yPos, false);
            yPos += 60;

            // Superpower
            CreateEditField(editPanel, "Superpower:", out txtSuperpower, 20, yPos, false);
            yPos += 60;

            // Exam Score
            CreateEditField(editPanel, "Exam Score:", out txtExamScore, 20, yPos, false);
            yPos += 70;

            // Update Button
            SiticoneButton updateBtn = new SiticoneButton
            {
                Text = "💾 Update Hero",
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

            // Cancel Button
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

        private void CreateEditField(SiticonePanel parent, string labelText, out TextBox textBox, int x, int y, bool isReadOnly)
        {
            // Label
            SiticoneLabel label = new SiticoneLabel
            {
                Text = labelText,
                Location = new Point(x, y),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = accentBlue,
                AutoSize = true
            };
            parent.Controls.Add(label);

            // TextBox
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

        private void OnHeroSelected()
        {
            if (heroesGrid.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = heroesGrid.SelectedRows[0];

                // Populate edit fields
                txtHeroID.Text = selectedRow.Cells["HeroID"].Value?.ToString() ?? "";
                txtName.Text = GetFullHeroData(selectedRow.Cells["HeroID"].Value?.ToString() ?? "", "Name");
                txtAge.Text = GetFullHeroData(selectedRow.Cells["HeroID"].Value?.ToString() ?? "", "Age");
                txtSuperpower.Text = GetFullHeroData(selectedRow.Cells["HeroID"].Value?.ToString() ?? "", "Superpower");
                txtExamScore.Text = GetFullHeroData(selectedRow.Cells["HeroID"].Value?.ToString() ?? "", "ExamScore");
            }
        }

        private string GetFullHeroData(string heroID, string fieldName)
        {
            // Complete hero database with all sample data
            switch (heroID)
            {
                case "H001":
                    return fieldName == "Name" ? "Saitama" : fieldName == "Age" ? "25" : fieldName == "Superpower" ? "One Punch" : "98";
                case "H002":
                    return fieldName == "Name" ? "Genos" : fieldName == "Age" ? "18" : fieldName == "Superpower" ? "Incinerate" : "92";
                case "H003":
                    return fieldName == "Name" ? "Mumen Rider" : fieldName == "Age" ? "28" : fieldName == "Superpower" ? "Justice Crash" : "78";
                case "H004":
                    return fieldName == "Name" ? "Tatsumaki" : fieldName == "Age" ? "28" : fieldName == "Superpower" ? "Telekinesis" : "95";
                case "H005":
                    return fieldName == "Name" ? "Sonic" : fieldName == "Age" ? "25" : fieldName == "Superpower" ? "Speed" : "88";
                case "H006":
                    return fieldName == "Name" ? "King" : fieldName == "Age" ? "30" : fieldName == "Superpower" ? "Popularity" : "72";
                case "H007":
                    return fieldName == "Name" ? "Puri Puri" : fieldName == "Age" ? "26" : fieldName == "Superpower" ? "Beast" : "85";
                case "H008":
                    return fieldName == "Name" ? "Zombieman" : fieldName == "Age" ? "40" : fieldName == "Superpower" ? "Regeneration" : "80";
                default:
                    return "";
            }
        }

        private void OnUpdateHeroClicked()
        {
            if (string.IsNullOrEmpty(txtHeroID.Text))
            {
                MessageBox.Show("Please select a hero to update.", "Warning");
                return;
            }

            MessageBox.Show($"Hero '{txtName.Text}' updated successfully!\nLogic to be implemented.", "Success");
        }

        private void ClearEditFields()
        {
            txtHeroID.Text = "";
            txtName.Text = "";
            txtAge.Text = "";
            txtSuperpower.Text = "";
            txtExamScore.Text = "";
            heroesGrid.ClearSelection();
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
                heroesGrid.Rows[i].Cells["Rank"].Style.Font = new Font("Segoe UI", 8, FontStyle.Bold);
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
