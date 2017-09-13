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
    public partial class ViewNurse : Form
    {
        public ViewNurse()
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["edit_column"].Index)
            {
                string intID = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() +"|"+ dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                PatientMedicalRecord myform = new PatientMedicalRecord(intID);
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
            cmd.CommandText = "SELECT ID, PatientID, LogDate FROM PatientsArrival WHERE PatientStatus = 'Consulted' ORDER BY LogDate";
            con.Open();
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ID");
                dt.Columns.Add("PatientID");
                dt.Columns.Add("PatientName");
                dt.Columns.Add("LogDate");
                while (reader.Read())
                {
                    DataRow row = dt.NewRow();
                    row["ID"] = reader["ID"];
                    row["PatientID"] = reader["PatientID"];
                    row["PatientName"] = GetPatientName(reader["PatientID"].ToString());
                    DateTime dtLogDate = DateTime.Parse(reader["LogDate"].ToString());
                    row["LogDate"] = dtLogDate.ToString("dd/MM/yyyy hh:mm:ss tt");
                    dt.Rows.Add(row);
                }
                dataGridView1.DataSource = dt;
            }
            reader.Close();
            con.Close();

            DataGridViewButtonColumn btnEditButtonColumn = new DataGridViewButtonColumn();
            btnEditButtonColumn.Name = "edit_column";
            btnEditButtonColumn.Text = "View";
            btnEditButtonColumn.HeaderText = "ww";
            btnEditButtonColumn.UseColumnTextForButtonValue = true;
            btnEditButtonColumn.Width = 50;
            int columnIndex = 1;
            if (dataGridView1.Columns["edit_column"] == null)
            {
                dataGridView1.Columns.Insert(columnIndex, btnEditButtonColumn);
            }
            dataGridView1.CellClick += dataGridView1_CellClick;

            dataGridView1.Columns["ID"].Visible = false;
            dataGridView1.Columns["PatientID"].Visible = false;
            dataGridView1.Columns["LogDate"].Width = 170;
            dataGridView1.Columns["LogDate"].HeaderText = "Log Time";
            dataGridView1.Columns["PatientName"].Width = 500;
            dataGridView1.Columns["PatientName"].HeaderText = "Log Time";

            con.Close();
        }

        public string GetPatientName(string strPatientId)
        {
            string strPatientName = string.Empty;
            string conString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            OleDbConnection con = new OleDbConnection(conString);
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandText = "select * from Patients where ID = " + strPatientId + "";
            con.Open();
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    strPatientName = reader["FullName"].ToString();
                }
            }
            reader.Close();
            con.Close();

            return strPatientName;
        }
    }
}
