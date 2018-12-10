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
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class Bild : DbContext
    {
        [Key]
        public uint ID { get; set; }
        [Required]
        public string Alt_Text { get; set; }

        public string Titel { get; set; }
        [Required]
        public byte[] Binaerdaten { get; set; }


        public static Bild GetByID()
        {
            Bild m = new Bild();
            // info: das ist nicht das using aus den cshtml-Seiten
            using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["demoReader"].ConnectionString))
            {
                
                try
                {
                    con.Open();
                    using (MySqlCommand cmd = new MySqlCommand("", con))
                    {
                        cmd.CommandText = "SELECT ID, Alt-Text, Titel, Binärdaten FROM Bilder";
                        var r = cmd.ExecuteReader();
                        while (r.Read())
                        {
                            // das eigentliche Mapping von DataReader zu Objekt vom Typ Mitarbeiter
                            m.ID = UInt16.Parse(r["ID"].ToString());
                            m.Alt_Text = r["Vorrat"].ToString();
                            m.Titel = r["Beschreibung"].ToString();
                            m.Binaerdaten = r["Binärdaten"] as byte[];

                            //ConvertToDate ist eine Methode, die zum instanziierten Objekt gehört - Beispiel: das Mitarbeiterobjekt kann DateTime aus MariaDB selbst konvertieren

                            //m.PreisJahr = r["Preisjahr"];

                            // neu erzeugten Mitarbeiter nun der Liste hinzufügen, die am Ende zurückgegeben werden soll
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
            return m; // letztlich die Liste zurückgeben, welche natürlich auch leer sein könnte!


        }
    }
}