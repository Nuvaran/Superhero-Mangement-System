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
    public partial class AddHeroForm : Form
    {
        private Color accentGold = Color.FromArgb(255, 215, 0);
        private Color accentBlue = Color.FromArgb(0, 191, 255);
        private Color accentGreen = Color.FromArgb(50, 205, 50);
        private Color accentGray = Color.FromArgb(169, 169, 169);
        private Color darkBg = Color.FromArgb(26, 26, 46);
        private Color darkSecondary = Color.FromArgb(18, 18, 43);

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
                Text = "🔄 Clear",
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
            DataGridView dataGridView = new DataGridView
            {
                Size = new Size(920, 207),
                Location = new Point(10, 10),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                ColumnHeadersHeight = 35,
                RowTemplate = { Height = 28 }
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

            // Set column widths
            dataGridView.Columns["HeroID"].Width = 70;
            dataGridView.Columns["Name"].Width = 100;
            dataGridView.Columns["Age"].Width = 60;
            dataGridView.Columns["Superpower"].Width = 130;
            dataGridView.Columns["ExamScore"].Width = 90;
            dataGridView.Columns["Rank"].Width = 70;
            dataGridView.Columns["ThreatLevel"].Width = 80;

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
                Font = new Font("Segoe UI", 10)
            };

            gridPanel.Controls.Add(dataGridView);
            this.Controls.Add(gridPanel);
        }

        private void ClearInputFieldsDashboard(SiticonePanel parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.Text = "";
                }
            }
        }

        private void OnSaveHeroClicked()
        {
            MessageBox.Show("Hero details saved! Logic to be implemented.", "Success");
        }
    }
}
