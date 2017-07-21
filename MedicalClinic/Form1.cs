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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Patients newMDIChild = new Patients();
            // Set the Parent Form of the Child window.  
            newMDIChild.MdiParent = this;
            newMDIChild.MaximizeBox = false;
            newMDIChild.WindowState = FormWindowState.Maximized;
            // Display the new form.  
            newMDIChild.Show();
        }
    }
}
