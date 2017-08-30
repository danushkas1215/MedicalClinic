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
    public partial class Patients : Form
    {
        public Patients()
        {
            InitializeComponent();
            comBloodGroup.SelectedIndex = 0;
            comSex.SelectedIndex = 0;
            comMaritalStatus.SelectedIndex = 0;
        }

        private void txtHomePhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Add)))
                e.Handled = true;
        }

        private void txtBirthday_ValueChanged(object sender, EventArgs e)
        {
            TimeSpan ts = DateTime.Now - Convert.ToDateTime(txtBirthday.Value);
            int intAge = Convert.ToInt32(ts.Days) / 365;
            txtAge.Text = intAge.ToString();
        }

        private void txtBirthday_DropDown(object sender, EventArgs e)
        {
            txtBirthday.ValueChanged -= txtBirthday_ValueChanged;
        }

        private void txtBirthday_CloseUp(object sender, EventArgs e)
        {
            txtBirthday.ValueChanged += txtBirthday_ValueChanged;
            txtBirthday_ValueChanged(sender, e);
        }

        public Boolean FormValidate()
        {
            bool blnValidated = false;
            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                blnValidated = true;
                MessageBox.Show("Patient Name cannot be empty", "Message");
            }
            return blnValidated;
        }

        public void FormRefresh()
        {
            txtName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtBirthday.Value = System.DateTime.Today;
            txtAge.Text = string.Empty;
            txtHomePhone.Text = string.Empty;
            txtMobile.Text = string.Empty;
            comMaritalStatus.SelectedIndex = 0;
            txtNIC.Text = string.Empty;
            txtEmail.Text = string.Empty;
            comBloodGroup.SelectedIndex = 0;
            comSex.SelectedIndex = 0;
            txtOccupation.Text = string.Empty;
            btnAdd.Text = "Add";
            //txtID.Text = "";
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
                    cmd.CommandText = "Insert into Patients(FullName, Birthday, Address, HomePhone, Mobile, Sex, BloodGroup, MaritalStatus, NIC, Email, Age, Occupation)" +
                        "Values('" + txtName.Text + "', '" + txtBirthday.Text + "', '" + txtAddress.Text + "', '" + txtHomePhone.Text + "', '" + txtMobile.Text + "'" +
                        ", '" + comSex.SelectedItem.ToString() + "', '" + comBloodGroup.SelectedItem.ToString() + "', '" + comMaritalStatus.SelectedItem.ToString() + "'" +
                        ", '" + txtNIC.Text + "', '" + txtEmail.Text + "', '" + txtAge.Text + "', '" + txtOccupation.Text + "')";
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "Select @@Identity";
                    int intId = (int)cmd.ExecuteScalar();
                    con.Close();
                    MessageBox.Show("Record Successfully Saved", "Message");
                    LoggingHelper.LogEntry("Patients", "Add", txtName.Text + "|" + txtBirthday.Text + "|" + txtAddress.Text + "|" + txtHomePhone.Text + "|" + txtMobile.Text + 
                        "|" + comSex.SelectedItem.ToString() + "|" + comBloodGroup.SelectedItem.ToString() + "|" + comMaritalStatus.SelectedItem.ToString() + 
                        "|" + txtNIC.Text + "|" + txtEmail.Text + "|" + txtAge.Text + "|" + txtOccupation.Text, intId);

                    FormRefresh();
                }
                else
                {
                    string conString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
                    OleDbConnection con = new OleDbConnection(conString);
                    con.Open();
                    string strUpdate = "update Patients set FullName='" + txtName.Text.Trim() + "', Birthday = '" + txtBirthday.Text + "' " +
                        ", Address = '" + txtAddress.Text + "', HomePhone = '" + txtHomePhone.Text + "', Mobile = '" + txtMobile.Text + "' " +
                        ", Sex = '" + comSex.SelectedItem.ToString() + "', BloodGroup = '" + comBloodGroup.SelectedItem.ToString() + "' " +
                        ", MaritalStatus = '" + comMaritalStatus.SelectedItem.ToString() + "', NIC = '" + txtNIC.Text + "' " +
                        ", Email = '" + txtEmail.Text + "', Age = '" + txtAge.Text + "', Occupation = '" + txtOccupation.Text + "' where ID=" + txtID.Text.Trim() + "";
                    OleDbCommand cmd = new OleDbCommand(strUpdate, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    LoggingHelper.LogEntry("Patients", "Update", txtName.Text + "|" + txtBirthday.Text + "|" + txtAddress.Text + "|" + txtHomePhone.Text + "|" + txtMobile.Text +
                        "|" + comSex.SelectedItem.ToString() + "|" + comBloodGroup.SelectedItem.ToString() + "|" + comMaritalStatus.SelectedItem.ToString() +
                        "|" + txtNIC.Text + "|" + txtEmail.Text + "|" + txtAge.Text + "|" + txtOccupation.Text, int.Parse(txtID.Text.Trim()));
                    MessageBox.Show("Record Successfuly Updated", "Message");

                    FormRefresh();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FormRefresh();
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
                    txtBirthday.Value = DateTime.Parse(reader["Birthday"].ToString());
                    txtAddress.Text = reader["Address"].ToString();
                    txtHomePhone.Text = reader["HomePhone"].ToString();
                    txtMobile.Text = reader["Mobile"].ToString();
                    comSex.SelectedItem = reader["Sex"].ToString();
                    comBloodGroup.SelectedItem = reader["BloodGroup"].ToString();
                    comMaritalStatus.SelectedItem = reader["MaritalStatus"].ToString();
                    txtNIC.Text = reader["NIC"].ToString();
                    txtEmail.Text = reader["Email"].ToString();
                    txtAge.Text = reader["Age"].ToString();
                    txtOccupation.Text = reader["Occupation"].ToString();
                }
            }
            reader.Close();
            con.Close();
            btnAdd.Text = "Update";
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
                    string strDelete = "delete from Patients where id=@id";
                    OleDbCommand cmd = new OleDbCommand(strDelete, con);
                    cmd.Parameters.AddWithValue("@id", txtID.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Records Successfuly Deleted");
                    LoggingHelper.LogEntry("Patients", "Delete", txtName.Text + "|" + txtBirthday.Text + "|" + txtAddress.Text + "|" + txtHomePhone.Text + "|" + txtMobile.Text +
                        "|" + comSex.SelectedItem.ToString() + "|" + comBloodGroup.SelectedItem.ToString() + "|" + comMaritalStatus.SelectedItem.ToString() +
                        "|" + txtNIC.Text + "|" + txtEmail.Text + "|" + txtAge.Text + "|" + txtOccupation.Text, int.Parse(txtID.Text.Trim()));

                    FormRefresh();
                }
            }
        }
    }
}
