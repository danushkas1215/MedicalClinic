using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace MedicalClinic
{
    public partial class GenericNamesList : Form
    {
        public GenericNamesList()
        {
            InitializeComponent();
            GetData();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("GenericName LIKE '%{0}%'", txtSearch.Text);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["edit_column"].Index)
            {
                string intID = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                GenericNames myform = new GenericNames(intID);
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
            con.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("select * from GenericNames", con);
            DataSet ds = new DataSet();
            da.Fill(ds, "GenericNames");
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns["ID"].Visible = false;
            dataGridView1.Columns["GenericName"].Width = 500;
            dataGridView1.Columns["GenericName"].HeaderText = "Generic Name";

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
