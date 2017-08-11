using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MedicalClinic
{
    public class LoggingHelper
    {
        public static void LogEntry(string strModule, string strAction, string strDesc, int intItemId)
        {
            string conString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            OleDbConnection con = new OleDbConnection(conString);
            OleDbCommand cmd = con.CreateCommand();
            con.Open();
            cmd.CommandText = "INSERT INTO LogTracker (AppModule, AppAction, AppDescription, AppActionDate, ItemId) VALUES " +
                "('"+ strModule +"', '"+ strAction +"', '"+ strDesc +"', '"+ DateTime.Now +"', "+ intItemId +")";
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
