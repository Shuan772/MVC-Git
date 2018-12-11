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

    [Bind(Exclude = "ID")]
    public class Kategorien : DbContext
    {
        [ScaffoldColumn(false)]
        public uint ID { get; set; }

        [DisplayName("Bezeichnung")]
        [Required(ErrorMessage = "Titel ist benötigt")]
        [StringLength(25)]
        public string Bezeichnung { get; set; }

        [DisplayName("Bild")]
        public uint BildID { get; set; }

        [DisplayName("Kategorie")]
        public int Kategorie { get; set; }


        public static List<Kategorien> GetAll()
        {
            List<Kategorien> list = new List<Kategorien>(); // Liste initialisieren. mit list.Add(item) können der Liste Objekte passenden Typs hinzugefügt werden

            // info: das ist nicht das using aus den cshtml-Seiten
            MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString);

            try
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("", con))
                {
                    cmd.CommandText = "SELECT ID, Bezeichnung, Bild, Kategorie FROM Kategorien;";
                    var r = cmd.ExecuteReader();
                    while (r.Read())
                    {

                        Kategorien m = new Kategorien();
                        m.ID = UInt16.Parse(r["ID"].ToString());
                        m.Bezeichnung = r["Bezeichnung"].ToString();
                        if(r["Kategorie"].ToString() == "")
                        {
                            m.Kategorie = 0;
                        }
                        else
                        {
                            m.Kategorie = Convert.ToInt16(r["Kategorie"].ToString());
                        }


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


            return list; // letztlich die Liste zurückgeben, welche natürlich auch leer sein könnte!

        }

    }
}