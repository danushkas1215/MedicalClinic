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
    public partial class PatientMedicalRecord : Form
    {
        public PatientMedicalRecord()
        {
            InitializeComponent();
            AddComboboxColumn();
        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {
            string conString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            OleDbConnection con = new OleDbConnection(conString);
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandText = "select * from Patients where ID = " + txtID.Text + "";
            con.Open();
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    txtName.Text = reader["FullName"].ToString();
                    txtDoB.Text = DateTime.Parse( reader["Birthday"].ToString()).ToString("dd/MMM/yyyy");
                    txtMaritalStatus.Text = reader["MaritalStatus"].ToString();
                    txtSex.Text = reader["Sex"].ToString();
                    txtOccupation.Text = reader["Birthday"].ToString();
                    txtNic.Text = reader["NIC"].ToString();
                    txtContactNo.Text = reader["Mobile"].ToString();
                }
            }
            reader.Close();
            con.Close();
        }

        public void AddPresentComplaint()
        {
            string conString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            OleDbConnection con = new OleDbConnection(conString);
            OleDbCommand cmd = con.CreateCommand();
            con.Open();
            cmd.CommandText = "INSERT INTO PatientPresentComplaint(PatientID, HistoryOfPresentComplaint, ExaminationFindings," +
                "RelevantInvestigation, ProblemListCurrent)VALUES(" + txtID.Text + ",'" + txtHistoryOfPresentComplaint.Text + "'," +
                "'" + txtExaminationFindings.Text + "', '" + txtRelevantInvestigation.Text + "'," +
                "'" + txtProblemListCurrent.Text + "')";
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            cmd.CommandText = "Select @@Identity";
            int intId = (int)cmd.ExecuteScalar();
            con.Close();
            LoggingHelper.LogEntry("Patient Present Complaint", "Add", txtID.Text + "|" + txtHistoryOfPresentComplaint.Text + "|" +
                txtExaminationFindings.Text + "|" + txtRelevantInvestigation.Text + "|" + txtProblemListCurrent.Text, intId);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            AddPresentComplaint();
            MessageBox.Show("Record Successfully Saved", "Message");
        }

        private void AddComboboxColumn()
        {
            string conString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            OleDbConnection con = new OleDbConnection(conString);
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandText = "select * from GenericNames";
            con.Open();
            OleDbDataReader reader = cmd.ExecuteReader();
            //dataGridView1.DataSource = ds.Tables[0];
            DataTable data = new DataTable();
            data.Columns.Add(new DataColumn("ID", typeof(string)));
            data.Columns.Add(new DataColumn("GenericName", typeof(string)));
            //data = ds.Tables[0];

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    DataRow row = data.NewRow();
                    data.Rows.Add(reader["ID"], reader["GenericName"]);
                }
            }

            DataGridViewComboBoxColumn ColComboBox = new DataGridViewComboBoxColumn();
            dataGridViewPrescription.Columns.Add(ColComboBox);
            ColComboBox.DataPropertyName = "ID";
            ColComboBox.HeaderText = "Medicine Name";
            ColComboBox.ValueType = typeof(string);
            ColComboBox.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
            //ColComboBox.DisplayIndex = 2;
            ColComboBox.Width = 530;
            ColComboBox.DataSource = data;
            ColComboBox.DisplayMember = "GenericName";
            ColComboBox.ValueMember = "ID";
            ColComboBox.Name = "ID";
            ColComboBox.DataPropertyName = "ID";

            dataGridViewPrescription.ColumnCount = 4;
            dataGridViewPrescription.Columns[1].Name = "Times";
            dataGridViewPrescription.Columns[2].Name = "Qty";
            dataGridViewPrescription.Columns[3].Name = "Days";
        }
    }
}
