using SiticoneNetFrameworkUI;
using Superhero_Mangement_System.DataLayer;
using Superhero_Mangement_System.BusinessLogicLayer;
using Superhero_Mangement_System.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Superhero_Mangement_System.PresentationLayer.Pages
{
    public partial class DashboardForm : Form
    {
        private bool isDarkMode = true;
        private SiticonePanel contentPanel;
        private SiticonePanel navPanel;
        private SiticoneButton activeNavButton;
        private bool isNavOpen = false;
        private System.Windows.Forms.Timer slideTimer;
        private Color accentGold = Color.FromArgb(255, 215, 0);
        private Color accentBlue = Color.FromArgb(0, 191, 255);
        private Color accentGreen = Color.FromArgb(50, 205, 50);
        private Color accentRed = Color.FromArgb(220, 53, 69);
        private Color accentGray = Color.FromArgb(169, 169, 169);
        private Color darkBg = Color.FromArgb(26, 26, 46);
        private Color darkSecondary = Color.FromArgb(18, 18, 43);

        private FileHandler fileHandler;
        private List<Dictionary<string, string>> heroesData;

        public DashboardForm()
        {
            InitializeComponent();
            fileHandler = new FileHandler();
            heroesData = new List<Dictionary<string, string>>();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (!DesignMode)
            {
                SetupForm();
                InitializeHeader();
                InitializeNavigation();
                InitializeDashboardContent();
            }
        }

        private void SetupForm()
        {
            this.Text = "One Kick Heroes Academy HQ";
            this.Size = new Size(1000, 600);
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.BackColor = darkBg;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.DoubleBuffered = true;
            this.MinimumSize = new Size(800, 500);
        }

        private void InitializeHeader()
        {
            SiticonePanel headerPanel = new SiticonePanel
            {
                Size = new Size(1000, 80),
                Location = new Point(0, 0),
                FillColor = darkSecondary,
                BorderThickness = 0
            };

            PictureBox logoPictureBox = new PictureBox
            {
                Image = Properties.Resources.logo, 
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(90, 115), 
                Location = new Point((headerPanel.Width - 120) / 2, (headerPanel.Height - 120) / 2),
                BackColor = Color.Transparent,
                Anchor = AnchorStyles.Top
            };

            SiticoneButton menuToggleBtn = new SiticoneButton
            {
                Text = "☰",
                Size = new Size(40, 40),
                Location = new Point(20, 20),
                ForeColor = accentGold,
                Font = new Font("Arial", 18, FontStyle.Bold)
            };
            menuToggleBtn.BackColor = Color.FromArgb(50, 50, 100);
            menuToggleBtn.MouseEnter += (s, e) => menuToggleBtn.BackColor = accentGold;
            menuToggleBtn.MouseLeave += (s, e) => menuToggleBtn.BackColor = Color.FromArgb(50, 50, 100);
            menuToggleBtn.Click += (s, e) => ToggleSideMenu();

            SiticoneButton themeToggleBtn = new SiticoneButton
            {
                Text = isDarkMode ? "☀" : "🌙",
                Size = new Size(40, 40),
                Location = new Point(940, 20),
                ForeColor = accentBlue,
                Font = new Font("Arial", 18, FontStyle.Bold)
            };
            themeToggleBtn.BackColor = Color.FromArgb(50, 50, 100);
            themeToggleBtn.MouseEnter += (s, e) => themeToggleBtn.BackColor = accentBlue;
            themeToggleBtn.MouseLeave += (s, e) => themeToggleBtn.BackColor = Color.FromArgb(50, 50, 100);
            themeToggleBtn.Click += (s, e) => ToggleTheme(themeToggleBtn);

            headerPanel.Controls.Add(logoPictureBox);
            headerPanel.Controls.Add(menuToggleBtn);
            headerPanel.Controls.Add(themeToggleBtn);
            this.Controls.Add(headerPanel);
        }

        private void InitializeNavigation()
        {
            navPanel = new SiticonePanel
            {
                Size = new Size(220, 520),
                Location = new Point(-220, 80),
                FillColor = darkSecondary,
                BorderThickness = 0
            };

            string[] navItems = { "Dashboard", "Add New Hero", "View All Heroes", "Update Hero", "Delete Hero", "Reports", "Close" };
            string[] navIcons = { "🏠", "➕", "👥", "✏️", "🗑️", "📊", "❌" };
            int yPos = 20;

            for (int i = 0; i < navItems.Length; i++)
            {
                CreateNavMenuItem(navItems[i], navIcons[i], yPos);
                yPos += 70;
            }

            this.Controls.Add(navPanel);
        }

        private void CreateNavMenuItem(string itemName, string icon, int yPos)
        {
            SiticonePanel menuItemPanel = new SiticonePanel
            {
                Size = new Size(190, 55),
                Location = new Point(15, yPos),
                FillColor = darkBg,
                BorderThickness = 0,
                Parent = navPanel
            };

            SiticoneLabel iconLabel = new SiticoneLabel
            {
                Text = icon,
                Location = new Point(12, 12),
                Size = new Size(30, 30),
                Font = new Font("Arial", 18, FontStyle.Bold),
                ForeColor = accentBlue,
                AutoSize = false
            };

            SiticoneLabel textLabel = new SiticoneLabel
            {
                Text = itemName,
                Location = new Point(50, 15),
                Size = new Size(130, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.White,
                AutoSize = false
            };

            menuItemPanel.Controls.Add(iconLabel);
            menuItemPanel.Controls.Add(textLabel);

            if (itemName == "Dashboard")
            {
                activeNavButton = new SiticoneButton { Tag = itemName };
                menuItemPanel.BackColor = Color.FromArgb(40, 40, 80);
                iconLabel.ForeColor = accentGold;
                textLabel.ForeColor = accentGold;

                Panel leftBorder = new Panel
                {
                    Size = new Size(4, 55),
                    Location = new Point(0, 0),
                    BackColor = accentGold
                };
                menuItemPanel.Controls.Add(leftBorder);
            }

            menuItemPanel.MouseEnter += (s, e) =>
            {
                menuItemPanel.BackColor = Color.FromArgb(40, 40, 80);
                iconLabel.ForeColor = accentGold;
                textLabel.ForeColor = accentGold;
                menuItemPanel.Cursor = Cursors.Hand;
            };

            menuItemPanel.MouseLeave += (s, e) =>
            {
                if (itemName != "Dashboard" || (activeNavButton != null && activeNavButton.Tag as string != itemName))
                {
                    menuItemPanel.BackColor = darkBg;
                    iconLabel.ForeColor = accentBlue;
                    textLabel.ForeColor = Color.White;
                }
            };

            menuItemPanel.Click += (s, e) => SelectNavItem(itemName, menuItemPanel, iconLabel, textLabel);
            iconLabel.Click += (s, e) => SelectNavItem(itemName, menuItemPanel, iconLabel, textLabel);
            textLabel.Click += (s, e) => SelectNavItem(itemName, menuItemPanel, iconLabel, textLabel);

            navPanel.Controls.Add(menuItemPanel);
        }

        private void SelectNavItem(string itemName, SiticonePanel menuPanel, SiticoneLabel iconLabel, SiticoneLabel textLabel)
        {
            foreach (Control control in navPanel.Controls)
            {
                if (control is SiticonePanel panel && panel != menuPanel)
                {
                    panel.BackColor = darkBg;
                    foreach (Control child in panel.Controls)
                    {
                        if (child is SiticoneLabel lbl)
                        {
                            if (lbl == panel.Controls[0])
                                lbl.ForeColor = accentBlue;
                            else
                                lbl.ForeColor = Color.White;
                        }
                        else if (child is Panel border)
                        {
                            panel.Controls.Remove(border);
                        }
                    }
                }
            }

            menuPanel.BackColor = Color.FromArgb(40, 40, 80);
            iconLabel.ForeColor = accentGold;
            textLabel.ForeColor = accentGold;

            Panel leftAccent = new Panel
            {
                Size = new Size(4, 55),
                Location = new Point(0, 0),
                BackColor = accentGold
            };
            menuPanel.Controls.Add(leftAccent);

            LoadContentPage(itemName);

            if (isNavOpen)
            {
                ToggleSideMenu();
            }
        }

        private void InitializeDashboardContent()
        {
            contentPanel = new SiticonePanel
            {
                Size = new Size(1000, 520),
                Location = new Point(0, 80),
                FillColor = darkBg,
                BorderThickness = 0,
                AutoScroll = true
            };

            LoadDashboardPage();
            this.Controls.Add(contentPanel);
        }

        private void LoadContentPage(string pageName)
        {
            contentPanel.Controls.Clear();

            switch (pageName)
            {
                case "Dashboard":
                    LoadDashboardPage();
                    break;
                case "Add New Hero":
                    LoadAddHeroForm();
                    break;
                case "View All Heroes":
                    LoadViewAllHeroesForm();
                    break;
                case "Update Hero":
                    LoadUpdateHeroForm();
                    break;
                case "Delete Hero":
                    LoadDeleteHeroForm();
                    break;
                case "Reports":
                    LoadReportsForm();
                    break;
                case "Close":
                    Application.Exit();
                    break;
            }
        }

        private void LoadAddHeroForm()
        {
            contentPanel.Controls.Clear();
            AddHeroForm addHeroForm = new AddHeroForm
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            contentPanel.Controls.Add(addHeroForm);
            addHeroForm.Show();
        }

        private void LoadViewAllHeroesForm()
        {
            contentPanel.Controls.Clear();
            ViewAllHeroesForm viewAllHeroesForm = new ViewAllHeroesForm
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            contentPanel.Controls.Add(viewAllHeroesForm);
            viewAllHeroesForm.Show();
        }

        private void LoadUpdateHeroForm()
        {
            contentPanel.Controls.Clear();
            UpdateHeroForm updateHeroForm = new UpdateHeroForm
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            contentPanel.Controls.Add(updateHeroForm);
            updateHeroForm.Show();
        }

        private void LoadDeleteHeroForm()
        {
            contentPanel.Controls.Clear();
            DeleteHeroForm deleteHeroForm = new DeleteHeroForm
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            contentPanel.Controls.Add(deleteHeroForm);
            deleteHeroForm.Show();
        }

        private void LoadReportsForm()
        {
            contentPanel.Controls.Clear();
            ReportsForm reportsForm = new ReportsForm
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            contentPanel.Controls.Add(reportsForm);
            reportsForm.Show();
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

        private void LoadDashboardPage()
        {
            LoadHeroesData();

            SiticoneLabel dashboardTitle = new SiticoneLabel
            {
                Text = "⚡ COMMAND CENTER ⚡",
                Location = new Point(20, 15),
                Font = new Font("Orbitron", 16, FontStyle.Bold),
                ForeColor = accentGold,
                AutoSize = true
            };
            contentPanel.Controls.Add(dashboardTitle);

            int totalHeroes = heroesData.Count;
            double avgAge = heroesData.Count > 0 ? heroesData.Average(h => double.TryParse(h["Age"], out double age) ? age : 0) : 0;
            double avgScore = heroesData.Count > 0 ? heroesData.Average(h => double.TryParse(h["ExamScore"], out double score) ? score : 0) : 0;

            CreateLargeStatCard("TOTAL HEROES", totalHeroes.ToString(), accentGold, 20, 70);
            CreateLargeStatCard("AVG AGE", avgAge.ToString("F1"), accentBlue, 250, 70);
            CreateLargeStatCard("AVG SCORE", avgScore.ToString("F1"), accentGreen, 480, 70);

            CreateTopHeroesPanel(20, 160);
            CreateRankDistributionPanel(490, 160);

            var topS = heroesData.Where(h => h["Rank"] == "S-Rank").OrderByDescending(h => double.TryParse(h["ExamScore"], out double s) ? s : 0).FirstOrDefault();
            if (topS != null)
            {
                CreateHeroSpotlightPanel(topS, 20, 340);
            }

            var topA = heroesData.Where(h => h["Rank"] == "A-Rank").OrderByDescending(h => double.TryParse(h["ExamScore"], out double s) ? s : 0).FirstOrDefault();
            if (topA != null)
            {
                CreateHeroSpotlightPanel(topA, 490, 340);
            }
        }

        private void CreateLargeStatCard(string title, string value, Color accentColor, int x, int y)
        {
            SiticonePanel card = new SiticonePanel
            {
                Size = new Size(210, 75),
                Location = new Point(x, y),
                FillColor = darkSecondary,
                BorderThickness = 2,
                Parent = contentPanel
            };

            SiticoneLabel cardTitle = new SiticoneLabel
            {
                Text = title,
                Location = new Point(10, 5),
                Font = new Font("Orbitron", 9, FontStyle.Bold),
                ForeColor = accentColor,
                AutoSize = true
            };

            SiticoneLabel cardValue = new SiticoneLabel
            {
                Text = value,
                Location = new Point(10, 28),
                Font = new Font("Orbitron", 24, FontStyle.Bold),
                ForeColor = accentColor,
                AutoSize = true
            };

            card.Controls.Add(cardTitle);
            card.Controls.Add(cardValue);

            card.MouseEnter += (s, e) =>
            {
                card.BorderThickness = 3;
                card.BackColor = Color.FromArgb(35, 35, 60);
            };
            card.MouseLeave += (s, e) =>
            {
                card.BorderThickness = 2;
                card.BackColor = darkSecondary;
            };
        }

        private void CreateTopHeroesPanel(int x, int y)
        {
            SiticonePanel panel = new SiticonePanel
            {
                Size = new Size(450, 160),
                Location = new Point(x, y),
                FillColor = darkSecondary,
                BorderThickness = 1,
                Parent = contentPanel
            };

            SiticoneLabel title = new SiticoneLabel
            {
                Text = "🏆 TOP 5 ELITE HEROES",
                Location = new Point(15, 10),
                Font = new Font("Orbitron", 10, FontStyle.Bold),
                ForeColor = accentGold,
                AutoSize = true
            };
            panel.Controls.Add(title);

            var topHeroes = heroesData.OrderByDescending(h => double.TryParse(h["ExamScore"], out double s) ? s : 0).Take(5).ToList();
            int heroY = 35;
            int rank = 1;

            foreach (var hero in topHeroes)
            {
                string heroText = $"{rank}. {hero["Name"]} ({hero["ExamScore"]}/100)";
                SiticoneLabel heroLabel = new SiticoneLabel
                {
                    Text = heroText,
                    Location = new Point(15, heroY),
                    Font = new Font("Segoe UI", 8),
                    ForeColor = rank == 1 ? accentGold : rank <= 3 ? accentBlue : Color.White,
                    AutoSize = true
                };
                panel.Controls.Add(heroLabel);
                heroY += 25;
                rank++;
            }
        }

        private void CreateRankDistributionPanel(int x, int y)
        {
            SiticonePanel panel = new SiticonePanel
            {
                Size = new Size(450, 160),
                Location = new Point(x, y),
                FillColor = darkSecondary,
                BorderThickness = 1,
                Parent = contentPanel
            };

            SiticoneLabel title = new SiticoneLabel
            {
                Text = "⚔️ RANK DISTRIBUTION",
                Location = new Point(15, 10),
                Font = new Font("Orbitron", 10, FontStyle.Bold),
                ForeColor = accentBlue,
                AutoSize = true
            };
            panel.Controls.Add(title);

            var sRank = heroesData.Count(h => h["Rank"] == "S-Rank");
            var aRank = heroesData.Count(h => h["Rank"] == "A-Rank");
            var bRank = heroesData.Count(h => h["Rank"] == "B-Rank");
            var cRank = heroesData.Count(h => h["Rank"] == "C-Rank");

            string rankInfo = $"S-Rank: {sRank}\nA-Rank: {aRank}\nB-Rank: {bRank}\nC-Rank: {cRank}";
            SiticoneLabel rankLabel = new SiticoneLabel
            {
                Text = rankInfo,
                Location = new Point(15, 35),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true
            };
            panel.Controls.Add(rankLabel);
        }

        private void CreateHeroSpotlightPanel(Dictionary<string, string> hero, int x, int y)
        {
            SiticonePanel panel = new SiticonePanel
            {
                Size = new Size(450, 140),
                Location = new Point(x, y),
                FillColor = darkSecondary,
                BorderThickness = 2,
                Parent = contentPanel
            };

            string rank = hero["Rank"].Replace("-Rank", "");
            Color rankColor = GetRankColor(rank);

            SiticoneLabel title = new SiticoneLabel
            {
                Text = $"🌟 SPOTLIGHT: {hero["Name"].ToUpper()}",
                Location = new Point(15, 10),
                Font = new Font("Orbitron", 11, FontStyle.Bold),
                ForeColor = rankColor,
                AutoSize = true
            };
            panel.Controls.Add(title);

            string heroDetails = $"Rank: {hero["Rank"]}\nPower: {hero["Superpower"]}\nScore: {hero["ExamScore"]}/100\nAge {hero["Age"]} Years";
            SiticoneLabel detailsLabel = new SiticoneLabel
            {
                Text = heroDetails,
                Location = new Point(15, 35),
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.White,
                AutoSize = true
            };
            panel.Controls.Add(detailsLabel);


            panel.UseBorderGradient = true;
            panel.BorderGradientStartColor = rankColor;
            panel.BorderGradientEndColor = rankColor;
        }

        private Color GetRankColor(string rank)
        {
            switch (rank)
            {
                case "S": return accentGold;
                case "A": return accentBlue;
                case "B": return accentGreen;
                case "C": return accentGray;
                default: return Color.White;
            }
        }

        private void ToggleTheme(SiticoneButton btn)
        {
            isDarkMode = !isDarkMode;
            btn.Text = isDarkMode ? "☀" : "🌙";
            MessageBox.Show("Theme toggle - Light/Dark mode switching logic would be implemented here.", "Theme");
        }

        private void ToggleSideMenu()
        {
            if (slideTimer != null && slideTimer.Enabled)
                return;

            slideTimer = new System.Windows.Forms.Timer();
            slideTimer.Interval = 10;
            int targetX = isNavOpen ? -220 : 0;
            int step = isNavOpen ? -8 : 8;

            slideTimer.Tick += (s, e) =>
            {
                if ((step > 0 && navPanel.Location.X >= targetX) || (step < 0 && navPanel.Location.X <= targetX))
                {
                    navPanel.Location = new Point(targetX, navPanel.Location.Y);
                    slideTimer.Stop();
                    slideTimer.Dispose();
                    isNavOpen = !isNavOpen;
                }
                else
                {
                    navPanel.Location = new Point(navPanel.Location.X + step, navPanel.Location.Y);
                }
            };

            slideTimer.Start();
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
        }
    }
}