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


        public static Bild GetByID(uint id)
        {
            Bild m = new Bild();
            using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString))
            {
                
                try
                {
                    con.Open();
                    using (MySqlCommand cmd = new MySqlCommand("", con))
                    {
                        string query = "Select b.ID , b.`Alt-Text` , b.Titel , b.`Binärdaten` From Mahlzeiten m Left Join mahlzeitenxbilder mxb on mxb.Mahlzeiten = m.ID Left Join Bilder b on b.ID = mxb.Bilder Where m.ID = @id;";
                        //TODO Parameter ID
                        cmd.CommandText = query;
                        var r = cmd.ExecuteReader();
                        while (r.Read())
                        {

                            m.ID = UInt16.Parse(r["ID"].ToString());
                            m.Alt_Text = r["Alt-Text"].ToString();
                            m.Titel = r["Titel"].ToString();
                            m.Binaerdaten = r["Binärdaten"] as byte[];
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
        public static List<Bild> GetProd()
        {
            List<Bild> BildL = new List<Bild>();
            using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString))
            {

                try
                {
                    con.Open();
                    using (MySqlCommand cmd = new MySqlCommand("", con))
                    {
                        string query = "Select b.ID , b.`Alt-Text` , b.Titel , b.`Binärdaten` From Mahlzeiten m Left Join mahlzeitenxbilder mxb on mxb.Mahlzeiten = m.ID Left Join Bilder b on b.ID = mxb.Bilder;";
                        MySqlCommand commandBild = new MySqlCommand(query, con);
                        MySqlDataReader rb = commandBild.ExecuteReader();
                        con.Open();
                        while (rb.Read())
                        {

                            Bild m = new Bild();
                            m.ID = UInt16.Parse(rb["ID"].ToString());
                            m.Alt_Text = rb["Alt-Text"].ToString();
                            m.Titel = rb["Titel"].ToString();
                            m.Binaerdaten = rb["Binärdaten"] as byte[];
                            BildL.Add(m);
                        }
                    }
                }
                catch (Exception e)
                {
                    Bild m = new Bild();
                    m.Titel = e.Message;
                    BildL.Add(m);
                }
            }
            return BildL;
        }
    }
}