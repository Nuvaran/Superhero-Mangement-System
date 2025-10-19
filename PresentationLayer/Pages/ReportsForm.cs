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
    public partial class ReportsForm : Form
    {
        private Color accentGold = Color.FromArgb(255, 215, 0);
        private Color accentBlue = Color.FromArgb(0, 191, 255);
        private Color accentGreen = Color.FromArgb(50, 205, 50);
        private Color accentRed = Color.FromArgb(220, 53, 69);
        private Color accentGray = Color.FromArgb(169, 169, 169);
        private Color darkBg = Color.FromArgb(26, 26, 46);
        private Color darkSecondary = Color.FromArgb(18, 18, 43);

        public ReportsForm()
        {
            InitializeComponent();
        }

        private void ReportsForm_Load(object sender, EventArgs e)
        {
            SetupForm();
            InitializeHeader();
            InitializeStatisticCards();
            InitializeRankDistribution();
            InitializeDetailedReport();
        }

        private void SetupForm()
        {
            this.Text = "Reports & Analytics";
            this.Size = new Size(1000, 700);
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = darkBg;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.DoubleBuffered = true;
            this.MinimumSize = new Size(1000, 650);
        }

        private void InitializeHeader()
        {
            // Title Label
            SiticoneLabel titleLabel = new SiticoneLabel
            {
                Text = "⚡ Reports and Analytics ⚡",
                Location = new Point(30, 20),
                Font = new Font("Orbitron", 18, FontStyle.Bold),
                ForeColor = accentGold,
                AutoSize = true
            };
            this.Controls.Add(titleLabel);

            // Subtitle
            SiticoneLabel subtitleLabel = new SiticoneLabel
            {
                Text = "Academy Performance Summary and Statistical Analysis",
                Location = new Point(30, 50),
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.LightGray,
                AutoSize = true
            };
            this.Controls.Add(subtitleLabel);
        }

        private void InitializeStatisticCards()
        {
            // Statistics Panel
            SiticonePanel statsPanel = new SiticonePanel
            {
                Size = new Size(917, 110),
                Location = new Point(30, 80),
                FillColor = darkSecondary,
                BorderThickness = 1
            };

            int xPos = 8;

            // Total Heroes Card
            CreateStatCard(statsPanel, "Total Heroes", "331", accentGold, xPos, 12);
            xPos += 230;

            // Average Age Card
            CreateStatCard(statsPanel, "Average Age", "27.5", accentBlue, xPos, 12);
            xPos += 230;

            // Average Score Card
            CreateStatCard(statsPanel, "Average Score", "85.2", accentGreen, xPos, 12);
            xPos += 230;

            // Last Updated Card
            CreateStatCard(statsPanel, "Last Updated", DateTime.Now.ToString("MM/dd/yyyy"), accentGray, xPos, 12);

            this.Controls.Add(statsPanel);
        }

        private void CreateStatCard(SiticonePanel parent, string title, string value, Color accentColor, int x, int y)
        {
            SiticonePanel card = new SiticonePanel
            {
                Size = new Size(210, 85),
                Location = new Point(x, y),
                FillColor = darkBg,
                BorderThickness = 2
            };

            // Title
            SiticoneLabel titleLabel = new SiticoneLabel
            {
                Text = title,
                Location = new Point(8, 5),
                Font = new Font("Segoe UI", 7, FontStyle.Bold),
                ForeColor = accentColor,
                AutoSize = true
            };
            card.Controls.Add(titleLabel);

            // Value
            SiticoneLabel valueLabel = new SiticoneLabel
            {
                Text = value,
                Location = new Point(8, 25),
                Font = new Font("Orbitron", 16, FontStyle.Bold),
                ForeColor = accentColor,
                AutoSize = true
            };
            card.Controls.Add(valueLabel);

            parent.Controls.Add(card);
        }

        private void InitializeRankDistribution()
        {
            // Rank Distribution Panel
            SiticonePanel rankPanel = new SiticonePanel
            {
                Size = new Size(530, 300),
                Location = new Point(30, 200),
                FillColor = darkSecondary,
                BorderThickness = 1
            };

            // Title
            SiticoneLabel titleLabel = new SiticoneLabel
            {
                Text = "📊 Heroes by Rank Distribution",
                Location = new Point(12, 8),
                Font = new Font("Orbitron", 10, FontStyle.Bold),
                ForeColor = accentBlue,
                AutoSize = true
            };
            rankPanel.Controls.Add(titleLabel);

            int yPos = 35;

            // S-Rank
            CreateRankBar(rankPanel, "S-Rank", 78, 331, accentGold, 12, yPos);
            yPos += 65;

            // A-Rank
            CreateRankBar(rankPanel, "A-Rank", 156, 331, accentBlue, 12, yPos);
            yPos += 65;

            // B-Rank
            CreateRankBar(rankPanel, "B-Rank", 68, 331, accentGreen, 12, yPos);
            yPos += 65;

            // C-Rank
            CreateRankBar(rankPanel, "C-Rank", 29, 331, accentGray, 12, yPos);

            this.Controls.Add(rankPanel);
        }

        private void CreateRankBar(SiticonePanel parent, string rankName, int count, int total, Color rankColor, int x, int y)
        {
            // Rank Label
            SiticoneLabel rankLabel = new SiticoneLabel
            {
                Text = $"{rankName}",
                Location = new Point(x, y),
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                ForeColor = rankColor,
                AutoSize = true
            };
            parent.Controls.Add(rankLabel);

            // Count Label
            SiticoneLabel countLabel = new SiticoneLabel
            {
                Text = $"{count}/{total}",
                Location = new Point(x + 65, y),
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true
            };
            parent.Controls.Add(countLabel);

            // Progress Bar Background
            Panel progressBg = new Panel
            {
                Size = new Size(280, 12),
                Location = new Point(x + 120, y + 2),
                BackColor = Color.FromArgb(50, 50, 80),
                BorderStyle = BorderStyle.FixedSingle
            };
            parent.Controls.Add(progressBg);

            // Progress Bar
            double percentage = (double)count / total;
            Panel progressBar = new Panel
            {
                Size = new Size((int)(280 * percentage), 12),
                Location = new Point(x + 120, y + 2),
                BackColor = rankColor
            };
            parent.Controls.Add(progressBar);

            // Percentage Label
            SiticoneLabel percentLabel = new SiticoneLabel
            {
                Text = $"{(percentage * 100):F1}%",
                Location = new Point(x + 410, y),
                Font = new Font("Segoe UI", 7, FontStyle.Bold),
                ForeColor = rankColor,
                AutoSize = true
            };
            parent.Controls.Add(percentLabel);
        }

        private void InitializeDetailedReport()
        {
            // Threat Level Distribution Panel
            SiticonePanel threatPanel = new SiticonePanel
            {
                Size = new Size(400, 320),
                Location = new Point(600, 200),
                FillColor = darkSecondary,
                BorderThickness = 1
            };

            // Title
            SiticoneLabel titleLabel = new SiticoneLabel
            {
                Text = "⚠️ Threat Level Distribution",
                Location = new Point(12, 8),
                Font = new Font("Orbitron", 10, FontStyle.Bold),
                ForeColor = accentRed,
                AutoSize = true
            };
            threatPanel.Controls.Add(titleLabel);

            // Threat Levels Table
            int yPos = 35;
            string[] threatLevels = { "Dragon", "Tiger", "Wolf", "Demon", "Unknown" };
            int[] threatCounts = { 45, 89, 127, 56, 14 };
            Color[] threatColors = { accentRed, accentGold, accentBlue, accentGreen, accentGray };

            for (int i = 0; i < threatLevels.Length; i++)
            {
                CreateThreatRow(threatPanel, threatLevels[i], threatCounts[i], threatColors[i], 12, yPos);
                yPos += 65;
            }

            this.Controls.Add(threatPanel);
        }

        private void CreateThreatRow(SiticonePanel parent, string threatLevel, int count, Color threatColor, int x, int y)
        {
            // Threat Level Label
            SiticoneLabel threatLabel = new SiticoneLabel
            {
                Text = threatLevel,
                Location = new Point(x, y),
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                ForeColor = threatColor,
                AutoSize = true
            };
            parent.Controls.Add(threatLabel);

            // Icon/Circle
            Panel circle = new Panel
            {
                Size = new Size(14, 14),
                Location = new Point(x + 90, y + 2),
                BackColor = threatColor,
                BorderStyle = BorderStyle.Fixed3D
            };
            parent.Controls.Add(circle);

            // Count Box
            SiticoneLabel countLabel = new SiticoneLabel
            {
                Text = count.ToString(),
                Location = new Point(x + 115, y),
                Font = new Font("Orbitron", 10, FontStyle.Bold),
                ForeColor = threatColor,
                AutoSize = true
            };
            parent.Controls.Add(countLabel);

            // Divider Line
            Panel divider = new Panel
            {
                Size = new Size(530, 1),
                Location = new Point(x, y + 25),
                BackColor = Color.FromArgb(50, 50, 80)
            };
            parent.Controls.Add(divider);
        }
    }
}
