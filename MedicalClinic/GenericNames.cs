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
    public partial class GenericNames : Form
    {
        public GenericNames(string strID)
        {
            InitializeComponent();
            txtID.Text = strID;
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
                    cmd.CommandText = "Insert into GenericNames(GenericName)Values('" + txtGenericName.Text + "')";
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record Successfully Saved", "Message");
                    con.Close();

                    FormRefresh();
                }
                else
                {
                    OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\dsi.SL\\Documents\\Visual Studio 2017\\Projects\\MedicalClinic\\MedicalClinic\\MedicalClinic.accdb");
                    con.Open();
                    string strUpdate = "update GenericNames set GenericName='" + txtGenericName.Text.Trim() + "' where ID=" + txtID.Text.Trim() + "";
                    OleDbCommand cmd = new OleDbCommand(strUpdate, con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Record Successfuly Updated", "Message");

                    FormRefresh();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FormRefresh();
        }

        public void FormRefresh()
        {
            txtGenericName.Text = "";
            btnAdd.Text = "Add";
            //txtID.Text = "";
        }

        public Boolean FormValidate()
        {
            bool blnValidated = false;
            if (string.IsNullOrEmpty(txtGenericName.Text.Trim()))
            {
                blnValidated = true;
                MessageBox.Show("Generic Name cannot be empty", "Message");
            }
            return blnValidated;
        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\dsi.SL\\Documents\\Visual Studio 2017\\Projects\\MedicalClinic\\MedicalClinic\\MedicalClinic.accdb");
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandText = "select * from GenericNames where ID = " + txtID.Text + "";
            con.Open();
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    txtGenericName.Text = reader["GenericName"].ToString();
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
                    OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\dsi.SL\\Documents\\Visual Studio 2017\\Projects\\MedicalClinic\\MedicalClinic\\MedicalClinic.accdb");
                    string strDelete = "delete from GenericNames where id=@id";
                    OleDbCommand cmd = new OleDbCommand(strDelete, con);
                    cmd.Parameters.AddWithValue("@id", txtID.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Records Successfuly Deleted");

                    FormRefresh();
                }
            }
        }
    }
}
