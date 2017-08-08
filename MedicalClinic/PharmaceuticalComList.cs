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
    public partial class PharmaceuticalComList : Form
    {
        public PharmaceuticalComList()
        {
            InitializeComponent();
            GetData();

            this.dataGridView1.RowsDefaultCellStyle.BackColor = Color.LightBlue;
            this.dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
            this.dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
            this.dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10.0F, FontStyle.Bold);
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("CompanyName LIKE '%{0}%'", txtSearch.Text);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["edit_column"].Index)
            {
                string intID = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                PharmaceuticalCom myform = new PharmaceuticalCom(intID);
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
            con.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("select ID, CompanyName from PharmaceuticalCompanies", con);
            DataSet ds = new DataSet();
            da.Fill(ds, "PharmaceuticalCompanies");
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns["ID"].Visible = false;
            dataGridView1.Columns["CompanyName"].Width = 500;
            dataGridView1.Columns["CompanyName"].HeaderText = "Company Name";

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

            ds.Tables.Clear();
            con.Close();
        }
    }
}
