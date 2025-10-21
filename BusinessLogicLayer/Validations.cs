using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Superhero_Mangement_System.BusinessLogicLayer
{
    public class Validations
    {
        /** 
         * Validate inputs for superhero details.
         *  Ensures that all fields are filled and that age and score are numeric.
         */
        public bool ValidateHeroInputs(TextBox id, TextBox name, TextBox age, TextBox power, TextBox score)
        {
            if (string.IsNullOrWhiteSpace(id.Text) ||
                string.IsNullOrWhiteSpace(name.Text) ||
                string.IsNullOrWhiteSpace(age.Text) ||
                string.IsNullOrWhiteSpace(power.Text) ||
                string.IsNullOrWhiteSpace(score.Text))
            {
                MessageBox.Show("All fields are required!");
                return false;
            }

            if (!int.TryParse(age.Text, out _) || !int.TryParse(score.Text, out _))
            {
                MessageBox.Show("Age and Score must be numbers!");
                return false;
            }

            return true;
        }
    }
}
