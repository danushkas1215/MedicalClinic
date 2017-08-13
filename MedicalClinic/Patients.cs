using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalClinic
{
    public partial class Patients : Form
    {
        public Patients()
        {
            InitializeComponent();
            comBloodGroup.SelectedIndex = 0;
            comSex.SelectedIndex = 0;
        }

        private void txtHomePhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Add)))
                e.Handled = true;
        }

        private void txtBirthday_ValueChanged(object sender, EventArgs e)
        {
            TimeSpan ts = DateTime.Now - Convert.ToDateTime(txtBirthday.Value);
            int intAge = Convert.ToInt32(ts.Days) / 365;
            txtAge.Text = intAge.ToString();
        }

        private void txtBirthday_DropDown(object sender, EventArgs e)
        {
            txtBirthday.ValueChanged -= txtBirthday_ValueChanged;
        }

        private void txtBirthday_CloseUp(object sender, EventArgs e)
        {
            txtBirthday.ValueChanged += txtBirthday_ValueChanged;
            txtBirthday_ValueChanged(sender, e);
        }
    }
}
