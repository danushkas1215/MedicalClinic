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
            if (ValidateForm())
            {
                // AddPresentComplaint();
                //AddPrescriptionData();
                //ReducePrescriptionData();
                MessageBox.Show("Record Successfully Saved", "Message");
            }
        }

        private void AddComboboxColumn()
        {
            string conString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            OleDbConnection con = new OleDbConnection(conString);
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandText = "select * from Medicines";
            con.Open();
            OleDbDataReader reader = cmd.ExecuteReader();
            //dataGridView1.DataSource = ds.Tables[0];
            DataTable data = new DataTable();
            data.Columns.Add(new DataColumn("ID", typeof(string)));
            data.Columns.Add(new DataColumn("MedicineName", typeof(string)));
            //data = ds.Tables[0];

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    DataRow row = data.NewRow();
                    string strTradeName = string.Empty;
                    strTradeName = reader["MedTradeName"].ToString();

                    if (string.IsNullOrEmpty(strTradeName))
                    {
                        data.Rows.Add(reader["ID"], reader["MedicineName"]);
                    }
                    else
                    {
                        data.Rows.Add(reader["ID"], reader["MedicineName"] + " - (" + reader["MedTradeName"] + ")");
                    }
                    
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
            ColComboBox.DisplayMember = "MedicineName";
            ColComboBox.ValueMember = "ID";
            ColComboBox.Name = "ID";
            ColComboBox.DataPropertyName = "ID";

            dataGridViewPrescription.ColumnCount = 4;
            dataGridViewPrescription.Columns[1].Name = "Times";
            dataGridViewPrescription.Columns[2].Name = "Qty";
            dataGridViewPrescription.Columns[3].Name = "Days";
        }

        public void AddPrescriptionData()
        {
            string conString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            OleDbConnection con = new OleDbConnection(conString);
            OleDbCommand cmd = con.CreateCommand();
            con.Open();
            foreach (DataGridViewRow ro in dataGridViewPrescription.Rows)
            {
                if (Convert.ToString(ro.Cells[0].Value) != string.Empty)
                {
                    cmd.CommandText = "INSERT INTO PatientPrescription(PatientID, MedicineID, MedicineTimes, MedicineQuantity, MedicineDays)VALUES("
                    + int.Parse(txtID.Text) + ","
                    + ro.Cells[0].Value + ",'"
                    + ro.Cells[1].Value + "',"
                    + ro.Cells[2].Value + ","
                    + ro.Cells[3].Value + ")";
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                }
            }
            con.Close();
        }

        public void ReducePrescriptionData()
        {
            string conString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            OleDbConnection con = new OleDbConnection(conString);
            OleDbCommand cmd = con.CreateCommand();
            con.Open();
            foreach (DataGridViewRow ro in dataGridViewPrescription.Rows)
            {
                if (Convert.ToString(ro.Cells[0].Value) != string.Empty)
                {
                    cmd.CommandText = "SELECT * FROM Medicines WHERE ID = "+ ro.Cells[0].Value + "";
                    OleDbDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int intTotalMedicines = int.Parse(reader["MedUnits"].ToString());
                            int intBalanceMedicines = intTotalMedicines - int.Parse(ro.Cells[2].Value.ToString());

                            string strUpdate = "UPDATE Medicines SET MedUnits=" + intBalanceMedicines + " WHERE ID=" + ro.Cells[0].Value + "";
                            OleDbCommand cmdUpdate = new OleDbCommand(strUpdate, con);
                            cmdUpdate.ExecuteNonQuery();
                        }
                    }
                    reader.Close();
                }
            }
            con.Close();
        }

        public Boolean ValidateForm()
        {
            bool isValidated = false;
            string conString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            OleDbConnection con = new OleDbConnection(conString);
            OleDbCommand cmd = con.CreateCommand();
            con.Open();
            foreach (DataGridViewRow ro in dataGridViewPrescription.Rows)
            {
                if (Convert.ToString(ro.Cells[2].Value) != string.Empty)
                {
                    cmd.CommandText = "SELECT * FROM Medicines WHERE ID = " + ro.Cells[0].Value + "";
                    OleDbDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int intTotalMedicines = int.Parse(reader["MedUnits"].ToString());
                            int intBalanceMedicines = intTotalMedicines - int.Parse(ro.Cells[2].Value.ToString());
                            if (intBalanceMedicines <= 0)
                            {
                                MessageBox.Show(reader["MedicineName"].ToString() + " does not have enough medicines to issue", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                goto endOfLoop;
                            }
                            else
                            {
                                isValidated = true;
                            }
                        }
                    }
                    reader.Close();
                }
            }
            endOfLoop:
            con.Close();

            return isValidated;
        }

        private void dataGridViewPrescription_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            dataGridViewPrescription.Rows[e.RowIndex].ErrorText = string.Empty;
            DataGridViewRow row = dataGridViewPrescription.Rows[e.RowIndex];
            row.Cells[dataGridViewPrescription.Columns[0].Index].ErrorText = string.Empty;
            row.Cells[dataGridViewPrescription.Columns[1].Index].ErrorText = string.Empty;
            row.Cells[dataGridViewPrescription.Columns[2].Index].ErrorText = string.Empty;
            row.Cells[dataGridViewPrescription.Columns[3].Index].ErrorText = string.Empty;
            DataGridViewCell medicineNameCell = row.Cells[dataGridViewPrescription.Columns[0].Index];
            DataGridViewCell timeCell = row.Cells[dataGridViewPrescription.Columns[1].Index];
            DataGridViewCell quantityCell = row.Cells[dataGridViewPrescription.Columns[2].Index];
            DataGridViewCell daysCell = row.Cells[dataGridViewPrescription.Columns[3].Index];
            e.Cancel = !(IsMedicineEmpty(medicineNameCell) && IsTimesEmpty(timeCell) && IsQuantityEmpty(quantityCell) && IsDaysEmpty(daysCell));
        }

        private Boolean IsMedicineEmpty(DataGridViewCell cell)
        {
            if (cell.Value == null)
            {
                cell.ErrorText = "Please enter a value";
                dataGridViewPrescription.Rows[cell.RowIndex].ErrorText = "Please enter a value";
                return false;
            }
            return true;
        }

        private Boolean IsTimesEmpty(DataGridViewCell cell)
        {
            dataGridViewPrescription.Rows[cell.RowIndex].ErrorText = string.Empty;
            if (cell.Value == null)
            {
                cell.ErrorText = "Please enter a value";
                dataGridViewPrescription.Rows[cell.RowIndex].ErrorText = "Please enter a value";
                return false;
            }
            return true;
        }

        private Boolean IsQuantityEmpty(DataGridViewCell cell)
        {
            if (cell.Value == null)
            {
                cell.ErrorText = "Please enter a value";
                dataGridViewPrescription.Rows[cell.RowIndex].ErrorText = "Please enter a value";
                return false;
            }
            return true;
        }

        private Boolean IsDaysEmpty(DataGridViewCell cell)
        {
            if (cell.Value == null)
            {
                cell.ErrorText = "Please enter a value";
                dataGridViewPrescription.Rows[cell.RowIndex].ErrorText = "Please enter a value";
                return false;
            }
            return true;
        }
    }
}
