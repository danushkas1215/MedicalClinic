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
            InitiateDatagrid();
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

                    MessageBox.Show("Record Successfuly Updated", "Message");

                    FormRefresh();
                    txtComID.Text = string.Empty;
                    txtComName.Enabled = true;
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
            btnDelete.Enabled = false;
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
                if (ro.Cells[0].Value != null)
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

        public void RetrieveRepData()
        {

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
                    MessageBox.Show("Records Successfuly Deleted");

                    FormRefresh();
                    txtComID.Text = string.Empty;
                }
            }
        }
    }
}
