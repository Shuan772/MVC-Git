using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;


namespace DBWT_Paket_5.Models
{


    public class Benutzer
    {
        [Key]
        public short Nummer { get; set; }
        [Required]
        public string E_Mail { get; set; }
        public DateTime LetzterLogin { get; set; }
        [Required]
        public string Nutzername { get; set; }
        public DateTime Geburtsdatum { get; set; }
        [Required]
        public bool Aktiv { get; set; }
        [Required]
        public DateTime Anlegedatum { get; set; }
        public short Alter { get; set; }
        [Required]
        public string Vorname { get; set; }
        [Required]
        public string Nachname { get; set; }
        [Required]
        public string Salt { get; set; }
        [Required]
        public string Hash { get; set; }
        [Required]
        public string Rolle { get; set; }
        public string Grund { get; set; }
        public DateTime Ablaufdatum { get; set; }
        private short Matrikelnummer { get; set; }
        public string Studiengang { get; set; }
        public string Büro { get; set; }
        public string Telefon { get; set; }

        /*ENUM("Gast" , "FH-Angehöriger")
         ENUM("ET", "INF", "ISE", "MCD", "WI")
         */
        public static Benutzer GetByNutzername(string Nutzername)
        {
            Benutzer m = new Benutzer();
            using (MySqlConnection con =
                new MySqlConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString))
            {
                try
                {
                    con.Open();
                    using (MySqlCommand cmd = new MySqlCommand("", con))
                    {
                        cmd.CommandText = "SELECT Nummer, LetzterLogin, Nutzername, Aktiv, Vorname , Nachname , Salt , Hash ,ISA FROM Benutzer WHERE Nutzername=@mid ";
                        cmd.Parameters.AddWithValue("mid", Nutzername);

                        var r = cmd.ExecuteReader();
                        if (r.Read())
                        {
                            m.Nummer = Int16.Parse(r["Nummer"].ToString());
                            m.Vorname = r["Vorname"].ToString();
                            m.Nachname = r["Nachname"].ToString();
                            m.Nutzername = r["Nutzername"].ToString();
                            m.LetzterLogin = Convert.ToDateTime(r["LetzterLogin"]);
                            m.Aktiv = Convert.ToBoolean(r["Aktiv"]);
                            m.Salt = r["Salt"].ToString();
                            m.Hash = r["Hash"].ToString();
                            m.Rolle = r["ISA"].ToString();
                        }
                    }
                    con.Close();
                    con.Open();
                    using (MySqlCommand cmd = new MySqlCommand("", con))
                    {
                        if (m.Rolle == "Gast")
                        {
                            cmd.CommandText = "SELECT Grund,Ablaufdatum FROM `gäste` WHERE Nummer =@mid ";
                        }
                        else
                        {
                            cmd.CommandText = "SELECT s.id m.Telefon, m.`büro`, s.Studiengang  FROM Benutzer b LEFT JOIN Mitarbeiter m on m.id = b.nummer LEFT JOIN Student s on s.id = b.nummer WHERE Nummer =@mid ";
                        }
                        cmd.Parameters.AddWithValue("mid", m.Nummer);

                        var r = cmd.ExecuteReader();
                        if (r.Read())
                        {
                            if (m.Rolle == "Gast")
                            {
                                m.Grund = r["Grund"].ToString();
                                m.Rolle = "Gast";
                                m.Ablaufdatum = Convert.ToDateTime(r["Ablaufdatum"]);
                            }
                            else
                            {
                                if (String.IsNullOrEmpty(r["s.id"].ToString()))
                                {
                                    m.Rolle = "Mitarbeiter";
                                    m.Büro = r["m.büro"].ToString();
                                    m.Telefon = r["m.Telefon"].ToString();
                                }
                                else
                                {
                                    m.Rolle = "Student";
                                    m.Studiengang = r["s.Studiengang"].ToString();
                                }
                            }
                        }
                    }
                }

                catch (Exception e)
                {
                    // Haltepunkt?
                    string ex = e.Message;
                }
                // using schließt die Verbindung auch wieder ;)
            }
            return m;
        }
        public static string VerifyBenutzer(string pw, string Nutzername)
        {
            Benutzer m = new Benutzer();
            m = Benutzer.GetByNutzername(Nutzername);

            if (m.Aktiv)
            {
                bool testhash = PasswordSecurity.PasswordStorage.VerifyPassword(pw, "sha1:64000:18:" + m.Salt + ":" + m.Hash);
                return testhash.ToString();
            }
            else
            {
                return "Account Inaktiv";
            }

            //Create 
            //Bei Create Type String 
            // muss grund zurück geben.
            //string result = PasswordSecurity.PasswordStorage.CreateHash(reader["salt"].ToString() + Request.Form["password"]);

        }












    }
    }

 