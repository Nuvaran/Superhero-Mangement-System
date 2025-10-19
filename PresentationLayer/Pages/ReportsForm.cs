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
    public partial class ReportsForm : Form
    {
        private Color accentGold = Color.FromArgb(255, 215, 0);
        private Color accentBlue = Color.FromArgb(0, 191, 255);
        private Color accentGreen = Color.FromArgb(50, 205, 50);
        private Color accentRed = Color.FromArgb(220, 53, 69);
        private Color accentGray = Color.FromArgb(169, 169, 169);
        private Color darkBg = Color.FromArgb(26, 26, 46);
        private Color darkSecondary = Color.FromArgb(18, 18, 43);

        private FileHandler fileHandler;
        private List<Dictionary<string, string>> heroesData;
        private Timer refreshTimer;
        private SiticonePanel rankPanel;
        private SiticonePanel threatPanel;
        private SiticonePanel statsPanel;

        public ReportsForm()
        {
            InitializeComponent();
            fileHandler = new FileHandler();
            heroesData = new List<Dictionary<string, string>>();
        }

        private void ReportsForm_Load(object sender, EventArgs e)
        {
            SetupForm();
            InitializeHeader();
            LoadHeroesData();
            InitializeStatisticCards();
            InitializeRankDistribution();
            InitializeDetailedReport();
            InitializeGenerateButton();
            SetupAutoRefresh();
        }

        private void SetupForm()
        {
            this.Text = "Reports & Analytics";
            this.Size = new Size(1000, 800);
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = darkBg;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.DoubleBuffered = true;
            this.MinimumSize = new Size(1000, 750);
        }

        private void SetupAutoRefresh()
        {
            refreshTimer = new Timer();
            refreshTimer.Interval = 2000;
            refreshTimer.Tick += (s, e) => RefreshReportsData();
            refreshTimer.Start();
        }

        private void RefreshReportsData()
        {
            var newHeroesData = new List<Dictionary<string, string>>();
            var lines = fileHandler.ReadAllHeroes();

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var parts = line.Split('|');
                if (parts.Length >= 7)
                {
                    var hero = new Dictionary<string, string>
                    {
                        { "HeroID", parts[0].Trim() },
                        { "Name", parts[1].Trim() },
                        { "Age", parts[2].Trim() },
                        { "Superpower", parts[3].Trim() },
                        { "ExamScore", parts[4].Trim() },
                        { "Rank", parts[5].Trim() },
                        { "ThreatLevel", parts[6].Trim() }
                    };
                    newHeroesData.Add(hero);
                }
            }

            if (newHeroesData.Count != heroesData.Count || !ListsEqual(newHeroesData, heroesData))
            {
                heroesData = newHeroesData;
                this.Controls.Remove(statsPanel);
                this.Controls.Remove(rankPanel);
                this.Controls.Remove(threatPanel);
                InitializeStatisticCards();
                InitializeRankDistribution();
                InitializeDetailedReport();
            }
        }

        private bool ListsEqual(List<Dictionary<string, string>> list1, List<Dictionary<string, string>> list2)
        {
            if (list1.Count != list2.Count) return false;
            for (int i = 0; i < list1.Count; i++)
            {
                if (list1[i]["HeroID"] != list2[i]["HeroID"]) return false;
            }
            return true;
        }

        private void InitializeHeader()
        {
            SiticoneLabel titleLabel = new SiticoneLabel
            {
                Text = "⚡ Reports and Analytics ⚡",
                Location = new Point(30, 20),
                Font = new Font("Orbitron", 18, FontStyle.Bold),
                ForeColor = accentGold,
                AutoSize = true
            };
            this.Controls.Add(titleLabel);

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

        private void LoadHeroesData()
        {
            heroesData.Clear();
            var lines = fileHandler.ReadAllHeroes();

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var parts = line.Split('|');
                if (parts.Length >= 7)
                {
                    var hero = new Dictionary<string, string>
                    {
                        { "HeroID", parts[0].Trim() },
                        { "Name", parts[1].Trim() },
                        { "Age", parts[2].Trim() },
                        { "Superpower", parts[3].Trim() },
                        { "ExamScore", parts[4].Trim() },
                        { "Rank", parts[5].Trim() },
                        { "ThreatLevel", parts[6].Trim() }
                    };
                    heroesData.Add(hero);
                }
            }
        }

        private int GetTotalHeroes() => heroesData.Count;

        private double GetAverageAge()
        {
            if (heroesData.Count == 0) return 0;
            return heroesData.Average(h => double.TryParse(h["Age"], out double age) ? age : 0);
        }

        private double GetAverageScore()
        {
            if (heroesData.Count == 0) return 0;
            return heroesData.Average(h => double.TryParse(h["ExamScore"], out double score) ? score : 0);
        }

        private Dictionary<string, int> GetRankDistribution()
        {
            var distribution = new Dictionary<string, int>
            {
                { "S-Rank", 0 },
                { "A-Rank", 0 },
                { "B-Rank", 0 },
                { "C-Rank", 0 }
            };

            foreach (var hero in heroesData)
            {
                string rank = hero["Rank"];
                if (distribution.ContainsKey(rank))
                    distribution[rank]++;
            }

            return distribution;
        }

        private Dictionary<string, int> GetThreatLevelDistribution()
        {
            var distribution = new Dictionary<string, int>();

            foreach (var hero in heroesData)
            {
                string threatLevel = hero["ThreatLevel"];
                if (distribution.ContainsKey(threatLevel))
                    distribution[threatLevel]++;
                else
                    distribution[threatLevel] = 1;
            }

            return distribution;
        }

        private void InitializeStatisticCards()
        {
            statsPanel = new SiticonePanel
            {
                Size = new Size(940, 110),
                Location = new Point(30, 80),
                FillColor = darkSecondary,
                BorderThickness = 1
            };

            int totalHeroes = GetTotalHeroes();
            double avgAge = GetAverageAge();
            double avgScore = GetAverageScore();

            int xPos = 8;

            CreateStatCard(statsPanel, "Total Heroes", totalHeroes.ToString(), accentGold, xPos, 12);
            xPos += 230;

            CreateStatCard(statsPanel, "Average Age", avgAge.ToString("F1"), accentBlue, xPos, 12);
            xPos += 230;

            CreateStatCard(statsPanel, "Average Score", avgScore.ToString("F1"), accentGreen, xPos, 12);
            xPos += 230;

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

            SiticoneLabel titleLabel = new SiticoneLabel
            {
                Text = title,
                Location = new Point(8, 5),
                Font = new Font("Segoe UI", 7, FontStyle.Bold),
                ForeColor = accentColor,
                AutoSize = true
            };
            card.Controls.Add(titleLabel);

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
            rankPanel = new SiticonePanel
            {
                Size = new Size(450, 260),
                Location = new Point(30, 205),
                FillColor = darkSecondary,
                BorderThickness = 1
            };

            SiticoneLabel titleLabel = new SiticoneLabel
            {
                Text = "📊 Heroes by Rank Distribution",
                Location = new Point(12, 8),
                Font = new Font("Orbitron", 10, FontStyle.Bold),
                ForeColor = accentBlue,
                AutoSize = true
            };
            rankPanel.Controls.Add(titleLabel);

            var rankDistribution = GetRankDistribution();
            int totalHeroes = GetTotalHeroes();

            int yPos = 35;
            foreach (var rank in new[] { "S-Rank", "A-Rank", "B-Rank", "C-Rank" })
            {
                int count = rankDistribution[rank];
                Color rankColor = GetRankColor(rank);
                CreateRankBar(rankPanel, rank, count, totalHeroes, rankColor, 12, yPos);
                yPos += 55;
            }

            this.Controls.Add(rankPanel);
        }

        private void CreateRankBar(SiticonePanel parent, string rankName, int count, int total, Color rankColor, int x, int y)
        {
            SiticoneLabel rankLabel = new SiticoneLabel
            {
                Text = $"{rankName}",
                Location = new Point(x, y),
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                ForeColor = rankColor,
                AutoSize = true
            };
            parent.Controls.Add(rankLabel);

            SiticoneLabel countLabel = new SiticoneLabel
            {
                Text = $"{count}/{total}",
                Location = new Point(x + 65, y),
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true
            };
            parent.Controls.Add(countLabel);

            Panel progressBg = new Panel
            {
                Size = new Size(220, 15),
                Location = new Point(x + 120, y),
                BackColor = Color.FromArgb(50, 50, 80),
                BorderStyle = BorderStyle.FixedSingle
            };
            parent.Controls.Add(progressBg);

            double percentage = total > 0 ? (double)count / total : 0;
            Panel progressBar = new Panel
            {
                Size = new Size((int)(220 * percentage), 15),
                Location = new Point(x + 120, y),
                BackColor = rankColor,
                BorderStyle = BorderStyle.None
            };
            parent.Controls.Add(progressBar);
            progressBar.BringToFront();

            SiticoneLabel percentLabel = new SiticoneLabel
            {
                Text = $"{(percentage * 100):F1}%",
                Location = new Point(x + 350, y),
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                ForeColor = rankColor,
                AutoSize = true
            };
            parent.Controls.Add(percentLabel);
            percentLabel.BringToFront();
        }

        private void InitializeDetailedReport()
        {
            threatPanel = new SiticonePanel
            {
                Size = new Size(460, 260),
                Location = new Point(510, 205),
                FillColor = darkSecondary,
                BorderThickness = 1
            };

            SiticoneLabel titleLabel = new SiticoneLabel
            {
                Text = "⚠️ Threat Level Distribution",
                Location = new Point(12, 8),
                Font = new Font("Orbitron", 10, FontStyle.Bold),
                ForeColor = accentRed,
                AutoSize = true
            };
            threatPanel.Controls.Add(titleLabel);

            var threatDistribution = GetThreatLevelDistribution();
            var sortedThreats = threatDistribution.OrderByDescending(x => x.Value).ToList();

            DrawPieChart(threatPanel, sortedThreats);

            this.Controls.Add(threatPanel);
        }

        private void DrawPieChart(SiticonePanel parent, List<KeyValuePair<string, int>> threatData)
        {
            if (threatData.Count == 0) return;

            Panel chartContainer = new Panel
            {
                Size = new Size(440, 220),
                Location = new Point(10, 35),
                BackColor = darkSecondary,
                BorderStyle = BorderStyle.None
            };

            int chartLeft = 10;
            int chartTop = 10;
            int chartSize = 150;

            Bitmap chartBitmap = new Bitmap(chartSize + 50, chartSize + 50);
            Graphics g = Graphics.FromImage(chartBitmap);
            g.Clear(darkSecondary);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int totalThreats = threatData.Sum(x => x.Value);
            float startAngle = 0;

            foreach (var threat in threatData)
            {
                float sweepAngle = (float)((threat.Value / (float)totalThreats) * 360);
                Color threatColor = GetThreatColor(threat.Key);
                double percentage = (threat.Value / (double)totalThreats) * 100;

                using (Brush brush = new SolidBrush(threatColor))
                {
                    g.FillPie(brush, 15, 15, chartSize, chartSize, startAngle, sweepAngle);
                }

                using (Pen pen = new Pen(darkSecondary, 2))
                {
                    g.DrawPie(pen, 15, 15, chartSize, chartSize, startAngle, sweepAngle);
                }

                float midAngle = startAngle + sweepAngle / 2;
                float radians = (float)(midAngle * Math.PI / 180);
                int labelX = (int)(15 + chartSize / 2 + Math.Cos(radians) * (chartSize / 3));
                int labelY = (int)(15 + chartSize / 2 + Math.Sin(radians) * (chartSize / 3));

                string percentText = $"{percentage:F0}%";
                using (Font font = new Font("Segoe UI", 8, FontStyle.Bold))
                using (Brush textBrush = new SolidBrush(Color.White))
                {
                    SizeF textSize = g.MeasureString(percentText, font);
                    g.DrawString(percentText, font, textBrush, labelX - textSize.Width / 2, labelY - textSize.Height / 2);
                }

                startAngle += sweepAngle;
            }

            g.Dispose();

            PictureBox chartPictureBox = new PictureBox
            {
                Image = chartBitmap,
                Size = new Size(chartSize + 50, chartSize + 50),
                Location = new Point(chartLeft, chartTop),
                SizeMode = PictureBoxSizeMode.AutoSize,
                BackColor = darkSecondary,
                BorderStyle = BorderStyle.None
            };
            chartContainer.Controls.Add(chartPictureBox);

            int legendX = chartLeft + chartSize + 80;
            int legendY = chartTop + 20;

            foreach (var threat in threatData)
            {
                Color threatColor = GetThreatColor(threat.Key);
                string threatName = ExtractThreatName(threat.Key);

                Panel legendBox = new Panel
                {
                    Size = new Size(12, 12),
                    Location = new Point(legendX, legendY),
                    BackColor = threatColor,
                    BorderStyle = BorderStyle.FixedSingle
                };
                chartContainer.Controls.Add(legendBox);

                SiticoneLabel legendLabel = new SiticoneLabel
                {
                    Text = $"{threatName}: {threat.Value}",
                    Location = new Point(legendX + 18, legendY - 2),
                    Font = new Font("Segoe UI", 8),
                    ForeColor = Color.White,
                    AutoSize = true
                };
                chartContainer.Controls.Add(legendLabel);

                legendY += 25;
            }

            parent.Controls.Add(chartContainer);
        }

        private string ExtractThreatName(string threatLevel)
        {
            if (threatLevel.Contains("Finals"))
                return "Finals Week";
            else if (threatLevel.Contains("Midterm"))
                return "Midterm";
            else if (threatLevel.Contains("Group"))
                return "Group Project";
            else if (threatLevel.Contains("Pop"))
                return "Pop Quiz";
            else
                return threatLevel;
        }

        private void InitializeGenerateButton()
        {
            SiticoneButton generateBtn = new SiticoneButton
            {
                Text = "Generate Summary",
                Size = new Size(200, 38),
                Location = new Point(400, 480),
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };
            generateBtn.BackColor = accentGold;
            generateBtn.MouseEnter += (s, e) => generateBtn.BackColor = Color.FromArgb(255, 230, 0);
            generateBtn.MouseLeave += (s, e) => generateBtn.BackColor = accentGold;
            generateBtn.Click += (s, e) => OnGenerateReportClicked();
            this.Controls.Add(generateBtn);
            generateBtn.BringToFront();
        }

        private void OnGenerateReportClicked()
        {
            try
            {
                LoadHeroesData();

                int totalHeroes = GetTotalHeroes();
                double avgAge = GetAverageAge();
                double avgScore = GetAverageScore();
                var rankDistribution = GetRankDistribution();

                StringBuilder reportBuilder = new StringBuilder();
                reportBuilder.AppendLine("=== ONE KICK HEROES ACADEMY SUMMARY REPORT ===");
                reportBuilder.AppendLine($"Generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                reportBuilder.AppendLine();
                reportBuilder.AppendLine($"Total Heroes: {totalHeroes}");
                reportBuilder.AppendLine($"Average Age: {avgAge:F1}");
                reportBuilder.AppendLine($"Average Exam Score: {avgScore:F1}");
                reportBuilder.AppendLine();
                reportBuilder.AppendLine("Heroes by Rank:");
                reportBuilder.AppendLine($"- S-Rank: {rankDistribution["S-Rank"]}");
                reportBuilder.AppendLine($"- A-Rank: {rankDistribution["A-Rank"]}");
                reportBuilder.AppendLine($"- B-Rank: {rankDistribution["B-Rank"]}");
                reportBuilder.AppendLine($"- C-Rank: {rankDistribution["C-Rank"]}");

                string reportContent = reportBuilder.ToString();
                fileHandler.SaveReport(reportContent);

                MessageBox.Show("Summary report has been generated and saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private Color GetThreatColor(string threatLevel)
        {
            string threat = threatLevel.ToLower();

            if (threat.Contains("finals week"))
                return accentRed;
            else if (threat.Contains("midterm madness"))
                return accentGold;
            else if (threat.Contains("group project"))
                return accentBlue;
            else if (threat.Contains("pop quiz"))
                return accentGreen;
            else
                return accentGray;
        }

    }
}
