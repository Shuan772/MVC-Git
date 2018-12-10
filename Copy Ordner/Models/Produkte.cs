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
    [Bind(Exclude = "ID")]
    public class Produkte
    {
        [ScaffoldColumn(false)]
        public uint ID { get; set; }

        [DisplayName("Vorrat")]
        [Required(ErrorMessage = "Vorrat ist benötigt")]
        public uint Vorrat { get; set; }


        [DisplayName("Beschreibung")]
        [Required(ErrorMessage = "Beschreibung ist benötigt")]
        [StringLength(255)]
        public string Beschreibung { get; set; }

        [DisplayName("Kategorie")]
        public uint Kategorie { get; set; }

        [DisplayName("Verfügbar")]
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

        public static Produkte GetByID()
        {
            Produkte a = new Produkte();
            return a;
        }

        public static List<Produkte> Filter()
        {
            List<Produkte> list = new List<Produkte>();
            return list;
        }

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
                        cmd.CommandText = "SELECT ID, Vorrat, Beschreibung, Kategorie, Verfügbar FROM Produkte";
                        var r = cmd.ExecuteReader();
                        while (r.Read())
                        {
                            // das eigentliche Mapping von DataReader zu Objekt vom Typ Mitarbeiter
                            Produkte m = new Produkte();
                            m.ID = UInt16.Parse(r["ID"].ToString());
                            m.Vorrat = UInt16.Parse(r["Vorrat"].ToString());
                            m.Beschreibung = r["Beschreibung"].ToString();
                            m.Kategorie = UInt16.Parse(r["Kategorie"].ToString());
                            m.Verfügbar =  Convert.ToBoolean(r["Verfügbar"].ToString());

                            //ConvertToDate ist eine Methode, die zum instanziierten Objekt gehört - Beispiel: das Mitarbeiterobjekt kann DateTime aus MariaDB selbst konvertieren

                            //m.PreisJahr = r["Preisjahr"];

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


        }

    }
}