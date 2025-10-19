using SiticoneNetFrameworkUI;
using System;
using System.Drawing;
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
        private Color accentGray = Color.FromArgb(169, 169, 169);
        private Color darkBg = Color.FromArgb(26, 26, 46);
        private Color darkSecondary = Color.FromArgb(18, 18, 43);

        public DashboardForm()
        {
            InitializeComponent();
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
            // Header Panel
            SiticonePanel headerPanel = new SiticonePanel
            {
                Size = new Size(1000, 80),
                Location = new Point(0, 0),
                FillColor = darkSecondary,
                BorderThickness = 0
            };

            // Logo/Title Label (Centered)
            SiticoneLabel titleLabel = new SiticoneLabel
            {
                Text = "⚡ ONE KICK HEROES HQ ⚡",
                Location = new Point(300, 20),
                Size = new Size(400, 40),
                ForeColor = accentGold,
                Font = new Font("Orbitron", 16, FontStyle.Bold),
                AutoSize = false
            };

            // Menu Toggle Button (Left Side)
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

            // Theme Toggle Button (Right Side with padding)
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

            headerPanel.Controls.Add(titleLabel);
            headerPanel.Controls.Add(menuToggleBtn);
            headerPanel.Controls.Add(themeToggleBtn);
            this.Controls.Add(headerPanel);
        }

        private void InitializeNavigation()
        {
            // Left Navigation Panel
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
            // Container panel for menu item
            SiticonePanel menuItemPanel = new SiticonePanel
            {
                Size = new Size(190, 55),
                Location = new Point(15, yPos),
                FillColor = darkBg,
                BorderThickness = 0,
                Parent = navPanel
            };

            // Icon label
            SiticoneLabel iconLabel = new SiticoneLabel
            {
                Text = icon,
                Location = new Point(12, 12),
                Size = new Size(30, 30),
                Font = new Font("Arial", 18, FontStyle.Bold),
                ForeColor = accentBlue,
                AutoSize = false
            };

            // Menu text label
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

            // Highlight for active state
            if (itemName == "Dashboard")
            {
                activeNavButton = new SiticoneButton { Tag = itemName }; // Reference for tracking
                menuItemPanel.BackColor = Color.FromArgb(40, 40, 80);
                iconLabel.ForeColor = accentGold;
                textLabel.ForeColor = accentGold;

                // Add left border accent
                Panel leftBorder = new Panel
                {
                    Size = new Size(4, 55),
                    Location = new Point(0, 0),
                    BackColor = accentGold
                };
                menuItemPanel.Controls.Add(leftBorder);
            }

            // Hover effects
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
            // Reset all menu items
            foreach (Control control in navPanel.Controls)
            {
                if (control is SiticonePanel panel && panel != menuPanel)
                {
                    panel.BackColor = darkBg;
                    foreach (Control child in panel.Controls)
                    {
                        if (child is SiticoneLabel lbl)
                        {
                            if (lbl == panel.Controls[0]) // Icon
                                lbl.ForeColor = accentBlue;
                            else // Text
                                lbl.ForeColor = Color.White;
                        }
                        else if (child is Panel border)
                        {
                            panel.Controls.Remove(border);
                        }
                    }
                }
            }

            // Highlight selected item
            menuPanel.BackColor = Color.FromArgb(40, 40, 80);
            iconLabel.ForeColor = accentGold;
            textLabel.ForeColor = accentGold;

            // Add left gold border
            Panel leftAccent = new Panel
            {
                Size = new Size(4, 55),
                Location = new Point(0, 0),
                BackColor = accentGold
            };
            menuPanel.Controls.Add(leftAccent);

            // Load content page
            LoadContentPage(itemName);

            // Auto-close side menu
            if (isNavOpen)
            {
                ToggleSideMenu();
            }
        }

        private void InitializeDashboardContent()
        {
            // Main Content Panel
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

            // Create instance of AddHeroForm
            AddHeroForm addHeroForm = new AddHeroForm
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            // Add it to content panel and show it
            contentPanel.Controls.Add(addHeroForm);
            addHeroForm.Show();
        }

        private void LoadViewAllHeroesForm()
        {
            contentPanel.Controls.Clear();

            // Create instance of AddHeroForm
            ViewAllHeroesForm viewAllHeroesForm = new ViewAllHeroesForm
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            // Add it to content panel and show it
            contentPanel.Controls.Add(viewAllHeroesForm);
            viewAllHeroesForm.Show();
        }

        private void LoadUpdateHeroForm()
        {
            contentPanel.Controls.Clear();

            // Create instance of AddHeroForm
            UpdateHeroForm updateHeroForm = new UpdateHeroForm
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            // Add it to content panel and show it
            contentPanel.Controls.Add(updateHeroForm);
            updateHeroForm.Show();
        }

        private void LoadDeleteHeroForm()
        {
            contentPanel.Controls.Clear();

            // Create instance of AddHeroForm
            DeleteHeroForm deleteHeroForm = new DeleteHeroForm
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            // Add it to content panel and show it
            contentPanel.Controls.Add(deleteHeroForm);
            deleteHeroForm.Show();
        }

        private void LoadReportsForm()
        {
            contentPanel.Controls.Clear();

            // Create instance of AddHeroForm
            ReportsForm reportsForm = new ReportsForm
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            // Add it to content panel and show it
            contentPanel.Controls.Add(reportsForm);
            reportsForm.Show();
        }

        private void LoadDashboardPage()
        {
            // Dashboard Title
            SiticoneLabel dashboardTitle = new SiticoneLabel
            {
                Text = "Dashboard Overview",
                Location = new Point(20, 20),
                Font = new Font("Orbitron", 16, FontStyle.Bold),
                ForeColor = accentGold,
                AutoSize = true
            };
            contentPanel.Controls.Add(dashboardTitle);

            // Summary Cards
            int[] stats = { 42, 18, 156, 38 };
            string[] cardTitles = { "Total Heroes", "Average Age", "Avg Score", "S-Rank Heroes" };
            Color[] cardColors = { accentGold, accentBlue, accentGreen, accentGray };

            int xPos = 20;
            for (int i = 0; i < 4; i++)
            {
                CreateSummaryCard(cardTitles[i], stats[i].ToString(), cardColors[i], xPos, 70);
                xPos += 185;
            }

            // Rank Distribution Panel
            CreateRankDistributionPanel(20, 190);

            // Hero Spotlight Panel
            CreateHeroSpotlightPanel(510, 190);

            // Footer info
            SiticoneLabel footerLbl = new SiticoneLabel
            {
                Text = "Last Updated: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                Location = new Point(20, 450),
                Font = new Font("Segoe UI", 8),
                ForeColor = accentGray,
                AutoSize = true
            };
            contentPanel.Controls.Add(footerLbl);
        }

        private void CreateSummaryCard(string title, string value, Color accentColor, int x, int y)
        {
            SiticonePanel card = new SiticonePanel
            {
                Size = new Size(170, 95),
                Location = new Point(x, y),
                FillColor = darkSecondary,
                BorderThickness = 2,
                Parent = contentPanel
            };

            SiticoneLabel cardTitle = new SiticoneLabel
            {
                Text = title,
                Location = new Point(12, 8),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.LightGray,
                AutoSize = true
            };

            SiticoneLabel cardValue = new SiticoneLabel
            {
                Text = value,
                Location = new Point(12, 32),
                Font = new Font("Orbitron", 18, FontStyle.Bold),
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

        private void CreateRankDistributionPanel(int x, int y)
        {
            SiticonePanel panel = new SiticonePanel
            {
                Size = new Size(460, 230),
                Location = new Point(x, y),
                FillColor = darkSecondary,
                BorderThickness = 1,
                Parent = contentPanel
            };

            SiticoneLabel title = new SiticoneLabel
            {
                Text = "Rank Distribution",
                Location = new Point(15, 12),
                Font = new Font("Orbitron", 11, FontStyle.Bold),
                ForeColor = accentBlue,
                AutoSize = true
            };
            panel.Controls.Add(title);

            string rankInfo = "S-Rank: 38 Heroes\nA-Rank: 156 Heroes\nB-Rank: 95 Heroes\nC-Rank: 42 Heroes";
            SiticoneLabel rankLabel = new SiticoneLabel
            {
                Text = rankInfo,
                Location = new Point(15, 40),
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.White,
                AutoSize = true
            };
            panel.Controls.Add(rankLabel);
        }

        private void CreateHeroSpotlightPanel(int x, int y)
        {
            SiticonePanel panel = new SiticonePanel
            {
                Size = new Size(460, 230),
                Location = new Point(x, y),
                FillColor = darkSecondary,
                BorderThickness = 1,
                Parent = contentPanel
            };

            SiticoneLabel title = new SiticoneLabel
            {
                Text = "⭐ Top S-Rank Heroes",
                Location = new Point(15, 12),
                Font = new Font("Orbitron", 11, FontStyle.Bold),
                ForeColor = accentGold,
                AutoSize = true
            };
            panel.Controls.Add(title);

            string[] topHeroes = { "Saitama", "Genos", "Mumen Rider" };
            int heroY = 45;

            foreach (string hero in topHeroes)
            {
                SiticoneLabel heroLabel = new SiticoneLabel
                {
                    Text = "🦸 " + hero,
                    Location = new Point(15, heroY),
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    ForeColor = accentGold,
                    AutoSize = true
                };
                panel.Controls.Add(heroLabel);
                heroY += 55;
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