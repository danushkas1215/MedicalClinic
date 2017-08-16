using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MedicalClinic
{
    public partial class MedicineOrders : Form
    {
        DataTable dataCompany = new DataTable();
        DataGridViewComboBoxColumn cmbCompany = new DataGridViewComboBoxColumn();

        public MedicineOrders()
        {
            InitializeComponent();
            InitiateDataGrid();
        }

        public void InitiateDataGrid()
        {
            string conString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            OleDbConnection con = new OleDbConnection(conString);
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandText = "select ID, DistributorName from MedicalDistributors";
            con.Open();
            OleDbDataReader reader = cmd.ExecuteReader();

            DataGridViewComboBoxColumn cmbDistributor = new DataGridViewComboBoxColumn();
            DataTable data = new DataTable();

            data.Columns.Add(new DataColumn("Value", typeof(string)));
            data.Columns.Add(new DataColumn("Description", typeof(string)));
            cmbDistributor.HeaderText = "Distributor";
            cmbDistributor.Name = "Distributor";

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    DataRow row = data.NewRow();
                    data.Rows.Add(reader["ID"], reader["DistributorName"]);
                }
                cmbDistributor.DataSource = data;
                cmbDistributor.ValueMember = "Value";
                cmbDistributor.DisplayMember = "Description";
            }

            dataGridView1.Columns.Add(cmbDistributor);
            con.Close();

            cmd.CommandText = "SELECT ID, CompanyName FROM PharmaceuticalCompanies";
            con.Open();
            reader = cmd.ExecuteReader();

            DataGridViewComboBoxColumn cmbCompany = new DataGridViewComboBoxColumn();
            DataTable dataCompany = new DataTable();

            dataCompany.Columns.Add(new DataColumn("Value", typeof(string)));
            dataCompany.Columns.Add(new DataColumn("Description", typeof(string)));
            cmbCompany.HeaderText = "Company";
            cmbCompany.Name = "Company";

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    DataRow row = dataCompany.NewRow();
                    dataCompany.Rows.Add(reader["ID"], reader["CompanyName"]);
                }
                cmbCompany.DataSource = dataCompany;
                cmbCompany.ValueMember = "Value";
                cmbCompany.DisplayMember = "Description";
            }

            dataGridView1.Columns.Add(cmbCompany);
            con.Close();

            cmd.CommandText = "SELECT ID, GenericName FROM GenericNames";
            con.Open();
            reader = cmd.ExecuteReader();

            DataGridViewComboBoxColumn cmbGenerics = new DataGridViewComboBoxColumn();
            DataTable dataGenerics = new DataTable();

            dataGenerics.Columns.Add(new DataColumn("Value", typeof(string)));
            dataGenerics.Columns.Add(new DataColumn("Description", typeof(string)));
            cmbGenerics.HeaderText = "Generic Names";
            cmbGenerics.Name = "GenericNames";

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    DataRow row = dataGenerics.NewRow();
                    dataGenerics.Rows.Add(reader["ID"], reader["GenericName"]);
                }
                cmbGenerics.DataSource = dataGenerics;
                cmbGenerics.ValueMember = "Value";
                cmbGenerics.DisplayMember = "Description";
            }

            dataGridView1.Columns.Add(cmbGenerics);
            con.Close();

            //dataGridView1.CellValueChanged += new DataGridViewCellEventHandler(dataGridView1_CellValueChanged);
            //dataGridView1.CurrentCellDirtyStateChanged += new EventHandler(dataGridView1_CurrentCellDirtyStateChanged);

        }

        void dataGridView1_CurrentCellDirtyStateChanged(object sender,
            EventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 0)
            {
                DataGridViewComboBoxCell cmbDistributorSelected = (DataGridViewComboBoxCell)dataGridView1.Rows[e.RowIndex].Cells[0];
                if (cmbDistributorSelected.Value != null)
                {
                    MessageBox.Show(cmbDistributorSelected.Value.ToString() + "|");
                    string conString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
                    OleDbConnection con = new OleDbConnection(conString);
                    OleDbCommand cmd = con.CreateCommand();
                    cmd.CommandText = "SELECT ID, CompanyName FROM PharmaceuticalCompanies WHERE DisCompanyID = "+ int.Parse(cmbDistributorSelected.Value.ToString()) + "";
                    con.Open();
                    OleDbDataReader reader = cmd.ExecuteReader();

                    //DataGridViewComboBoxColumn cmbCompany = new DataGridViewComboBoxColumn();
                    //DataTable dataCompany = new DataTable();

                    //dataCompany.Columns.Add(new DataColumn("Value", typeof(string)));
                    //dataCompany.Columns.Add(new DataColumn("Description", typeof(string)));
                    //cmbCompany.HeaderText = "Company";
                    //cmbCompany.Name = "Company";

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            DataRow row = dataCompany.NewRow();
                            dataCompany.Rows.Add(reader["ID"], reader["CompanyName"]);
                        }
                        cmbCompany.DataSource = dataCompany;
                        cmbCompany.ValueMember = "Value";
                        cmbCompany.DisplayMember = "Description";
                    }

                    dataGridView1.Columns.Remove(cmbCompany);
                    dataGridView1.Columns.Add(cmbCompany);
                    con.Close();
                    dataGridView1.Invalidate();
                }
            }

            if (dataGridView1.CurrentCell.ColumnIndex == 1)
            {
                DataGridViewComboBoxCell cmbCompanySelected = (DataGridViewComboBoxCell)dataGridView1.Rows[e.RowIndex].Cells[1];
                if (cmbCompanySelected.Value != null)
                {
                    MessageBox.Show(cmbCompanySelected.Value.ToString() + "|");

                    dataGridView1.Invalidate();
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            printDialog1.Document = printDocument1;

            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Testing", new Font("Arial", 20, FontStyle.Bold), Brushes.Black, 100, 20);
            e.Graphics.PageUnit = GraphicsUnit.Inch;
        }
    }
}
