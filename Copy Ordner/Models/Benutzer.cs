using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Web.Mvc;
using System.ComponentModel;


namespace DBWT_Paket_5.Models
{

    [Bind(Exclude = "Nummer")]
    public class Benutzer
    {
        [ScaffoldColumn(false)]
        public short Nummer { get; set; }

        [DisplayName("E-Mail")]
        [Required(ErrorMessage = "E-Mail ist benötigt")]
        [StringLength(100)]
        public string E_Mail { get; set; }

        [DisplayName("Letzter Login")]
        public DateTime? LetzterLogin { get; set; }

        [DisplayName("Nutzername")]
        [Required(ErrorMessage = "Nutzername ist benötigt")]
        [StringLength(30)]
        public string Nutzername { get; set; }

        [DisplayName("Geburtsdatum")]
        public DateTime? Geburtsdatum { get; set; }

        [DisplayName("Aktiv")]
        [Required(ErrorMessage = "Aktiv ist benötigt")]
        public bool Aktiv { get; set; }

        [DisplayName("Anlegedatum")]
        [Required(ErrorMessage = "Anlegedatum ist benötigt")]
        public DateTime? Anlegedatum { get; set; }

        [DisplayName("Alter")]
        public short Alter { get; set; }

        [DisplayName("Vorname")]
        [Required(ErrorMessage = "Vorname ist benötigt")]
        [StringLength(50)]
        public string Vorname { get; set; }

        [DisplayName("Nachname")]
        [Required(ErrorMessage = "Nachname ist benötigt")]
        [StringLength(25)]
        public string Nachname { get; set; }

        [DisplayName("Salt")]
        [Required(ErrorMessage = "Salt ist benötigt")]
        [StringLength(32)]
        public string Salt { get; set; }

        [DisplayName("Hash")]
        [Required(ErrorMessage = "Hash ist benötigt")]
        [StringLength(24)]
        public string Hash { get; set; }

        [DisplayName("ISA")]
        [Required(ErrorMessage = "ISA ist benötigt")]
        public string ISA { get; set; }

        /*ENUM("Gast" , "FH-Angehöriger")
         ENUM("ET", "INF", "ISE", "MCD", "WI")
         */

        public static string Rolle(Benutzer b)
        {
            string tempname = "";
            using (MySqlConnection con =
               new MySqlConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString))
            {
                try
                {

                    con.Open();
                    MySqlCommand commandSession = con.CreateCommand();

                    commandSession.CommandText = "Call Nutzerrolle(@nummer);";
                    commandSession.Parameters.AddWithValue("nummer", b.Nummer);

                    MySqlDataReader readerSession = commandSession.ExecuteReader();
                    //Rolle in Session übernehmen
                    if (readerSession.Read())
                    {
                        tempname = readerSession["Rolle"].ToString();
                    }

                    readerSession.Close();
                    
                }
                catch (Exception e)
                {
                    // Haltepunkt?
                    string ex = e.Message;

                }
                return tempname;
            }
        }

        public static bool SetLogin(Benutzer b)
        {   
            using (MySqlConnection con =
                new MySqlConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString))
            {
                try
                {

                    using (MySqlCommand cmd = new MySqlCommand("", con))
                    {
                        cmd.CommandText = "Update Benutzer Set LetzterLogin = @now WHERE Nutzername=@mid ";
                        cmd.Parameters.AddWithValue("mid", b.Nutzername);
                        cmd.Parameters.AddWithValue("now", DateTime.Now);
                        con.Open();
                        var r = cmd.ExecuteReader();
                    }

                }

                catch (Exception e)
                {
                    string ex = e.Message;
                    return false;
                }
             
            }
            return true;
        }
        public static Benutzer GetByNutzername(string Nutzername)
        {
            Benutzer m = new Benutzer();
            using (MySqlConnection con =
                new MySqlConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString))
            {
                try
                {
                    
                    using (MySqlCommand cmd = new MySqlCommand("", con))
                    {
                        cmd.CommandText = "SELECT Nummer, LetzterLogin, Nutzername, Aktiv, Vorname , Nachname , Salt , Hash ,ISA FROM Benutzer WHERE Nutzername= @mid ;";
                        cmd.Parameters.AddWithValue("mid", Nutzername);
                        con.Open();
                        var r = cmd.ExecuteReader();
                        if (r.Read())
                        {
							if(! r.HasRows)
							{ return m;}
                            m.Nummer = Int16.Parse(r["Nummer"].ToString());
                            m.Vorname = r["Vorname"].ToString();
                            m.Nachname = r["Nachname"].ToString();
                            m.Nutzername = r["Nutzername"].ToString();
                            
                            if (r["LetzterLogin"].ToString() != "")
                            {
                                m.LetzterLogin = Convert.ToDateTime(r["LetzterLogin"].ToString());
                            }
                            else
                            {
                                m.LetzterLogin = null;
                            }
                            m.Aktiv = Convert.ToBoolean(r["Aktiv"]);
                            m.Salt = r["Salt"].ToString();
                            m.Hash = r["Hash"].ToString();
                            m.ISA = r["ISA"].ToString();

                        }
                    }
                  
                }

                catch (Exception e)
                {
                    // Haltepunkt?
                    string ex = e.Message;
                    m.E_Mail = ex;
                }
                // using schließt die Verbindung auch wieder ;)
            }
            return m;
        }
        public static string VerifyBenutzer(string pw, string Nutzername)
        {
            Benutzer m = new Benutzer();
            m = Benutzer.GetByNutzername(Nutzername);
			if(m.Nutzername == null)
			{
				return "False";
			}
            if (m.Aktiv)
            {
                bool testhash = PasswordSecurity.PasswordStorage.VerifyPassword(pw, "sha1:64000:18:" + m.Salt + ":" + m.Hash);
                return testhash.ToString();
            }
            else
            {
                return "Account Inaktiv bitte wenden sie sich an einen Admin.";
            }

            //  Benutzer.SetLogin(test);
            // setzte login zeit.
            //Create 
            //Bei Create Type String 
            // muss grund zurück geben.
            //string result = PasswordSecurity.PasswordStorage.CreateHash(reader["salt"].ToString() + Request.Form["password"]);

        }

        public static bool Create(Benutzer  Neu, Gast gast, Mitarbeiter Ma, Studenten Stu , int State , string pw)
        {
            using (MySqlConnection con =
                new MySqlConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString))
            {
                con.Open();
                MySqlCommand countSQL = con.CreateCommand();
                countSQL.CommandText = "Select Count(Nummer) From Benutzer ;";
                MySqlDataReader r = countSQL.ExecuteReader();
                //TODO
                r.Read();
                int count = Int16.Parse(r["Count(Nummer)"].ToString());
                int id = 20 + count;
                con.Close();
                con.Open();
                var tr = con.BeginTransaction();
                try
                {// innerhalb der Connection con eine Transaktion beginnen
                   
                    using (MySqlCommand cmd = new MySqlCommand("", con))
                    {
                        cmd.Transaction = tr;
                        //BEnutzer
                        cmd.CommandText = "INsert INTO `Benutzer` (`Nummer`, `Vorname`, `Nachname`, `E-Mail`, `Nutzername`, `LetzterLogin`, `Anlegedatum`, `Geburtsdatum`, `Salt`, `Hash`, `Aktiv`) VALUES" +
                            " (@id, @Vornamne, @Nachname, @mail, @Username, null , @andate, @gbdate,  @Salt, @hash, 0);";
                        cmd.Parameters.AddWithValue("id", id);
                        cmd.Parameters.AddWithValue("Vornamne", Neu.Vorname);
                        cmd.Parameters.AddWithValue("Nachname", Neu.Nachname);
                        cmd.Parameters.AddWithValue("mail", Neu.E_Mail);
                        cmd.Parameters.AddWithValue("Username", Neu.Nutzername);
                        cmd.Parameters.AddWithValue("andate", DateTime.Now);
                        cmd.Parameters.AddWithValue("gbdate", Neu.Geburtsdatum);
                        string pwhasstring = PasswordSecurity.PasswordStorage.CreateHash(pw);
                        string[] pwarray = pwhasstring.Split(':');
                        cmd.Parameters.AddWithValue("Salt", pwarray[3]);
                        cmd.Parameters.AddWithValue("hash", pwarray[4]);

                        int rows = cmd.ExecuteNonQuery(); // DML
                        if(Neu.ISA == "Gast")
                        {
                            //gast
                            cmd.CommandText = "INSERT INTO `Gäste` (ID , Grund , Ablaufdatum) Values (@id, @g, @dat); ";

                            cmd.Parameters.AddWithValue("g", gast.Grund);
                            cmd.Parameters.AddWithValue("dat", gast.Ablaufdatum);
                            rows = cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            if(State == 1 || State == 3)
                            {
                                cmd.CommandText = "INSERT INTO `FHAngehörige` (`ID`) VALUES (@id);";
                                //params

                               

                                rows = cmd.ExecuteNonQuery();
                                //FH angehörige
                                cmd.CommandText = "INSERT INTO `Student` (Matrikelnummer, Studiengang , ID) VALUES (@mat, @stugang, @id);";
                                //params
                                cmd.Parameters.AddWithValue("mat", Stu.Matrikelnummer);
                                cmd.Parameters.AddWithValue("stugang", Stu.Studiengang);

                                rows = cmd.ExecuteNonQuery();
                                //student
                            }
                            if (State == 2 || State == 3)
                            {
                                if(State == 2 )
                                {
                                    cmd.CommandText = "INSERT INTO `FHAngehörige` (`ID`) VALUES (@id);";
                                    //params
                                    rows = cmd.ExecuteNonQuery();
                                    //FH angehörige
                                }
                                cmd.CommandText = "INSERT INTO `Mitarbeiter` (`büro`, Telefon , ID) VALUES ( @b, @T, @id); ";
                                //params
                                cmd.Parameters.AddWithValue("T", Ma.Telefon);
                                cmd.Parameters.AddWithValue("b", Ma.Büro);

                                rows = cmd.ExecuteNonQuery();
                                //MA
                            }
                        }
                        // alle fehlerfrei?  commit!
                        tr.Commit();
                        // falls es Probleme gab
                      

                    }

                }

                catch (Exception e)
                {
                    tr.Rollback();
                    string ex = e.Message;
                    return false;
                }

            }
            return true;
        }










    }
    }

 