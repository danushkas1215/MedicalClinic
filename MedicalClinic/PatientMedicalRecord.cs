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

            RetrievePastMedicalHistory();
            RetrieveFamilyHistory();
            RetrieveAllergyHistory();
            RetrieveSocialHistory();
            RetrieveDrugHistory();
            RetrieveVaccinationHistory();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                AddPresentComplaint();
                AddPrescriptionData();
                ReducePrescriptionData();
                AddUpdatePastMedicalHistory();
                AddUpdateFamilyHistory();
                AddUpdateAllergyHistory();
                AddUpdateSocialHistory();
                AddUpdateDrugHistory();
                AddUpdateVaccinationHistory();
                MessageBox.Show("Record Successfully Saved", "Message");
            }
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

#region Past Medical History
        public void AddUpdatePastMedicalHistory()
        {
            string conString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            OleDbConnection con = new OleDbConnection(conString);
            con.Open();
            foreach (DataGridViewRow ro in dataGridViewPastMedicalHistory.Rows)
            {
                if (Convert.ToString(ro.Cells[0].Value) != string.Empty)
                {
                    string strUpdate = "UPDATE PatientPastMedicalHistory SET Disease = '" + ro.Cells[1].Value.ToString().Trim() + "', DiagnosedYear = " + ro.Cells[2].Value.ToString().Trim() + " " +
                        " WHERE ID = " + ro.Cells[0].Value.ToString().Trim() + "";
                    OleDbCommand cmdUpdate = new OleDbCommand(strUpdate, con);
                    cmdUpdate.ExecuteNonQuery();

                    LoggingHelper.LogEntry("Patient Past Medical History", "Update",
                        txtID.Text + "|" + ro.Cells[1].Value.ToString().Trim() + "|" + ro.Cells[2].Value.ToString().Trim(), int.Parse(ro.Cells[0].Value.ToString().Trim()));
                }
                else
                {
                    if (Convert.ToString(ro.Cells[1].Value) != string.Empty)
                    {
                        OleDbCommand cmd = con.CreateCommand();
                        cmd.CommandText = "INSERT INTO PatientPastMedicalHistory(PatientID, Disease, DiagnosedYear)VALUES("
                        + int.Parse(txtID.Text) + ",'"
                        + ro.Cells[1].Value + "',"
                        + ro.Cells[2].Value + ")";
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "Select @@Identity";
                        int intId = (int)cmd.ExecuteScalar();

                        LoggingHelper.LogEntry("Patient Past Medical History", "Add", txtID.Text + "|" + ro.Cells[1].Value.ToString() + "|" +
                            ro.Cells[2].Value.ToString(), intId);
                    }
                }
            }
            con.Close();
        }

        public void RetrievePastMedicalHistory()
        {
            string conString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            OleDbConnection con = new OleDbConnection(conString);
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandText = "select * from PatientPastMedicalHistory where PatientID = " + txtID.Text + "";
            con.Open();
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ID");
                dt.Columns.Add("Disease");
                dt.Columns.Add("Diagnosed");
                while (reader.Read())
                {
                    DataRow row = dt.NewRow();
                    row["ID"] = reader["ID"];
                    row["Disease"] = reader["Disease"];
                    row["Diagnosed"] = reader["DiagnosedYear"];
                    dt.Rows.Add(row);
                }
                dataGridViewPastMedicalHistory.DataSource = dt;

                SetGridStylesPastMedicalHistory();
            }
            else
            {
                InitiateDataGridPastMedicalHistory();
            }
            reader.Close();
            con.Close();
        }

        public void InitiateDataGridPastMedicalHistory()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Disease");
            dt.Columns.Add("Diagnosed");
            dataGridViewPastMedicalHistory.DataSource = dt;
            SetGridStylesPastMedicalHistory();
        }

        public void SetGridStylesPastMedicalHistory()
        {
            this.dataGridViewPastMedicalHistory.RowsDefaultCellStyle.BackColor = Color.LightBlue;
            this.dataGridViewPastMedicalHistory.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
            //this.dataGridViewPastMedicalHistory.Columns["ID"].Width = 220;
            this.dataGridViewPastMedicalHistory.Columns["Disease"].Width = 289;
            this.dataGridViewPastMedicalHistory.Columns["Diagnosed"].Width = 120;
            this.dataGridViewPastMedicalHistory.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
            this.dataGridViewPastMedicalHistory.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.dataGridViewPastMedicalHistory.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10.0F, FontStyle.Bold);
            this.dataGridViewPastMedicalHistory.EnableHeadersVisualStyles = false;
            this.dataGridViewPastMedicalHistory.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.dataGridViewPastMedicalHistory.AllowUserToAddRows = true;
            this.dataGridViewPastMedicalHistory.EditMode = DataGridViewEditMode.EditOnKeystroke;
            this.dataGridViewPastMedicalHistory.AllowUserToDeleteRows = false;
        }

#endregion

#region Family History
        public void AddUpdateFamilyHistory()
        {
            string conString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            OleDbConnection con = new OleDbConnection(conString);
            con.Open();
            foreach (DataGridViewRow ro in dataGridViewFamilyHistory.Rows)
            {
                if (Convert.ToString(ro.Cells[0].Value) != string.Empty)
                {
                    string strUpdate = "UPDATE PatientFamilyHistory SET Disease = '" + ro.Cells[1].Value.ToString().Trim() + "', Relationship = '" + ro.Cells[2].Value.ToString().Trim() + "' " +
                        " WHERE ID = " + ro.Cells[0].Value.ToString().Trim() + "";
                    OleDbCommand cmdUpdate = new OleDbCommand(strUpdate, con);
                    cmdUpdate.ExecuteNonQuery();

                    LoggingHelper.LogEntry("Patient Family History", "Update",
                        txtID.Text + "|" + ro.Cells[1].Value.ToString().Trim() + "|" + ro.Cells[2].Value.ToString().Trim(), int.Parse(ro.Cells[0].Value.ToString().Trim()));
                }
                else
                {
                    if (Convert.ToString(ro.Cells[1].Value) != string.Empty)
                    {
                        OleDbCommand cmd = con.CreateCommand();
                        cmd.CommandText = "INSERT INTO PatientFamilyHistory(PatientID, Disease, Relationship)VALUES("
                        + int.Parse(txtID.Text) + ",'"
                        + ro.Cells[1].Value + "','"
                        + ro.Cells[2].Value + "')";
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "Select @@Identity";
                        int intId = (int)cmd.ExecuteScalar();

                        LoggingHelper.LogEntry("Patient Family History", "Add", txtID.Text + "|" + ro.Cells[1].Value.ToString() + "|" +
                            ro.Cells[2].Value.ToString(), intId);
                    }
                }
            }
            con.Close();
        }

        public void RetrieveFamilyHistory()
        {
            string conString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            OleDbConnection con = new OleDbConnection(conString);
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandText = "select * from PatientFamilyHistory where PatientID = " + txtID.Text + "";
            con.Open();
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("IDFamilyHistory");
                dt.Columns.Add("DiseaseFamilyHistory");
                dt.Columns.Add("RelationshipFamilyHistory");
                while (reader.Read())
                {
                    DataRow row = dt.NewRow();
                    row["IDFamilyHistory"] = reader["ID"];
                    row["DiseaseFamilyHistory"] = reader["Disease"];
                    row["RelationshipFamilyHistory"] = reader["Relationship"];
                    dt.Rows.Add(row);
                }
                dataGridViewFamilyHistory.DataSource = dt;

                SetGridStylesFamilyHistory();
            }
            else
            {
                InitiateDataGridFamilyHistory();
            }
            reader.Close();
            con.Close();
        }

        public void InitiateDataGridFamilyHistory()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("IDFamilyHistory");
            dt.Columns.Add("DiseaseFamilyHistory");
            dt.Columns.Add("RelationshipFamilyHistory");
            dataGridViewFamilyHistory.DataSource = dt;
            SetGridStylesFamilyHistory();
        }

        public void SetGridStylesFamilyHistory()
        {
            this.dataGridViewFamilyHistory.RowsDefaultCellStyle.BackColor = Color.LightBlue;
            this.dataGridViewFamilyHistory.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
            this.dataGridViewFamilyHistory.Columns["DiseaseFamilyHistory"].Width = 289;
            this.dataGridViewFamilyHistory.Columns["RelationshipFamilyHistory"].Width = 120;
            this.dataGridViewFamilyHistory.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
            this.dataGridViewFamilyHistory.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.dataGridViewFamilyHistory.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10.0F, FontStyle.Bold);
            this.dataGridViewFamilyHistory.EnableHeadersVisualStyles = false;
            this.dataGridViewFamilyHistory.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.dataGridViewFamilyHistory.AllowUserToAddRows = true;
            this.dataGridViewFamilyHistory.EditMode = DataGridViewEditMode.EditOnKeystroke;
            this.dataGridViewFamilyHistory.AllowUserToDeleteRows = false;
        }

#endregion

#region Allergy History
        public void AddUpdateAllergyHistory()
        {
            string conString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            OleDbConnection con = new OleDbConnection(conString);
            con.Open();
            foreach (DataGridViewRow ro in dataGridViewAllergyHistory.Rows)
            {
                if (Convert.ToString(ro.Cells[0].Value) != string.Empty)
                {
                    string strUpdate = "UPDATE PatientAllergyHistory SET Description = '" + ro.Cells[1].Value.ToString().Trim() + "' " +
                        " WHERE ID = " + ro.Cells[0].Value.ToString().Trim() + "";
                    OleDbCommand cmdUpdate = new OleDbCommand(strUpdate, con);
                    cmdUpdate.ExecuteNonQuery();

                    LoggingHelper.LogEntry("Patient Allergy History", "Update",
                        txtID.Text + "|" + ro.Cells[1].Value.ToString().Trim(), int.Parse(ro.Cells[0].Value.ToString().Trim()));
                }
                else
                {
                    if (Convert.ToString(ro.Cells[1].Value) != string.Empty)
                    {
                        OleDbCommand cmd = con.CreateCommand();
                        cmd.CommandText = "INSERT INTO PatientAllergyHistory(PatientID, Description)VALUES("
                        + int.Parse(txtID.Text) + ",'"
                        + ro.Cells[1].Value + "')";
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "Select @@Identity";
                        int intId = (int)cmd.ExecuteScalar();

                        LoggingHelper.LogEntry("Patient Allergy History", "Add", txtID.Text + "|" + ro.Cells[1].Value.ToString(), intId);
                    }
                }
            }
            con.Close();
        }

        public void RetrieveAllergyHistory()
        {
            string conString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            OleDbConnection con = new OleDbConnection(conString);
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandText = "select * from PatientAllergyHistory where PatientID = " + txtID.Text + "";
            con.Open();
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("IDAllergyHistory");
                dt.Columns.Add("DescriptionAllergyHistory");
                while (reader.Read())
                {
                    DataRow row = dt.NewRow();
                    row["IDAllergyHistory"] = reader["ID"];
                    row["DescriptionAllergyHistory"] = reader["Description"];
                    dt.Rows.Add(row);
                }
                dataGridViewAllergyHistory.DataSource = dt;

                SetGridStylesAllergyHistory();
            }
            else
            {
                InitiateDataGridAllergyHistory();
            }
            reader.Close();
            con.Close();
        }

        public void InitiateDataGridAllergyHistory()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("IDAllergyHistory");
            dt.Columns.Add("DescriptionAllergyHistory");
            dataGridViewAllergyHistory.DataSource = dt;
            SetGridStylesAllergyHistory();
        }

        public void SetGridStylesAllergyHistory()
        {
            this.dataGridViewAllergyHistory.RowsDefaultCellStyle.BackColor = Color.LightBlue;
            this.dataGridViewAllergyHistory.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
            this.dataGridViewAllergyHistory.Columns["DescriptionAllergyHistory"].Width = 409;
            //this.dataGridViewAllergyHistory.Columns["RelationshipFamilyHistory"].Width = 120;
            this.dataGridViewAllergyHistory.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
            this.dataGridViewAllergyHistory.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.dataGridViewAllergyHistory.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10.0F, FontStyle.Bold);
            this.dataGridViewAllergyHistory.EnableHeadersVisualStyles = false;
            this.dataGridViewAllergyHistory.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.dataGridViewAllergyHistory.AllowUserToAddRows = true;
            this.dataGridViewAllergyHistory.EditMode = DataGridViewEditMode.EditOnKeystroke;
            this.dataGridViewAllergyHistory.AllowUserToDeleteRows = false;
        }

#endregion

#region Social History
        public void AddUpdateSocialHistory()
        {
            string conString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            OleDbConnection con = new OleDbConnection(conString);
            con.Open();
            foreach (DataGridViewRow ro in dataGridViewSocialHistory.Rows)
            {
                if (Convert.ToString(ro.Cells[0].Value) != string.Empty)
                {
                    string strUpdate = "UPDATE PatientSocialHistory SET Type = '" + ro.Cells[1].Value.ToString().Trim() + "', Details = '" + ro.Cells[2].Value.ToString().Trim() + "' " +
                        " WHERE ID = " + ro.Cells[0].Value.ToString().Trim() + "";
                    OleDbCommand cmdUpdate = new OleDbCommand(strUpdate, con);
                    cmdUpdate.ExecuteNonQuery();

                    LoggingHelper.LogEntry("Patient Social History", "Update",
                        txtID.Text + "|" + ro.Cells[1].Value.ToString().Trim() + "|" + ro.Cells[2].Value.ToString().Trim(), int.Parse(ro.Cells[0].Value.ToString().Trim()));
                }
                else
                {
                    if (Convert.ToString(ro.Cells[1].Value) != string.Empty)
                    {
                        OleDbCommand cmd = con.CreateCommand();
                        cmd.CommandText = "INSERT INTO PatientSocialHistory(PatientID, Type, Details)VALUES("
                        + int.Parse(txtID.Text) + ",'"
                        + ro.Cells[1].Value + "','"
                        + ro.Cells[2].Value + "')";
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "Select @@Identity";
                        int intId = (int)cmd.ExecuteScalar();

                        LoggingHelper.LogEntry("Patient Social History", "Add", txtID.Text + "|" + ro.Cells[1].Value.ToString() + "|" +
                            ro.Cells[2].Value.ToString(), intId);
                    }
                }
            }
            con.Close();
        }

        public void RetrieveSocialHistory()
        {
            string conString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            OleDbConnection con = new OleDbConnection(conString);
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandText = "select * from PatientSocialHistory where PatientID = " + txtID.Text + "";
            con.Open();
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("IDSocialHistory");
                dt.Columns.Add("TypeSocialHistory");
                dt.Columns.Add("DetailsSocialHistory");
                while (reader.Read())
                {
                    DataRow row = dt.NewRow();
                    row["IDSocialHistory"] = reader["ID"];
                    row["TypeSocialHistory"] = reader["Type"];
                    row["DetailsSocialHistory"] = reader["Details"];
                    dt.Rows.Add(row);
                }
                dataGridViewSocialHistory.DataSource = dt;

                SetGridStylesSocialHistory();
            }
            else
            {
                InitiateDataGridSocialHistory();
            }
            reader.Close();
            con.Close();
        }

        public void InitiateDataGridSocialHistory()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("IDSocialHistory");
            dt.Columns.Add("TypeSocialHistory");
            dt.Columns.Add("DetailsSocialHistory");

            string conString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            OleDbConnection con = new OleDbConnection(conString);
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandText = "select * from SocialHistoryTypes";
            con.Open();
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    DataRow row = dt.NewRow();
                    row["TypeSocialHistory"] = reader["SocialHistoryTypes"];
                    dt.Rows.Add(row);
                }
            }

            dataGridViewSocialHistory.DataSource = dt;
            SetGridStylesSocialHistory();
        }

        public void SetGridStylesSocialHistory()
        {
            this.dataGridViewSocialHistory.RowsDefaultCellStyle.BackColor = Color.LightBlue;
            this.dataGridViewSocialHistory.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
            this.dataGridViewSocialHistory.Columns["TypeSocialHistory"].Width = 289;
            this.dataGridViewSocialHistory.Columns["DetailsSocialHistory"].Width = 120;
            this.dataGridViewSocialHistory.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
            this.dataGridViewSocialHistory.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.dataGridViewSocialHistory.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10.0F, FontStyle.Bold);
            this.dataGridViewSocialHistory.EnableHeadersVisualStyles = false;
            this.dataGridViewSocialHistory.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.dataGridViewSocialHistory.AllowUserToAddRows = false;
            this.dataGridViewSocialHistory.EditMode = DataGridViewEditMode.EditOnKeystroke;
            this.dataGridViewSocialHistory.AllowUserToDeleteRows = false;
        }

        #endregion

#region Drug History

        public void AddUpdateDrugHistory()
        {
            string conString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            OleDbConnection con = new OleDbConnection(conString);
            con.Open();
            foreach (DataGridViewRow ro in dataGridViewDrugHistory.Rows)
            {
                if (Convert.ToString(ro.Cells[0].Value) != string.Empty)
                {
                    string strUpdate = "UPDATE PatientDrugHistory SET DrugName = '" + ro.Cells[1].Value.ToString().Trim() + "', Dose = '" + ro.Cells[2].Value.ToString().Trim() + "', " +
                        "Duration = '" + ro.Cells[3].Value.ToString().Trim() + "' WHERE ID = " + ro.Cells[0].Value.ToString().Trim() + "";
                    OleDbCommand cmdUpdate = new OleDbCommand(strUpdate, con);
                    cmdUpdate.ExecuteNonQuery();

                    LoggingHelper.LogEntry("Patient Drug History", "Update",
                        txtID.Text + "|" + ro.Cells[1].Value.ToString().Trim() + "|" + ro.Cells[2].Value.ToString().Trim() + "|" + ro.Cells[3].Value.ToString().Trim(), 
                        int.Parse(ro.Cells[0].Value.ToString().Trim()));
                }
                else
                {
                    if (Convert.ToString(ro.Cells[1].Value) != string.Empty)
                    {
                        OleDbCommand cmd = con.CreateCommand();
                        cmd.CommandText = "INSERT INTO PatientDrugHistory(PatientID, DrugName, Dose, Duration)VALUES("
                        + int.Parse(txtID.Text) + ",'"
                        + ro.Cells[1].Value + "', '"
                        + ro.Cells[2].Value + "', '"
                        + ro.Cells[3].Value + "')";
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "Select @@Identity";
                        int intId = (int)cmd.ExecuteScalar();

                        LoggingHelper.LogEntry("Patient Drug History", "Add", txtID.Text + "|" + ro.Cells[1].Value.ToString() + "|" + ro.Cells[2].Value.ToString().Trim() 
                            + "|" + ro.Cells[3].Value.ToString().Trim(), intId);
                    }
                }
            }
            con.Close();
        }

        public void RetrieveDrugHistory()
        {
            string conString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            OleDbConnection con = new OleDbConnection(conString);
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandText = "select * from PatientDrugHistory where PatientID = " + txtID.Text + "";
            con.Open();
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("IDDrugHistory");
                dt.Columns.Add("NameDrugHistory");
                dt.Columns.Add("DoseDrugHistory");
                dt.Columns.Add("DurationDrugHistory");
                while (reader.Read())
                {
                    DataRow row = dt.NewRow();
                    row["IDDrugHistory"] = reader["ID"];
                    row["NameDrugHistory"] = reader["DrugName"];
                    row["DoseDrugHistory"] = reader["Dose"];
                    row["DurationDrugHistory"] = reader["Duration"];
                    dt.Rows.Add(row);
                }
                dataGridViewDrugHistory.DataSource = dt;

                SetGridStylesDrugHistory();
            }
            else
            {
                InitiateDataGridDrugHistory();
            }
            reader.Close();
            con.Close();
        }

        public void InitiateDataGridDrugHistory()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("IDDrugHistory");
            dt.Columns.Add("NameDrugHistory");
            dt.Columns.Add("DoseDrugHistory");
            dt.Columns.Add("DurationDrugHistory");
            dataGridViewDrugHistory.DataSource = dt;
            SetGridStylesDrugHistory();
        }

        public void SetGridStylesDrugHistory()
        {
            this.dataGridViewDrugHistory.RowsDefaultCellStyle.BackColor = Color.LightBlue;
            this.dataGridViewDrugHistory.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
            this.dataGridViewDrugHistory.Columns["NameDrugHistory"].Width = 269;
            this.dataGridViewDrugHistory.Columns["DoseDrugHistory"].Width = 60;
            this.dataGridViewDrugHistory.Columns["DurationDrugHistory"].Width = 80;
            this.dataGridViewDrugHistory.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
            this.dataGridViewDrugHistory.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.dataGridViewDrugHistory.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10.0F, FontStyle.Bold);
            this.dataGridViewDrugHistory.EnableHeadersVisualStyles = false;
            this.dataGridViewDrugHistory.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.dataGridViewDrugHistory.AllowUserToAddRows = true;
            this.dataGridViewDrugHistory.EditMode = DataGridViewEditMode.EditOnKeystroke;
            this.dataGridViewDrugHistory.AllowUserToDeleteRows = false;
        }

        #endregion

#region Vaccination
        public void AddUpdateVaccinationHistory()
        {
            string conString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            OleDbConnection con = new OleDbConnection(conString);
            con.Open();
            foreach (DataGridViewRow ro in dataGridViewVaccination.Rows)
            {
                if (Convert.ToString(ro.Cells[0].Value) != string.Empty)
                {
                    string strUpdate = "UPDATE PatientVaccination SET Description = '" + ro.Cells[1].Value.ToString().Trim() + "' " +
                        " WHERE ID = " + ro.Cells[0].Value.ToString().Trim() + "";
                    OleDbCommand cmdUpdate = new OleDbCommand(strUpdate, con);
                    cmdUpdate.ExecuteNonQuery();

                    LoggingHelper.LogEntry("Patient Vaccination", "Update",
                        txtID.Text + "|" + ro.Cells[1].Value.ToString().Trim(), int.Parse(ro.Cells[0].Value.ToString().Trim()));
                }
                else
                {
                    if (Convert.ToString(ro.Cells[1].Value) != string.Empty)
                    {
                        OleDbCommand cmd = con.CreateCommand();
                        cmd.CommandText = "INSERT INTO PatientVaccination(PatientID, Description)VALUES("
                        + int.Parse(txtID.Text) + ",'"
                        + ro.Cells[1].Value + "')";
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "Select @@Identity";
                        int intId = (int)cmd.ExecuteScalar();

                        LoggingHelper.LogEntry("Patient Vaccination", "Add", txtID.Text + "|" + ro.Cells[1].Value.ToString(), intId);
                    }
                }
            }
            con.Close();
        }

        public void RetrieveVaccinationHistory()
        {
            string conString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            OleDbConnection con = new OleDbConnection(conString);
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandText = "select * from PatientVaccination where PatientID = " + txtID.Text + "";
            con.Open();
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("IDVaccination");
                dt.Columns.Add("DescriptionVaccination");
                while (reader.Read())
                {
                    DataRow row = dt.NewRow();
                    row["IDVaccination"] = reader["ID"];
                    row["DescriptionVaccination"] = reader["Description"];
                    dt.Rows.Add(row);
                }
                dataGridViewVaccination.DataSource = dt;

                SetGridStylesVaccination();
            }
            else
            {
                InitiateDataGridVaccination();
            }
            reader.Close();
            con.Close();
        }

        public void InitiateDataGridVaccination()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("IDVaccination");
            dt.Columns.Add("DescriptionVaccination");
            dataGridViewVaccination.DataSource = dt;
            SetGridStylesVaccination();
        }

        public void SetGridStylesVaccination()
        {
            this.dataGridViewVaccination.RowsDefaultCellStyle.BackColor = Color.LightBlue;
            this.dataGridViewVaccination.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
            this.dataGridViewVaccination.Columns["DescriptionVaccination"].Width = 409;
            this.dataGridViewVaccination.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
            this.dataGridViewVaccination.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.dataGridViewVaccination.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10.0F, FontStyle.Bold);
            this.dataGridViewVaccination.EnableHeadersVisualStyles = false;
            this.dataGridViewVaccination.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.dataGridViewVaccination.AllowUserToAddRows = true;
            this.dataGridViewVaccination.EditMode = DataGridViewEditMode.EditOnKeystroke;
            this.dataGridViewVaccination.AllowUserToDeleteRows = false;
        }

#endregion

        public Boolean ValidateForm()
        {
            bool isValidated = false;

            if (!string.IsNullOrEmpty(txtID.Text))
            {
                string conString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
                OleDbConnection con = new OleDbConnection(conString);
                OleDbCommand cmd = con.CreateCommand();
                con.Open();
                if (dataGridViewPrescription.Rows.Count > 1)
                {
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
                }
                else
                {
                    isValidated = true;
                }
                endOfLoop:
                con.Close();
            }
            else
            {
                MessageBox.Show("Need to select a Patient to proceed.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FormRefresh();
        }

        public void FormRefresh()
        {
            //txtID.Text = string.Empty;
            txtName.Text = string.Empty;
            txtDoB.Text = string.Empty;
            txtMaritalStatus.Text = string.Empty;
            txtSex.Text = string.Empty;
            txtOccupation.Text = string.Empty;
            txtNic.Text = string.Empty;
            txtContactNo.Text = string.Empty;

            txtHistoryOfPresentComplaint.Text = string.Empty;
            txtRelevantInvestigation.Text = string.Empty;
            txtExaminationFindings.Text = string.Empty;
            txtProblemListCurrent.Text = string.Empty;

            InitiateDataGridPastMedicalHistory();
            InitiateDataGridFamilyHistory();
            InitiateDataGridAllergyHistory();
            InitiateDataGridSocialHistory();
            InitiateDataGridDrugHistory();
            InitiateDataGridVaccination();
        }
    }
}
