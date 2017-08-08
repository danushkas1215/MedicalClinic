﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalClinic
{
    public partial class Medicines : Form
    {
        public Medicines(string strID)
        {
            InitializeComponent();
            PopulateGeneticName();
            PopulateCompany();
            txtID.Text = strID;
        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\dsi.SL\\Documents\\Visual Studio 2017\\Projects\\MedicalClinic\\MedicalClinic\\MedicalClinic.accdb");
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandText = "select * from Medicines where ID = " + txtID.Text + "";
            con.Open();
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    txtMedicineName.Text = reader["MedicineName"].ToString();
                    comGenericName.SelectedValue = reader["MedGenericNameID"].ToString();
                    comCompany.SelectedValue = reader["MedCompanyID"].ToString();
                    txtContents.Text = reader["MedContents"].ToString();
                    txtPrice.Text = reader["MedPrice"].ToString();
                    txtUnits.Text = reader["MedUnits"].ToString();
                    txtUOM.Text = reader["MedUoM"].ToString();
                }
            }
            reader.Close();
            con.Close();
            btnAdd.Text = "Update";
        }

        public void PopulateGeneticName()
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\dsi.SL\\Documents\\Visual Studio 2017\\Projects\\MedicalClinic\\MedicalClinic\\MedicalClinic.accdb");
            OleDbCommand cmd = con.CreateCommand();
            con.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("select ID, GenericName from GenericNames", con);
            DataSet ds = new DataSet();
            da.Fill(ds, "GenericNames");
            comGenericName.DataSource = ds.Tables[0];
            comGenericName.ValueMember = "ID";
            comGenericName.DisplayMember = "GenericName";
            ds.Tables.Clear();
            con.Close();
        }

        public void PopulateCompany()
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\dsi.SL\\Documents\\Visual Studio 2017\\Projects\\MedicalClinic\\MedicalClinic\\MedicalClinic.accdb");
            OleDbCommand cmd = con.CreateCommand();
            con.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("select ID, CompanyName from PharmaceuticalCompanies", con);
            DataSet ds = new DataSet();
            da.Fill(ds, "PharmaceuticalCompanies");
            comCompany.DataSource = ds.Tables[0];
            comCompany.ValueMember = "ID";
            comCompany.DisplayMember = "CompanyName";
            ds.Tables.Clear();
            con.Close();
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
                    cmd.CommandText = "Insert into Medicines(MedicineName, MedGenericNameID, MedCompanyID, MedContents, MedPrice, MedUnits, MedUoM)" +
                        "Values('" + txtMedicineName.Text.Trim() + "'," +
                        " " +comGenericName.SelectedValue+ ", "+
                        " " + comCompany.SelectedValue + ", " +
                        " '" + txtContents.Text.Trim() + "', " +
                        " " + txtPrice.Text.Trim() + ", " +
                        " " + txtUnits.Text.Trim() + ", " +
                        " '" + txtUOM.Text.Trim() + "' )";
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
                    string strUpdate = "UPDATE Medicines SET MedicineName = '" + txtMedicineName.Text.Trim() + "', MedGenericNameID = " + comGenericName.SelectedValue + " " +
                        ", MedCompanyID = " + comCompany.SelectedValue + ", MedContents = '" + txtContents.Text.Trim() + "' " +
                        ", MedPrice = " + txtPrice.Text.Trim() + ", MedUnits = " + txtUnits.Text.Trim() + " " +
                        ", MedUoM = '" + txtUOM.Text.Trim() + "' WHERE ID = " + txtID.Text.Trim() + "";
                    OleDbCommand cmd = new OleDbCommand(strUpdate, con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Record Successfuly Updated", "Message");

                    FormRefresh();
                }
            }
        }

        public Boolean FormValidate()
        {
            bool blnValidated = false;
            if (string.IsNullOrEmpty(txtMedicineName.Text.Trim()))
            {
                blnValidated = true;
                MessageBox.Show("Medicine Name cannot be empty", "Message");
            }
            return blnValidated;
        }

        public void FormRefresh()
        {
            txtMedicineName.Text = "";
            comCompany.SelectedIndex = 0;
            comGenericName.SelectedIndex = 0;
            txtContents.Text = "";
            txtUOM.Text = "";
            txtUnits.Text = "0";
            txtPrice.Text = "0";
            btnAdd.Text = "Add";
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtUnits_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FormRefresh();
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
                    string strDelete = "DELETE FROM Medicines where id=@id";
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
