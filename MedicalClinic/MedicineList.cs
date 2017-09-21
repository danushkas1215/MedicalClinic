using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalClinic
{
    public partial class MedicineList : Form
    {
        public MedicineList()
        {
            InitializeComponent();
            GetData();

            this.dataGridView1.RowsDefaultCellStyle.BackColor = Color.LightBlue;
            this.dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
            this.dataGridView1.Columns["MedicineName"].Width = 250;
            this.dataGridView1.Columns["MedicineName"].HeaderText = "Medicine Name";
            this.dataGridView1.Columns["Generic Name"].Width = 400;
            this.dataGridView1.Columns["Company"].Width = 300;
            this.dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
            this.dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10.0F, FontStyle.Bold);
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["edit_column"].Index)
            {
                string intID = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                Medicines myform = new Medicines(intID);
                myform.MdiParent = this.ParentForm;
                myform.WindowState = FormWindowState.Maximized;
                myform.Show();
                this.Close();
            }
        }

        public void GetData()
        {
            string conString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            OleDbConnection con = new OleDbConnection(conString);
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT Medicines.ID, Medicines.MedicineName, Medicines.MedGenericNameID, Medicines.MedCompanyID, GenericNames.GenericName, PharmaceuticalCompanies.CompanyName " +
                "FROM(Medicines INNER JOIN GenericNames ON Medicines.MedGenericNameID = GenericNames.ID) INNER JOIN PharmaceuticalCompanies ON Medicines.MedCompanyID = PharmaceuticalCompanies.ID " +
                "ORDER BY Medicines.MedicineName";
            con.Open();
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ID");
                dt.Columns.Add("MedicineName");
                dt.Columns.Add("Generic Name");
                dt.Columns.Add("Company");
                while (reader.Read())
                {
                    DataRow row = dt.NewRow();
                    row["ID"] = reader["ID"];
                    row["MedicineName"] = reader["MedicineName"];
                    row["Generic Name"] = reader["GenericName"];
                    row["Company"] = reader["CompanyName"];
                    dt.Rows.Add(row);
                }
                dataGridView1.DataSource = dt;
            }
            reader.Close();
            con.Close();
            dataGridView1.Columns["ID"].Visible = false;

            DataGridViewButtonColumn btnEditButtonColumn = new DataGridViewButtonColumn();
            btnEditButtonColumn.Name = "edit_column";
            btnEditButtonColumn.Text = "Edit";
            btnEditButtonColumn.HeaderText = "";
            btnEditButtonColumn.UseColumnTextForButtonValue = true;
            btnEditButtonColumn.Width = 50;
            int columnIndex = 1;
            if (dataGridView1.Columns["edit_column"] == null)
            {
                dataGridView1.Columns.Insert(columnIndex, btnEditButtonColumn);
            }
            dataGridView1.CellClick += dataGridView1_CellClick;

            //ds.Tables.Clear();
            con.Close();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("MedicineName LIKE '%{0}%'", txtSearch.Text);
        }
    }
}
