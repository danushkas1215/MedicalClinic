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
            this.WindowState = FormWindowState.Maximized;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
                ActiveMdiChild.Close();
            Patients newMDIChild = new Patients(string.Empty);
            newMDIChild.MdiParent = this;
            newMDIChild.MaximizeBox = false;
            newMDIChild.WindowState = FormWindowState.Maximized;
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
            newMDIChild.WindowState = FormWindowState.Maximized;
            newMDIChild.Show();
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

        private void medicineListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
                ActiveMdiChild.Close();
            MedicineList newMDIChild = new MedicineList();
            newMDIChild.MdiParent = this;
            newMDIChild.MaximizeBox = false;
            newMDIChild.Show();
            newMDIChild.WindowState = FormWindowState.Maximized;
        }

        private void medicineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
                ActiveMdiChild.Close();
            Medicines newMDIChild = new Medicines(string.Empty);
            newMDIChild.MdiParent = this;
            newMDIChild.MaximizeBox = false;
            newMDIChild.Show();
            newMDIChild.WindowState = FormWindowState.Maximized;
        }

        private void patientToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
                ActiveMdiChild.Close();
            Patients newMDIChild = new Patients(string.Empty);
            newMDIChild.MdiParent = this;
            newMDIChild.MaximizeBox = false;
            newMDIChild.WindowState = FormWindowState.Maximized;
            newMDIChild.Show();
        }

        private void medicalRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
                ActiveMdiChild.Close();
            PatientMedicalRecord newMDIChild = new PatientMedicalRecord(string.Empty);
            newMDIChild.MdiParent = this;
            newMDIChild.MaximizeBox = false;
            newMDIChild.WindowState = FormWindowState.Maximized;
            newMDIChild.Show();
        }

        private void patientsListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
                ActiveMdiChild.Close();
            PatientsList newMDIChild = new PatientsList();
            newMDIChild.MdiParent = this;
            newMDIChild.MaximizeBox = false;
            newMDIChild.WindowState = FormWindowState.Maximized;
            newMDIChild.Show();
        }

        private void doctorViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
                ActiveMdiChild.Close();
            ViewDoctor newMDIChild = new ViewDoctor();
            newMDIChild.MdiParent = this;
            newMDIChild.MaximizeBox = false;
            newMDIChild.WindowState = FormWindowState.Maximized;
            newMDIChild.Show();
        }

        //private void medicineOrderToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (ActiveMdiChild != null)
        //        ActiveMdiChild.Close();
        //    MedicineOrders newMDIChild = new MedicineOrders();
        //    newMDIChild.MdiParent = this;
        //    newMDIChild.MaximizeBox = false;
        //    newMDIChild.Show();
        //    newMDIChild.WindowState = FormWindowState.Maximized;
        //}
    }
}
