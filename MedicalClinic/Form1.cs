﻿using System;
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

        private void companyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PharmaceuticalCom newMDIChild = new PharmaceuticalCom();
            newMDIChild.MdiParent = this;
            newMDIChild.MaximizeBox = false;
            newMDIChild.WindowState = FormWindowState.Maximized;
            newMDIChild.Show();
        }

        private void genericNamesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenericNames newMDIChild = new GenericNames();
            newMDIChild.MdiParent = this;
            newMDIChild.WindowState = FormWindowState.Maximized;
            newMDIChild.Show();
        }

        private void genericNamesListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenericNamesList newMDIChild = new GenericNamesList();
            newMDIChild.MdiParent = this;
            newMDIChild.WindowState = FormWindowState.Maximized;
            newMDIChild.Show();
        }
    }
}
