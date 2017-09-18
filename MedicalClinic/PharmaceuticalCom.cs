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
using System.Configuration;

namespace MedicalClinic
{
    public partial class PharmaceuticalCom : Form
    {
        public PharmaceuticalCom(string strID)
        {
            InitializeComponent();
            txtComID.Text = strID;
            if(string.IsNullOrEmpty(strID))
            {
                InitiateDatagrid();
                InitiateDistributorDatagrid();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!FormValidate())
            {
                if (btnAdd.Text == "Add")
                {
                    string conString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
                    OleDbConnection con = new OleDbConnection(conString);
                    OleDbCommand cmd = con.CreateCommand();
                    con.Open();
                    cmd.CommandText = "Insert into PharmaceuticalCompanies(CompanyName,CompanyAddress,CompanyPhone,CompanyEmail)Values('" + txtComName.Text + "'," +
                        "'" + txtComAddress.Text + "','" + txtComPhone.Text + "','" + txtComEmail.Text + "')";
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "Select @@Identity";
                    int intId = (int)cmd.ExecuteScalar();
                    con.Close();
                    AddRepData(intId.ToString());
                    AddDistributorData(intId.ToString());
                    MessageBox.Show("Record Successfully Saved", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoggingHelper.LogEntry("Pharmaceutical company", "Add", txtComName.Text.Trim() + "|" + txtComAddress.Text.Trim() + "|" + txtComPhone.Text.Trim() 
                        + "|" + txtComEmail.Text.Trim(), intId);
                    FormRefresh();
                }
                else
                {
                    string conString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
                    OleDbConnection con = new OleDbConnection(conString);
                    con.Open();
                    string strUpdate = "update PharmaceuticalCompanies set " +
                        "CompanyAddress ='" + txtComAddress.Text.Trim() + "', " +
                        "CompanyPhone ='" + txtComPhone.Text.Trim() + "'," +
                        "CompanyEmail ='" + txtComEmail.Text.Trim() + "' where ID=" + txtComID.Text.Trim() + "";
                    OleDbCommand cmd = new OleDbCommand(strUpdate, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    DeleteRepData(txtComID.Text);
                    AddRepData(txtComID.Text);
                    DeleteDistributorData(txtComID.Text);
                    AddDistributorData(txtComID.Text);
                    LoggingHelper.LogEntry("Pharmaceutical company", "Update", txtComName.Text.Trim() + "|" + txtComAddress.Text.Trim() + "|" + txtComPhone.Text.Trim() 
                        + "|" + txtComEmail.Text.Trim(), int.Parse(txtComID.Text.Trim()));
                    MessageBox.Show("Record Successfully Updated", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    FormRefresh();
                    txtComID.Text = string.Empty;
                    txtComName.Enabled = true;
                    btnAdd.Text = "Add";
                }
            }


        }

        public void FormRefresh()
        {
            txtComName.Text = "";
            txtComAddress.Text = "";
            txtComPhone.Text = "";
            txtComEmail.Text = "";

            InitiateDatagrid();
            InitiateDistributorDatagrid();
            btnDelete.Enabled = false;
            txtComName.Enabled = true;
            btnAdd.Text = "Add";
            btnDelete.Enabled = false;
        }

        public void InitiateDatagrid()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Mobile");
            dt.Columns.Add("Email");
            dataGridViewRep.DataSource = dt;
            SetRepGridStyles();
        }

        public void AddRepData(string strID)
        {
            string conString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            OleDbConnection con = new OleDbConnection(conString);
            OleDbCommand cmd = con.CreateCommand();
            con.Open();
            foreach (DataGridViewRow ro in dataGridViewRep.Rows)
            {
                if (Convert.ToString(ro.Cells[0].Value) != string.Empty)
                {
                    cmd.CommandText = "Insert into MedicalRep(CompanyID, RepName, RepMobile, RepEmail)Values("
                    + int.Parse(strID) + ",'"
                    + ro.Cells[0].Value + "','"
                    + ro.Cells[1].Value + "','"
                    + ro.Cells[2].Value + "')";
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                }
            }
            con.Close();
        }

        public void DeleteRepData(string strID)
        {
            string conString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            OleDbConnection con = new OleDbConnection(conString);
            OleDbCommand cmd = con.CreateCommand();
            con.Open();
            foreach (DataGridViewRow ro in dataGridViewRep.Rows)
            {
                if (ro.Cells[0].Value != null)
                {
                    cmd.CommandText = "DELETE FROM MedicalRep WHERE CompanyID = "+int.Parse(strID)+"";
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                }
            }
            con.Close();
        }

        public void RetrieveRepData(string strComId)
        {
            string conString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            OleDbConnection con = new OleDbConnection(conString);
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandText = "select * from MedicalRep where CompanyID = " + int.Parse(strComId) + "";
            con.Open();
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Name");
                dt.Columns.Add("Mobile");
                dt.Columns.Add("Email");
                while (reader.Read())
                {
                    DataRow row = dt.NewRow();
                    row["Name"] = reader["RepName"];
                    row["Mobile"] = reader["RepMobile"];
                    row["Email"] = reader["RepEmail"];
                    dt.Rows.Add(row);
                }
                dataGridViewRep.DataSource = dt;

                SetRepGridStyles();
            }
            else
            {
                InitiateDatagrid();
            }
            reader.Close();
            con.Close();
        }

        public void InitiateDistributorDatagrid()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Adress");
            dt.Columns.Add("Phone");
            dt.Columns.Add("Email");
            dataGridViewDistributor.DataSource = dt;
            SetDistributorGridStyles();
        }

        public void AddDistributorData(string strID)
        {
            string conString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            OleDbConnection con = new OleDbConnection(conString);
            OleDbCommand cmd = con.CreateCommand();
            con.Open();
            foreach (DataGridViewRow ro in dataGridViewDistributor.Rows)
            {
                if (Convert.ToString(ro.Cells[0].Value) != string.Empty)
                {
                    cmd.CommandText = "Insert into MedicalDistributors(DisCompanyID, DistributorName, DistributorAddress, DistributorPhone, DistributorEmail)Values("
                    + int.Parse(strID) + ",'"
                    + ro.Cells[0].Value + "','"
                    + ro.Cells[1].Value + "','"
                    + ro.Cells[2].Value + "','"
                    + ro.Cells[3].Value + "')";
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                }
            }
            con.Close();
        }

        public void DeleteDistributorData(string strID)
        {
            string conString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            OleDbConnection con = new OleDbConnection(conString);
            OleDbCommand cmd = con.CreateCommand();
            con.Open();
            foreach (DataGridViewRow ro in dataGridViewDistributor.Rows)
            {
                if (ro.Cells[0].Value != null)
                {
                    cmd.CommandText = "DELETE FROM MedicalDistributors WHERE DisCompanyID = " + int.Parse(strID) + "";
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                }
            }
            con.Close();
        }

        public void RetrieveDistributorData(string strComId)
        {
            string conString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            OleDbConnection con = new OleDbConnection(conString);
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandText = "select * from MedicalDistributors where DisCompanyID = " + int.Parse(strComId) + "";
            con.Open();
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Name");
                dt.Columns.Add("Adress");
                dt.Columns.Add("Phone");
                dt.Columns.Add("Email");
                while (reader.Read())
                {
                    DataRow row = dt.NewRow();
                    row["Name"] = reader["DistributorName"];
                    row["Adress"] = reader["DistributorAddress"];
                    row["Phone"] = reader["DistributorPhone"];
                    row["Email"] = reader["DistributorEmail"];
                    dt.Rows.Add(row);
                }
                dataGridViewDistributor.DataSource = dt;

                SetDistributorGridStyles();
            }
            else
            {
                InitiateDistributorDatagrid();
            }
            reader.Close();
            con.Close();
        }

        private void txtComID_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtComID.Text))
            {
                string conString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
                OleDbConnection con = new OleDbConnection(conString);
                OleDbCommand cmd = con.CreateCommand();
                cmd.CommandText = "select * from PharmaceuticalCompanies where ID = " + txtComID.Text + "";
                con.Open();
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        txtComName.Text = reader["CompanyName"].ToString();
                        txtComAddress.Text = reader["CompanyAddress"].ToString();
                        txtComPhone.Text = reader["CompanyPhone"].ToString();
                        txtComEmail.Text = reader["CompanyEmail"].ToString();
                    }
                }
                reader.Close();
                con.Close();
                RetrieveRepData(txtComID.Text);
                RetrieveDistributorData(txtComID.Text);
                btnAdd.Text = "Update";
                txtComName.Enabled = false;
                btnDelete.Enabled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FormRefresh();
            txtComID.Text = string.Empty;
        }

        public Boolean FormValidate()
        {
            bool blnValidated = false;
            if (string.IsNullOrEmpty(txtComName.Text.Trim()))
            {
                blnValidated = true;
                MessageBox.Show("Company Name cannot be empty", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return blnValidated;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!FormValidate())
            {
                DialogResult result;

                result = MessageBox.Show("Are you sure want to Delete the record?", "Conformation", MessageBoxButtons.YesNo);

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    string conString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
                    OleDbConnection con = new OleDbConnection(conString);
                    string strDelete = "delete from PharmaceuticalCompanies where id=@id";
                    OleDbCommand cmd = new OleDbCommand(strDelete, con);
                    cmd.Parameters.AddWithValue("@id", txtComID.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    DeleteRepData(txtComID.Text);
                    DeleteDistributorData(txtComID.Text);

                    LoggingHelper.LogEntry("Pharmaceutical company", "Delete", txtComName.Text.Trim() + "|" + txtComAddress.Text.Trim() + "|" + txtComPhone.Text.Trim()
                        + "|" + txtComEmail.Text.Trim(), int.Parse(txtComID.Text.Trim()));
                    MessageBox.Show("Records Successfully  Deleted", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    FormRefresh();
                    txtComID.Text = string.Empty;
                }
            }
        }

        public void SetRepGridStyles()
        {
            this.dataGridViewRep.RowsDefaultCellStyle.BackColor = Color.LightBlue;
            this.dataGridViewRep.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
            this.dataGridViewRep.Columns["Name"].Width = 220;
            this.dataGridViewRep.Columns["Mobile"].Width = 125;
            this.dataGridViewRep.Columns["Email"].Width = 190;
            this.dataGridViewRep.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
            this.dataGridViewRep.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.dataGridViewRep.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10.0F, FontStyle.Bold);
            this.dataGridViewRep.EnableHeadersVisualStyles = false;
            this.dataGridViewRep.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.dataGridViewRep.AllowUserToAddRows = true;
            this.dataGridViewRep.EditMode = DataGridViewEditMode.EditOnKeystroke;
            this.dataGridViewRep.AllowUserToDeleteRows = true;
        }

        public void SetDistributorGridStyles()
        {
            this.dataGridViewDistributor.RowsDefaultCellStyle.BackColor = Color.LightBlue;
            this.dataGridViewDistributor.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
            this.dataGridViewDistributor.Columns["Name"].Width = 200;
            this.dataGridViewDistributor.Columns["Adress"].Width = 190;
            this.dataGridViewDistributor.Columns["Email"].Width = 65;
            this.dataGridViewDistributor.Columns["Phone"].Width = 100;
            this.dataGridViewDistributor.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
            this.dataGridViewDistributor.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.dataGridViewDistributor.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10.0F, FontStyle.Bold);
            this.dataGridViewDistributor.EnableHeadersVisualStyles = false;
            this.dataGridViewDistributor.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.dataGridViewDistributor.AllowUserToAddRows = true;
            this.dataGridViewDistributor.AllowUserToDeleteRows = true;
            this.dataGridViewDistributor.EditMode = DataGridViewEditMode.EditOnKeystroke;
        }
    }
}
