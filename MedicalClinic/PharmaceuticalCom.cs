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
            this.dataGridViewRep.RowsDefaultCellStyle.BackColor = Color.LightBlue;
            this.dataGridViewRep.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
            this.dataGridViewRep.Columns["Name"].Width = 220;
            this.dataGridViewRep.Columns["Mobile"].Width = 125;
            this.dataGridViewRep.Columns["Email"].Width = 190;

            this.dataGridViewDistributor.RowsDefaultCellStyle.BackColor = Color.LightBlue;
            this.dataGridViewDistributor.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
            this.dataGridViewDistributor.Columns["Name"].Width = 200;
            this.dataGridViewDistributor.Columns["Adress"].Width = 190;
            this.dataGridViewDistributor.Columns["Email"].Width = 65;
            this.dataGridViewDistributor.Columns["Phone"].Width = 100;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!FormValidate())
            {
                if (btnAdd.Text == "Add")
                {
                    OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\dsi.SL\\Documents\\Visual Studio 2017\\Projects\\MedicalClinic\\MedicalClinic\\MedicalClinic.accdb");
                    OleDbCommand cmd = con.CreateCommand();
                    con.Open();
                    cmd.CommandText = "Insert into PharmaceuticalCompanies(CompanyName,CompanyAddress,CompanyPhone,CompanyEmail)Values('" + txtComName.Text + "','" + txtComAddress.Text + "','" + txtComPhone.Text + "','" + txtComEmail.Text + "')";
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    con.Close();
                    string strID = RetrieveID();
                    AddRepData(strID);
                    AddDistributorData(strID);
                    MessageBox.Show("Record Successfully Saved", "Message");
                    FormRefresh();
                }
                else
                {
                    OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\dsi.SL\\Documents\\Visual Studio 2017\\Projects\\MedicalClinic\\MedicalClinic\\MedicalClinic.accdb");
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
                    MessageBox.Show("Record Successfuly Updated", "Message");

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
        }

        public void InitiateDatagrid()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Mobile");
            dt.Columns.Add("Email");
            dataGridViewRep.AllowUserToAddRows = true;
            dataGridViewRep.EditMode = DataGridViewEditMode.EditOnKeystroke;
            dataGridViewRep.DataSource = dt;
        }

        public void AddRepData(string strID)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\dsi.SL\\Documents\\Visual Studio 2017\\Projects\\MedicalClinic\\MedicalClinic\\MedicalClinic.accdb");
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
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\dsi.SL\\Documents\\Visual Studio 2017\\Projects\\MedicalClinic\\MedicalClinic\\MedicalClinic.accdb");
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
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\dsi.SL\\Documents\\Visual Studio 2017\\Projects\\MedicalClinic\\MedicalClinic\\MedicalClinic.accdb");
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
            }
            reader.Close();
            con.Close();
        }

        public string RetrieveID()
        {
            string strID = string.Empty;
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\dsi.SL\\Documents\\Visual Studio 2017\\Projects\\MedicalClinic\\MedicalClinic\\MedicalClinic.accdb");
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandText = "select * from PharmaceuticalCompanies where CompanyName = '" + txtComName.Text + "'";
            con.Open();
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    strID = reader["ID"].ToString();
                }
            }
            reader.Close();
            con.Close();

            return strID;
        }

        public void InitiateDistributorDatagrid()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Adress");
            dt.Columns.Add("Phone");
            dt.Columns.Add("Email");
            dataGridViewDistributor.AllowUserToAddRows = true;
            dataGridViewDistributor.EditMode = DataGridViewEditMode.EditOnKeystroke;
            dataGridViewDistributor.DataSource = dt;
        }

        public void AddDistributorData(string strID)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\dsi.SL\\Documents\\Visual Studio 2017\\Projects\\MedicalClinic\\MedicalClinic\\MedicalClinic.accdb");
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
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\dsi.SL\\Documents\\Visual Studio 2017\\Projects\\MedicalClinic\\MedicalClinic\\MedicalClinic.accdb");
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
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\dsi.SL\\Documents\\Visual Studio 2017\\Projects\\MedicalClinic\\MedicalClinic\\MedicalClinic.accdb");
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
            }
            reader.Close();
            con.Close();
        }

        private void txtComID_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtComID.Text))
            {
                OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\dsi.SL\\Documents\\Visual Studio 2017\\Projects\\MedicalClinic\\MedicalClinic\\MedicalClinic.accdb");
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
                MessageBox.Show("Company Name cannot be empty", "Message");
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
                    OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\dsi.SL\\Documents\\Visual Studio 2017\\Projects\\MedicalClinic\\MedicalClinic\\MedicalClinic.accdb");
                    string strDelete = "delete from PharmaceuticalCompanies where id=@id";
                    OleDbCommand cmd = new OleDbCommand(strDelete, con);
                    cmd.Parameters.AddWithValue("@id", txtComID.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    DeleteRepData(txtComID.Text);
                    DeleteDistributorData(txtComID.Text);

                    MessageBox.Show("Records Successfuly Deleted");

                    FormRefresh();
                    txtComID.Text = string.Empty;
                }
            }
        }
    }
}
