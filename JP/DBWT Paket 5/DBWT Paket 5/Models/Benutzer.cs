using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace DBWT_Paket_5.Models
{
    public class Benutzer
    {
        public int id { get; set; }
        public String name { get; set; }
        public String rolle { get; set; }
        public DateTime LetzterLogin;
        public bool success(string passwort, string user)
        {



            return true;

        }
        public DateTime GetLetzterLogin(string user)
        {
            const string constring = "Server=localhost;Database=praktikum;Uid=webapp;Pwd=webapp;";
            MySqlConnection con = new MySqlConnection(constring);
            con.Open();
            MySqlCommand cmd;
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT LetzterLogin FROM `fe-nutzer` WHERE Loginname = '" + @user + "';";
            MySqlDataReader r = cmd.ExecuteReader();

            try
            {
                r.Read();
                LetzterLogin = r.GetDateTime(0);

                r.Close();
                con.Close();

                return LetzterLogin;
            }
            catch (Exception)
            {
                LetzterLogin = default(DateTime);
                return LetzterLogin;
            }

        }

        public void SetLetzterLogin(string user)
        {
            const string constring = "Server=localhost;Database=praktikum;Uid=webapp;Pwd=webapp;";
            MySqlConnection con = new MySqlConnection(constring);
            con.Open();
            MySqlCommand cmd;
            cmd = con.CreateCommand();
            cmd.CommandText = "UPDATE `fe-nutzer` SET LetzterLogin = CURRENT_TIMESTAMP() WHERE Loginname = '" + user + "'; ";
            cmd.CommandText += "Update `fe-nutzer`SET Aktiv = 1 where Loginname = '" +user +"'";

            MySqlDataReader r = cmd.ExecuteReader();
            r.Read();
            r.Close();
            con.Close();
        }

    }
}