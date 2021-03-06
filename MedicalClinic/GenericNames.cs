﻿using System;
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
                    string conString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
                    OleDbConnection con = new OleDbConnection(conString);
                    OleDbCommand cmd = con.CreateCommand();
                    con.Open();
                    cmd.CommandText = "Insert into GenericNames(GenericName)Values('" + txtGenericName.Text + "')";
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "Select @@Identity";
                    int intId = (int)cmd.ExecuteScalar();
                    con.Close();
                    MessageBox.Show("Record Successfully Saved", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoggingHelper.LogEntry("Generic Names", "Add", txtGenericName.Text, intId);

                    FormRefresh();
                }
                else
                {
                    string conString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
                    OleDbConnection con = new OleDbConnection(conString);
                    con.Open();
                    string strUpdate = "update GenericNames set GenericName='" + txtGenericName.Text.Trim() + "' where ID=" + txtID.Text.Trim() + "";
                    OleDbCommand cmd = new OleDbCommand(strUpdate, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    LoggingHelper.LogEntry("Generic Names", "Update", txtGenericName.Text, int.Parse(txtID.Text.Trim()));
                    MessageBox.Show("Record Successfully Updated", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
            btnDelete.Enabled = false;
            //txtID.Text = "";
        }

        public Boolean FormValidate()
        {
            bool blnValidated = false;
            if (string.IsNullOrEmpty(txtGenericName.Text.Trim()))
            {
                blnValidated = true;
                MessageBox.Show("Generic Name cannot be empty", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return blnValidated;
        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {
            string conString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            OleDbConnection con = new OleDbConnection(conString);
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
            btnDelete.Enabled = true;
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
                    string strDelete = "delete from GenericNames where id=@id";
                    OleDbCommand cmd = new OleDbCommand(strDelete, con);
                    cmd.Parameters.AddWithValue("@id", txtID.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Records Successfully Deleted", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoggingHelper.LogEntry("Generic Names", "Delete", txtGenericName.Text, int.Parse(txtID.Text.Trim()));
                    FormRefresh();
                }
            }
        }
    }
}
