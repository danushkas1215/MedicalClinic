﻿using System;
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
    public partial class PharmaceuticalComList : Form
    {
        public PharmaceuticalComList()
        {
            InitializeComponent();
            GetData();
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
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\dsi.SL\\Documents\\Visual Studio 2017\\Projects\\MedicalClinic\\MedicalClinic\\MedicalClinic.accdb");
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
