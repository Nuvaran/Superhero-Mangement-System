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

namespace Superhero_Mangement_System
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            FileHandler.CreateTextFile();
            FileHandler.AddNewHero(new string[] { "Maritn", "14", "Flight", "50" });
        }
    }
}
