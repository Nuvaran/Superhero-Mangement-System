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

namespace Superhero_Mangement_System.PresentationLayer.Forms
{
    public partial class GetID : Form
    {
        public int EnteredID { get; private set; }

        public GetID()
        {
            InitializeComponent();
            
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void GetID_Load(object sender, EventArgs e)
        {
            IDInput.Maximum = FileHandler.GetNextAvailableId()-1;
            IDInput.Minimum = 1;
            lblError.Visible = false;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            bool ValidID = false;
            foreach (int ID in FileHandler.GetAllHeroIds())
            {
                if (IDInput.Value == ID) ValidID = true;
            }
            if (ValidID)
            {
                this.EnteredID = (int)IDInput.Value;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                lblError.Visible = true;
                
            }
        }
    }
}
