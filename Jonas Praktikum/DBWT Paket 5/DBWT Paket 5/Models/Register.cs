using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using PasswordSecurity;

namespace DBWT_Paket_5.Models
{
    public class Register
    {
        public bool rolleGesetzt = false;
        public bool student;
        public bool mitarbeiter;
        public bool gast;
        public string rolle;

        public string benutzername;
        public string vorname;
        public string nachname;
        public string email;
        public string pw;
        public int matrikelnummer;
        public string studiengang;
        public string telefonnummer;
        public string buero;
        public string grund;

        public bool alert = false;
        public string alert_text_pw;
        public string alert_text_loginname;
        public string alert_text_mail;

        public bool erfolgreich = false;
        const string constring = "Server=localhost;Database=praktikum;Uid=webapp;Pwd=webapp;";

        //Registrierung. Es müssen keien Variablen übergeben werden, 
        //da reg direkt die Methode aufruft und zu diesem Zeitpunkt 
        // alle nötigen Variablen vorhanden sein sollten.
        public void Nutzer_Anlegen()
        {
            //INSERT BEFEHLE

            MySqlConnection con = new MySqlConnection(constring);
            MySqlCommand cmd;
            MySqlTransaction tr = null;

            //um festzustelle, ob Email oder Benutzername bereits vorhanden; select bewusst nicht benutzt
            string hash = PasswordStorage.CreateHash("test");
            hash = hash.Split(':')[4];

            try
            {
                cmd = con.CreateCommand();
                cmd.CommandText = "";

                con.Open();
                tr = con.BeginTransaction();

                string passwort = PasswordStorage.CreateHash(pw);

                string s = "insert into `fe-nutzer`";
                s += " (Loginname, `Email`, Vorname, Nachname, Algorithmus, Stretch, Salt, Hash)";
                s += " values ('" + benutzername + "','" + email + "','" + vorname + "','" + nachname + "','" + passwort.Split(':')[0] + "','" + passwort.Split(':')[1];
                s += "','" + passwort.Split(':')[3] + "','" + passwort.Split(':')[4] + "')";

                cmd.CommandText = s;

                cmd.ExecuteNonQuery();

                // ID vom Benutzer kurz holen
                s = "select NR from `fe-nutzer` where Loginname='" + benutzername + "'";
                cmd.CommandText = s;

                int id = Convert.ToInt32(cmd.ExecuteScalar());

                cmd.ExecuteNonQuery();
                if (student || mitarbeiter)
                {
                    s = "insert into `fh-angehörige`";
                    s += " (ID)";
                    s += " values(" + id + ")";
                    cmd.CommandText = s;

                    cmd.ExecuteNonQuery();
                }
                if (student)
                {
                    s = "insert into student";
                    s += " (ID,Matrikelnummer,Studiengang)";
                    s += " values(" + id + "," + matrikelnummer + ",'" + studiengang + "')";
                    cmd.CommandText = s;

                    cmd.ExecuteNonQuery();
                }
                if (mitarbeiter)
                {
                    s = "insert into mitarbeiter";
                    s += " (fkfh,Telefonnummer,`Büro`)";
                    s += " values(" + id + ",'" + telefonnummer + "','" + buero + "')";
                    cmd.CommandText = s;

                    cmd.ExecuteNonQuery();
                }
                if (gast)
                {
                    s = "insert into gast";
                    s += " (ID,Grund)";
                    s += " values(" + id + ",'" + grund + "')";
                    cmd.CommandText = s;

                    cmd.ExecuteNonQuery();
                }

                tr.Commit();
                con.Close();
            }
            catch (Exception e)
            {
                tr.Rollback();
                con.Close();

                throw e;
            }

            // ENDE
        }


        public bool rolle_pruefen_setzen(string rolle, Register reg)
        {
            reg.rolleGesetzt = true;
            if (rolle == "student")
            {
                reg.student = true;
                reg.rolle = "Student";
                return true;
            }
            else if (rolle == "mitarbeiter")
            {
                reg.mitarbeiter = true;
                reg.rolle = "Mitarbeiter";
                return true;
            }
            else if (rolle == "gast")
            {
                reg.gast = true;
                reg.rolle = "Gast";
                return true;
            }
            else
            {
                reg.rolleGesetzt = false;
                return false;
            }
        }

        public bool eingabe_korret(string pw1, string pw2, string loginname, string mail, Register reg)
        {
            // Passwort Prüfen
            if (pw1 != pw2)
            {
                reg.alert_text_pw = "Passwörter stimmen nicht überein!";
                reg.alert = true;
            }
            else
            {
                reg.pw = pw1;
            }

            //Loginname Prüfen

           
            MySqlConnection con = new MySqlConnection(constring);
            con.Open();
            MySqlCommand cmd;
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT Loginname FROM `fe-nutzer` WHERE Loginname = '" + loginname + "';";
            MySqlDataReader r = cmd.ExecuteReader();
            if (r.Read())
            {
                reg.alert_text_loginname = "Loginname schon vorhanden. Bitte wähle einen anderen.";
                reg.alert = true;
            }
            else
            {
                reg.benutzername = loginname;
            }
            r.Close();
            con.Close();


            //E-Mail Prüfen

            MySqlConnection con2 = new MySqlConnection(constring);
            con2.Open();
            MySqlCommand cmd2;
            cmd2 = con2.CreateCommand();
            cmd2.CommandText = "SELECT `EMail` FROM `fe-nutzer` WHERE `EMail` = '" + mail + "';";
            MySqlDataReader r2 = cmd2.ExecuteReader();
            if (r2.Read())
            {
                reg.alert_text_mail = "Die angegebene E-Mail ist bereits vergeben. Bitte geben Sie eine andere an oder "; //Actionlink muss folgen
                reg.alert = true;
            }
            else
            {
                reg.email = mail;
            }
            r2.Close();
            con2.Close();

            //alert abfragen
            if (reg.alert)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}