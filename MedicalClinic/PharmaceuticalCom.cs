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
        public PharmaceuticalCom()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\dsi.SL\\Documents\\Visual Studio 2017\\Projects\\MedicalClinic\\MedicalClinic\\MedicalClinic.accdb");
            OleDbCommand cmd = con.CreateCommand();
            con.Open();
            cmd.CommandText = "Insert into PharmaceuticalCompanies(CompanyName,CompanyAddress)Values('" + textBox1.Text + "','" + textBox2.Text + "')";
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            MessageBox.Show("Record Submitted", "Congrats");
            con.Close();
        }
    }
}
