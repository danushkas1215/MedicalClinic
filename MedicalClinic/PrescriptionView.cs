using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MedicalClinic
{
    public partial class PrescriptionView : Form
    {
        public String strArrivalID = string.Empty;
        public PrescriptionView(string strID)
        {
            InitializeComponent();
            string[] strArr = null;
            char[] splitchar = { '|' };
            strArr = strID.Split(splitchar);
            strArrivalID = strArr[1].ToString();
            txtID.Text = strArr[0].ToString();

            dataGridViewPrescriptionData.CellFormatting += new DataGridViewCellFormattingEventHandler(dataGridViewPrescriptionData_CellFormatting);
        }

        private void dataGridViewPrescriptionData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex == dataGridViewPrescriptionData.RowCount - 1)
            {
                if (e.ColumnIndex == dataGridViewPrescriptionData.Columns["MedicineID"].Index && e.Value != null)
                {
                    e.CellStyle.Font = new Font("Microsoft Sans Serif", 10.0F, FontStyle.Bold);
                }
                else if (e.ColumnIndex == dataGridViewPrescriptionData.Columns["Price"].Index && e.Value != null)
                {
                    e.CellStyle.Font = new Font("Microsoft Sans Serif", 10.0F, FontStyle.Bold);
                }
            }
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
                    txtDoB.Text = DateTime.Parse(reader["Birthday"].ToString()).ToString("dd/MMM/yyyy");
                    txtMaritalStatus.Text = reader["MaritalStatus"].ToString();
                    txtSex.Text = reader["Sex"].ToString();
                    txtOccupation.Text = reader["Occupation"].ToString();
                    txtNic.Text = reader["NIC"].ToString();
                    txtContactNo.Text = reader["Mobile"].ToString();
                }
            }
            reader.Close();
            con.Close();

            RetrievePrescriptionData();
        }

        public void RetrievePrescriptionData()
        {
            string conString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            OleDbConnection con = new OleDbConnection(conString);
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT Medicines.MedTradeName, Medicines.MedicineName, Medicines.MedDispensing, Medicines.MedPrice, PatientPrescription.DurationType, PatientPrescription.FrequencyType, "+
                "PatientPrescription.MedicineID, PatientPrescription.Dosage, PatientPrescription.Duration, MedicineTypes.MedicineType, MedicineFrequencyTypes.MedicineFrequencyType, "+
                "MedicineDurationTypes.MedicineDurationType, MedicineRelationToMealTypes.MedicineRelationToMealType, MedicineRoutTypes.MedicineRoutType " +
                "FROM (((((PatientPrescription INNER JOIN Medicines ON PatientPrescription.MedicineID = Medicines.ID) INNER JOIN MedicineTypes ON PatientPrescription.MedicineType = MedicineTypes.ID) " +
                "INNER JOIN MedicineFrequencyTypes ON PatientPrescription.FrequencyType = MedicineFrequencyTypes.ID) INNER JOIN MedicineDurationTypes ON " +
                "PatientPrescription.DurationType = MedicineDurationTypes.ID) INNER JOIN MedicineRelationToMealTypes ON PatientPrescription.RelationType = MedicineRelationToMealTypes.ID) " +
                "INNER JOIN MedicineRoutTypes ON PatientPrescription.RouteType = MedicineRoutTypes.ID " +
                "WHERE PatientID = " + txtID.Text + " AND PatientArrivalID = " + strArrivalID + " ORDER BY Medicines.MedicineName";
            con.Open();
            OleDbDataReader reader = cmd.ExecuteReader();
            double dblColumnTotal = 0;
            if (reader.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("MedicineID");
                dt.Columns.Add("MedicineType");
                dt.Columns.Add("Dosage");
                dt.Columns.Add("FrequencyType");
                dt.Columns.Add("Duration");
                dt.Columns.Add("DurationType");
                dt.Columns.Add("RelationType");
                dt.Columns.Add("RouteType");
                dt.Columns.Add("UnitPrice");
                dt.Columns.Add("Total");
                dt.Columns.Add("Price");

                while (reader.Read())
                {
                    DataRow row = dt.NewRow();
                    string strTradeName = string.Empty;
                    string strMedicineName = string.Empty;
                    strTradeName = reader["MedTradeName"].ToString();

                    if (string.IsNullOrEmpty(strTradeName))
                    {
                        strMedicineName = reader["MedicineName"].ToString();
                    }
                    else
                    {
                        strMedicineName = reader["MedicineName"].ToString() + " - (" + reader["MedTradeName"].ToString() + ")";
                    }
                    row["MedicineID"] = strMedicineName;
                    row["MedicineType"] = reader["MedicineType"];
                    row["Dosage"] = reader["Dosage"];
                    row["FrequencyType"] = reader["MedicineFrequencyType"];
                    row["Duration"] = reader["Duration"];
                    row["DurationType"] = reader["MedicineDurationType"];
                    row["RelationType"] = reader["MedicineRelationToMealType"];
                    row["RouteType"] = reader["MedicineRoutType"];
                    bool boolIsDespensing = bool.Parse(reader["MedDispensing"].ToString());
                    if (boolIsDespensing)
                    {
                        row["UnitPrice"] = "FOC";
                    }
                    else
                    {
                        row["UnitPrice"] = Math.Round(Double.Parse(reader["MedPrice"].ToString()), 2).ToString("#.00");
                    }
                    
                    string strTotalTablets = GetTotalMedicines(reader["Duration"].ToString(), reader["DurationType"].ToString(), reader["FrequencyType"].ToString(), 
                        reader["Dosage"].ToString(), reader["MedicineID"].ToString());
                    row["Total"] = strTotalTablets;

                    string strRowTotal = string.Empty;
                    if (boolIsDespensing)
                    {
                        strRowTotal = Math.Round(0.0, 2).ToString("#.00");
                    }
                    else
                    {
                        strRowTotal = Math.Round(GetPriceValues(reader["MedicineID"].ToString(), strTotalTablets), 2).ToString("#.00");
                    }
                         
                    dblColumnTotal = dblColumnTotal + Math.Round(Double.Parse(strRowTotal), 2);
                    row["Price"] = strRowTotal;

                    dt.Rows.Add(row);
                }

                DataRow rowBlank = dt.NewRow();
                rowBlank["MedicineID"] = "Total";
                rowBlank["MedicineType"] = "";
                rowBlank["Dosage"] = "";
                rowBlank["FrequencyType"] = "";
                rowBlank["Duration"] = "";
                rowBlank["DurationType"] = "";
                rowBlank["RelationType"] = "";
                rowBlank["RouteType"] = "";
                rowBlank["UnitPrice"] = "";
                rowBlank["Total"] = "";
                rowBlank["Price"] = dblColumnTotal.ToString("#.00");
                dt.Rows.Add(rowBlank);

                dataGridViewPrescriptionData.DataSource = dt;

                SetGridStylesPrescriptionData();
            }
            reader.Close();
            con.Close();
        }

        public void SetGridStylesPrescriptionData()
        {
            this.dataGridViewPrescriptionData.RowsDefaultCellStyle.BackColor = Color.LightBlue;
            this.dataGridViewPrescriptionData.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
            this.dataGridViewPrescriptionData.Columns["MedicineID"].HeaderText = "Medicine";
            this.dataGridViewPrescriptionData.Columns["MedicineID"].Width = 300;
            this.dataGridViewPrescriptionData.Columns["MedicineType"].HeaderText = "Type";
            this.dataGridViewPrescriptionData.Columns["MedicineType"].Width = 80;
            this.dataGridViewPrescriptionData.Columns["Dosage"].Width = 80;
            this.dataGridViewPrescriptionData.Columns["FrequencyType"].HeaderText = "Frequency";
            this.dataGridViewPrescriptionData.Columns["FrequencyType"].Width = 80;
            this.dataGridViewPrescriptionData.Columns["Duration"].Width = 80;
            this.dataGridViewPrescriptionData.Columns["DurationType"].HeaderText = "Duration Type";
            this.dataGridViewPrescriptionData.Columns["DurationType"].Width = 110;
            this.dataGridViewPrescriptionData.Columns["RelationType"].HeaderText = "Relation";
            this.dataGridViewPrescriptionData.Columns["RouteType"].HeaderText = "Route";
            this.dataGridViewPrescriptionData.Columns["UnitPrice"].Width = 80;
            this.dataGridViewPrescriptionData.Columns["UnitPrice"].HeaderText = "Unit Price";
            this.dataGridViewPrescriptionData.Columns["UnitPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewPrescriptionData.Columns["Total"].Width = 80;
            this.dataGridViewPrescriptionData.Columns["Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewPrescriptionData.Columns["Price"].Width = 80;
            this.dataGridViewPrescriptionData.Columns["Price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewPrescriptionData.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
            this.dataGridViewPrescriptionData.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.dataGridViewPrescriptionData.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10.0F, FontStyle.Bold);
            this.dataGridViewPrescriptionData.EnableHeadersVisualStyles = false;
            this.dataGridViewPrescriptionData.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.dataGridViewPrescriptionData.AllowUserToAddRows = false;
            this.dataGridViewPrescriptionData.AllowUserToDeleteRows = false;
        }

        public string GetTotalMedicines(string strDuration, string strDurationTypeId, string strFrequencyId, string strDosage, string strMedicineId)
        {
            string strTotalMedicines = string.Empty;
            int intTotalMedicineUnits = 0;
            double intMedicineContents = 0;

            string conString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            OleDbConnection con = new OleDbConnection(conString);
            OleDbCommand cmd = con.CreateCommand();
            con.Open();
            cmd.CommandText = "SELECT * FROM Medicines WHERE ID = " + int.Parse(strMedicineId) + "";
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    intTotalMedicineUnits = int.Parse(reader["MedUnits"].ToString());
                    intMedicineContents = int.Parse(reader["MedContents"].ToString());
                }
            }
            reader.Close();
            con.Close();

            int intFrequencyTimes = int.Parse(RetrieveFrequencyTimes(strFrequencyId));

            var match = Regex.Match(strDosage, "(\\w+)(\\d+)");
            double intDosage = int.Parse(match.Groups[0].Value);

            int intNoOfDays = int.Parse(CalculateDays(strDuration, strDurationTypeId));
            int intTotalTimes = intFrequencyTimes * intNoOfDays;

            if (intDosage != intMedicineContents)
            {
                if (intMedicineContents > intDosage)
                {
                    double intHalfTablets = intMedicineContents / intDosage;
                    if (intHalfTablets == 2)
                    {
                        strTotalMedicines = (double.Parse(intTotalMedicineUnits.ToString()) - 0.5).ToString();
                    }
                }
                else
                {
                    double intTotalTablets = intDosage / intMedicineContents;
                    strTotalMedicines = (intTotalTablets * intTotalTimes).ToString();
                }
            }
            else
            {
                strTotalMedicines = intTotalTimes.ToString();
            }

            return strTotalMedicines;
        }

        public string CalculateDays(string strDuration, string strDurationTypeId)
        {
            string strNoOfDays = string.Empty;
            string strDurationType = string.Empty;

            string conString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            OleDbConnection con = new OleDbConnection(conString);
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandText = "select * from MedicineDurationTypes where ID = " + strDurationTypeId + "";
            con.Open();
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    strDurationType = reader["MedicineDurationType"].ToString();
                }
            }
            reader.Close();
            con.Close();

            if (strDurationType.Equals("days"))
            {
                strNoOfDays = strDuration;
            }
            else if (strDurationType.Equals("weeks"))
            {
                strNoOfDays = (int.Parse(strDuration) * 7).ToString();
            }
            else if (strDurationType.Equals("months"))
            {
                DateTime dtNextDate = DateTime.Today.AddMonths(int.Parse(strDuration));
                strNoOfDays = dtNextDate.Date.Subtract(DateTime.Now.Date).Days.ToString();
            }

            return strNoOfDays;
        }

        public string RetrieveFrequencyTimes(string strID)
        {
            string strFrequencyTimes = string.Empty;
            string conString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            OleDbConnection con = new OleDbConnection(conString);
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandText = "select * from MedicineFrequencyTypes where ID = " + strID + "";
            con.Open();
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    strFrequencyTimes = reader["MedicineNoOfTimes"].ToString();
                }
            }
            reader.Close();
            con.Close();

            return strFrequencyTimes;
        }

        public double GetPriceValues(string strMedicineId, string strTotalMedicine)
        {
            double dblTotal = 0;
            double dblUnitPrice = 0;
            string conString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            OleDbConnection con = new OleDbConnection(conString);
            OleDbCommand cmd = con.CreateCommand();
            con.Open();
            cmd.CommandText = "SELECT * FROM Medicines WHERE ID = " + int.Parse(strMedicineId) + "";
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    dblUnitPrice = double.Parse(reader["MedPrice"].ToString());
                }
            }
            reader.Close();
            con.Close();

            dblTotal = dblUnitPrice * double.Parse(strTotalMedicine);

            return dblTotal;
        }

        private void dataGridViewPrescriptionData_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            this.dataGridViewPrescriptionData.ClearSelection();
        }

        private void btnDispense_Click(object sender, EventArgs e)
        {
            string conString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            OleDbConnection con = new OleDbConnection(conString);
            con.Open();

            string strUpdate = "UPDATE PatientsArrival SET PatientStatus = 'Despense' " +
                " WHERE ID = " + int.Parse(strArrivalID) + " AND PatientID = " + int.Parse(txtID.Text) + "";
            OleDbCommand cmdUpdate = new OleDbCommand(strUpdate, con);
            cmdUpdate.ExecuteNonQuery();

            LoggingHelper.LogEntry("Patients Arrival", "Update", txtID.Text + "|Despense|", int.Parse(strArrivalID));
            con.Close();
        }
    }
}
