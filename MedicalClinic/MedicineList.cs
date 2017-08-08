using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            this.dataGridView1.Columns["Medicine Name"].Width = 220;
            this.dataGridView1.Columns["Generic Name"].Width = 175;
            this.dataGridView1.Columns["Company"].Width = 150;
            this.dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
            this.dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10.0F, FontStyle.Bold);
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("MedicineName LIKE '%{0}%'", txtSearch.Text);
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
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\dsi.SL\\Documents\\Visual Studio 2017\\Projects\\MedicalClinic\\MedicalClinic\\MedicalClinic.accdb");
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandText = "select ID, MedicineName, MedGenericNameID, MedCompanyID from Medicines";
            con.Open();
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ID");
                dt.Columns.Add("Medicine Name");
                dt.Columns.Add("Generic Name");
                dt.Columns.Add("Company");
                while (reader.Read())
                {
                    DataRow row = dt.NewRow();
                    row["ID"] = reader["ID"];
                    row["Medicine Name"] = reader["MedicineName"];
                    row["Generic Name"] = GetGenericName(reader["MedGenericNameID"].ToString());
                    row["Company"] = GetCompanyName(reader["MedCompanyID"].ToString());
                    dt.Rows.Add(row);
                }
                dataGridView1.DataSource = dt;
            }
            reader.Close();
            con.Close();
            //DataSet ds = new DataSet();
            //da.Fill(ds, "Medicines");
            //dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns["ID"].Visible = false;
            //dataGridView1.Columns["MedicineName"].Width = 200;
            //dataGridView1.Columns["MedicineName"].HeaderText = "Medicine Name";
            //dataGridView1.Columns["MedGenericNameID"].HeaderText = "Generic Name";
            //dataGridView1.Columns["MedCompanyID"].HeaderText = "Company";

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

        public string GetGenericName(string strGenericId)
        {
            string strGenericName = string.Empty;
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\dsi.SL\\Documents\\Visual Studio 2017\\Projects\\MedicalClinic\\MedicalClinic\\MedicalClinic.accdb");
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandText = "select * from GenericNames where ID = " + strGenericId + "";
            con.Open();
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    strGenericName = reader["GenericName"].ToString();
                }
            }
            reader.Close();
            con.Close();

            return strGenericName;
        }

        public string GetCompanyName(string strCompanyId)
        {
            string strCompanyName = string.Empty;
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\dsi.SL\\Documents\\Visual Studio 2017\\Projects\\MedicalClinic\\MedicalClinic\\MedicalClinic.accdb");
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandText = "select * from PharmaceuticalCompanies where ID = " + strCompanyId + "";
            con.Open();
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    strCompanyName = reader["CompanyName"].ToString();
                }
            }
            reader.Close();
            con.Close();

            return strCompanyName;
        }
    }
}
