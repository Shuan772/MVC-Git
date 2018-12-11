using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using MySql.Data.MySqlClient;
namespace DBWT_Paket_5.Models
{
    public class Login
    {

      
        public string UserName { get; set; }

        public bool success { get; set; }
        public string Password { get; set; }

        public string role { get; set; }
        public bool RememberMe { get; set; }
        public bool isactiv(string user)
        {
            const string constring = "Server=localhost;Database=praktikum;Uid=webapp;Pwd=webapp;";
            MySqlConnection con = new MySqlConnection(constring);
            con.Open();
            MySqlCommand cmd;
            cmd = con.CreateCommand();
            cmd.CommandText = "select `fe-nutzer`.aktiv from `fe-nutzer` where `fe-nutzer`.Loginname = '" + user + "'";
            MySqlDataReader r = cmd.ExecuteReader();
            r.Read();
            int be = Convert.ToInt16(r["Aktiv"].ToString());
          
            if (be == 1) {
                con.Close(); return true;

            }

            else { con.Close(); return false; };
           
        }
        public bool isBE(string user)
        {
            const string constring = "Server=localhost;Database=praktikum;Uid=webapp;Pwd=webapp;";
            MySqlConnection con = new MySqlConnection(constring);
            con.Open();
            MySqlCommand cmd;
            cmd = con.CreateCommand();
            cmd.CommandText = "select `fe-nutzer`.BE from `fe-nutzer` where `fe-nutzer`.Loginname = '" +user+"'";
            MySqlDataReader r = cmd.ExecuteReader();
            r.Read();
            int be = Convert.ToInt16(r["BE"].ToString());
            if (be == 1)
            {
                con.Close(); return true;

            }

            else { con.Close(); return false; };

        }
        public static string rolle(string user)
        {
            const string constring = "Server=localhost;Database=praktikum;Uid=webapp;Pwd=webapp;";
            MySqlConnection con = new MySqlConnection(constring);
            con.Open();
            MySqlCommand cmd;
            cmd = con.CreateCommand();
            cmd.CommandText = "select `hash` as h, salt, stretch, algorithmus, matrikelnummer, mitarbeiternummer from `fe-nutzer` as fe left join gast on fe.NR = gast.ID left join `fh-angehörige` as fh on fe.NR = fh.ID left join student as s on s.ID = fh.ID left join mitarbeiter as ma on ma.fkfh=fh.ID where loginname = '" + user + "'";
            MySqlDataReader r = cmd.ExecuteReader();
            r.Read();
            if (!string.IsNullOrEmpty(r["matrikelnummer"].ToString()))
            {
                con.Close();
                return "student";
            }
            else if (!string.IsNullOrEmpty(r["mitarbeiternummer"].ToString()))
            {
                con.Close();
                return "mitarbeiter";
            }

            else { con.Close(); return "gast"; }
        }
        public static bool login(string hash,string user)
        {
            const string constring = "Server=localhost;Database=praktikum;Uid=webapp;Pwd=webapp;";
            MySqlConnection con = new MySqlConnection(constring);
            bool ret = false;
            con.Open();
            MySqlCommand cmd;
            cmd = con.CreateCommand();
            cmd.CommandText = "select `hash` as h, salt, stretch, algorithmus, matrikelnummer, mitarbeiternummer from `fe-nutzer` as fe left join gast on fe.NR = gast.ID left join `fh-angehörige` as fh on fe.NR = fh.ID left join student as s on s.ID = fh.ID left join mitarbeiter as ma on ma.fkfh=fh.ID where loginname = '" + user + "'";
            MySqlDataReader r = cmd.ExecuteReader();
            bool rows = r.Read();
            if (rows == false)
            {
                ret = false;
            }
            else
            {

                string dbhash = r["algorithmus"].ToString() + ":" + r["stretch"].ToString() + ":18:" + r["salt"].ToString() + ":" + r["h"].ToString();


                if (PasswordSecurity.PasswordStorage.VerifyPassword(hash, dbhash))
                {

                    /*    Session["username"] = user;
                        Session["role"] = "gast";
                        if (!string.IsNullOrEmpty(r["matrikelnummer"].ToString()))
                        {
                            Session["role"] = "student";
                        }
                        else if (!string.IsNullOrEmpty(r["mitarbeiternummer"].ToString()))
                        {
                            Session["role"] = "mitarbeiter";
                        }*/
                    ret = true;
                
          
              }
            }
            con.Close();
            return ret;
        }
    }
}