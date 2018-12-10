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

    public class Zutaten
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public bool Bio { get; set; }
        [Required]
        public bool Vegan { get; set; }
        [Required]
        public bool Vegetarisch { get; set; }
        [Required]
        public bool Glutenfrei { get; set; }
        [Required]
        public string Name { get; set; }


        /*
        ID INT UNSIGNED PRIMARY KEY CHECK(ID BETWEEN 9999 AND 100000),
	Bio BOOL NOT NULL DEFAULT 0,
	Vegan BOOL NOT NULL DEFAULT 0,
	Vegetarisch BOOL NOT NULL DEFAULT 0,
	Glutenfrei BOOL NOT NULL DEFAULT 0,
	Name VARCHAR(30) NOT NULL -- e
            */

        public static List<Zutaten> GetAll()
        {
            List<Zutaten> list = new List<Zutaten>(); // Liste initialisieren. mit list.Add(item) können der Liste Objekte passenden Typs hinzugefügt werden

            // info: das ist nicht das using aus den cshtml-Seiten
            MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString);
            
                try
                {
                    con.Open();
                    using (MySqlCommand cmd = new MySqlCommand("", con))
                    {
                        cmd.CommandText = "SELECT ID, Bio, Vegan, Vegetarisch, Glutenfrei, Name FROM Zutaten ORDER BY Bio DESC , Name ASC";
                        var r = cmd.ExecuteReader();
                        while (r.Read())
                        {

                            Zutaten m = new Zutaten();
                            m.ID = Int16.Parse(r["ID"].ToString());
                            m.Bio = Convert.ToBoolean(r["Bio"].ToString());
                            m.Vegan = Convert.ToBoolean(r["Vegan"].ToString());
                            m.Vegetarisch = Convert.ToBoolean(r["Vegetarisch"].ToString());
                            m.Glutenfrei = Convert.ToBoolean(r["Glutenfrei"].ToString());
                            m.Name = r["Name"].ToString();

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