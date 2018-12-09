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

    public class Produkte
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int Vorrat { get; set; }
        [Required]
        public string Beschreibung { get; set; }


        public int Kategorie { get; set; }

        public bool Verfügbar { get; set; }
        /*
        public year PreisJahr { get; set; }

        FOREIGN KEY(Kategorie) REFERENCES Kategorien(ID),

        /*
        ID INT UNSIGNED PRIMARY KEY AUTO_INCREMENT,
Vorrat INT NOT NULL DEFAULT 0,
Beschreibung VARCHAR(255) NOT NULL,
Kategorie INT UNSIGNED,
FOREIGN KEY(Kategorie) REFERENCES Kategorien(ID),
	Verfügbar BOOL,
--	CHECK(Vorrat > 0),
	PreisJahr year*/
    /*
        public static List<Produkte> GetAll()
        {
            List<Produkte> list = new List<Produkte>(); // Liste initialisieren. mit list.Add(item) können der Liste Objekte passenden Typs hinzugefügt werden

            // info: das ist nicht das using aus den cshtml-Seiten
            using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["demoReader"].ConnectionString))
            {
                try
                {
                    con.Open();
                    using (MySqlCommand cmd = new MySqlCommand("", con))
                    {
                        cmd.CommandText = "SELECT ID, Vorrat, Beschreibung, Kategorie, Verfügbar, PreisJahr FROM Produkte";
                        var r = cmd.ExecuteReader();
                        while (r.Read())
                        {
                            // das eigentliche Mapping von DataReader zu Objekt vom Typ Mitarbeiter
                            Produkte m = new Produkte();
                            m.ID = Int16.Parse(r["ID"].ToString());
                            m.Vorrat = r["Vorrat"];
                            m.Beschreibung = r["Beschreibung"].ToString();
                            m.Kategorie = r["Kategorie"];


                            //ConvertToDate ist eine Methode, die zum instanziierten Objekt gehört - Beispiel: das Mitarbeiterobjekt kann DateTime aus MariaDB selbst konvertieren
                            m.Verfügbar = r["Verfügbar"];
                            m.PreisJahr = r["Preisjahr"];

                            // neu erzeugten Mitarbeiter nun der Liste hinzufügen, die am Ende zurückgegeben werden soll
                            list.Add(m);
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
            return list; // letztlich die Liste zurückgeben, welche natürlich auch leer sein könnte!


        }*/

    }
}