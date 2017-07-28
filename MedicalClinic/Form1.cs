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

        private void genericNamesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
                ActiveMdiChild.Close();
            GenericNames newMDIChild = new GenericNames(string.Empty);
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
            newMDIChild.WindowState = FormWindowState.Maximized;
        }

        private void genericNamesListToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
                ActiveMdiChild.Close();
            GenericNamesList newMDIChild = new GenericNamesList();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
            newMDIChild.WindowState = FormWindowState.Maximized;
        }

        private void companyListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
                ActiveMdiChild.Close();
            PharmaceuticalComList newMDIChild = new PharmaceuticalComList();
            newMDIChild.MdiParent = this;
            newMDIChild.MaximizeBox = false;
            newMDIChild.Show();
            newMDIChild.WindowState = FormWindowState.Maximized;
        }

        private void companyToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
                ActiveMdiChild.Close();
            PharmaceuticalCom newMDIChild = new PharmaceuticalCom(string.Empty);
            newMDIChild.MdiParent = this;
            newMDIChild.MaximizeBox = false;
            newMDIChild.Show();
            newMDIChild.WindowState = FormWindowState.Maximized;
        }
    }
}
