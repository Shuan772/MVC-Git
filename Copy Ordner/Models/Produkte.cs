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
        public int Vorrat { get; set; }

        [DisplayName("Beschreibung")]
        [Required(ErrorMessage = "Beschreibung ist benötigt")]
        [StringLength(255)]
        public string Beschreibung { get; set; }

        [DisplayName("Kategorie")]
        public int Kategorie { get; set; }

        [DisplayName("Verfügbar")]
        public bool Verfügbar { get; set; }
        [Required]
        public string Alt_Text { get; set; }

        public string Titel { get; set; }
        [Required]
        public byte[] Binaerdaten { get; set; }

        public string Name { get; set; }

        public static Produkte GetByID(uint id)
        {
            Produkte m = new Produkte();
            using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString))
            {
                try
                {
                    con.Open();
                    using (MySqlCommand cmd = new MySqlCommand("", con))
                    {
                        cmd.CommandText = "SELECT DISTINCT mahl.ID, mahl.Vorrat , mahl.Name  , mahl.Beschreibung  ,bild.`ALt-Text` , bild.`Titel`, bild.`Binärdaten` From Mahlzeiten mahl JOIN MahlzeitenXBilder mxb ON mahl.ID = mxb.Mahlzeiten JOIN Bilder bild ON bild.ID = mxb.Bilder WHERE mahl.ID = @id;";
                        //TODO add parameters
                        cmd.Parameters.AddWithValue("id", id);
                        var r = cmd.ExecuteReader();
                        while (r.Read())
                        {
                            m.ID = UInt16.Parse(r["ID"].ToString());
                            m.Vorrat = Int16.Parse(r["Vorrat"].ToString());
                            m.Beschreibung = r["Beschreibung"].ToString();
                            m.Name = r["Name"].ToString();
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
                    m.Beschreibung = e.Message;
                }
                // using schließt die Verbindung auch wieder ;)
            }
            return m; // letztlich die Liste zurückgeben, welche natürlich auch leer sein könnte!


        }
        public static List<Produkte> Filter(string tname, bool tvege, bool tvega, bool tvorrat)
        {
            MySqlConnection con = null;
            List<Produkte> ProdukteL = new List<Produkte>();
            try
            {
                using (con = new MySqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
                {

                    con.Open();
                    bool vege = false;
                    bool vega = false;
                    string query;
                    string querystart = "";
                    string queryjoin = "";
                    string queryend = "";


                    query = "SELECT DISTINCT mahl.ID, mahl.Vorrat , mahl.Name  , mahl.Beschreibung , mahl.Kategorie  , bild.`ALt-Text` , bild.`Titel`, bild.`Binärdaten` From Mahlzeiten mahl JOIN MahlzeitenXBilder mxb ON mahl.ID = mxb.Mahlzeiten JOIN Bilder bild ON bild.ID = mxb.Bilder";

                    if (tname != null && tname != "0")
                    {
                        querystart = querystart + " Join Kategorien kat on mahl.Kategorie ='" + tname + "'"; //ID = Bezeichnung
                    }
                    if (tvorrat)
                    {
                        queryjoin = queryjoin + " WHERE mahl.Vorrat > '0'"; //ID = Bezeichnung
                    }
                    if (tvege)
                    {
                        if (queryjoin == "")
                        {
                            queryjoin = queryjoin + " WHERE NOT EXISTS (Select * FROM zutaten z Join zutatenxmahlzeiten x on z.ID = x.Zutaten where (z.vegetarisch = 0 ";
                        }
                        else
                        {
                            queryjoin = queryjoin + "AND NOT EXISTS (Select * FROM zutaten z Join zutatenxmahlzeiten x on z.ID = x.Zutaten where (z.vegetarisch = 0 ";
                        }
                        vege = true;
                    }
                    if (tvega)
                    {
                        if (queryjoin == "")
                        {
                            queryjoin = queryjoin + "  WHERE NOT EXISTS (Select * FROM zutaten z Join zutatenxmahlzeiten x on z.ID = x.Zutaten where (z.vegan = 0 ";
                        }
                        else if (vege)
                        {
                            queryjoin = queryjoin + " OR  z.vegan = 0 ";
                        }
                        else
                        {
                            queryjoin = queryjoin + " AND NOT EXISTS (Select * FROm zutaten z Join zutatenxmahlzeiten x on z.ID = x.Zutaten where (z.vegan = 0 ";
                        }
                        vega = true;
                    }
                    if (vega || vege)
                    {
                        queryend = ") AND x.Mahlzeiten = mahl.ID) ";
                    }
                    query = query + querystart + queryjoin + queryend + ";";

                    MySqlCommand commandPro = new MySqlCommand(query, con);
                    MySqlDataReader r = commandPro.ExecuteReader();


                    while (r.Read())
                    {

                        Produkte m = new Produkte();
                        m.ID = UInt16.Parse(r["ID"].ToString());
                        m.Vorrat = UInt16.Parse(r["Vorrat"].ToString());
                        m.Beschreibung = r["Beschreibung"].ToString();
                        m.Name = r["Name"].ToString();
                        m.Kategorie = Convert.ToUInt16(r["Kategorie"].ToString());
                        m.Alt_Text = r["Alt-Text"].ToString();
                        m.Titel = r["Titel"].ToString();
                        m.Binaerdaten = r["Binärdaten"] as byte[];
                        ProdukteL.Add(m);
                    }

                    return ProdukteL;
                }
            }
            catch (Exception ex)
            {
                Produkte m = new Produkte();

                m.Beschreibung = ex.Message;
                return ProdukteL;
            }
        }
        public static double GetPrice(string Rolle , Produkte prod)
        {
            using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString))
            {
                try
                {

                    con.Open();
                    using (MySqlCommand cmd = new MySqlCommand("", con))
                    {

                        cmd.CommandText = "Select `MA-Preis` , `Studentpreis` ,`Gastpreis`From Preise where MahlzeitenID = @mahl AND Jahr = @jahr;"; 
                        cmd.Parameters.AddWithValue("mahl", prod.ID);
                        cmd.Parameters.AddWithValue("jahr", DateTime.Now.Year);

                        MySqlDataReader r = cmd.ExecuteReader();
                        r.Read();
                        //Rolle in Session übernehmen
                        if (Rolle == "Mitarbeiter")
                        {
                            if ((double)r["MA-Preis"] != 0.0)
                            {
                                return Convert.ToDouble(r["MA-Preis"]);
                            }
                            return Convert.ToDouble(r["Gastpreis"]);
                        }
                        else
                        {
                            if (Rolle == "Student")
                            {
                                if((double)r["Studentpreis"] != 0.0)
                                {
                                    return Convert.ToDouble(r["Studentpreis"]);
                                }
                                return Convert.ToDouble(r["Gastpreis"]);
                            }
                            else
                            {
                                return Convert.ToDouble(r["Gastpreis"]);
                            }
                        }
                    }
                }
                catch (Exception e)
                {

                    return 99.99;
                }
            }
        }
        public static List<Produkte> GetAll()
        {
            List<Produkte> list = new List<Produkte>();
            Produkte m = new Produkte();
            using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString))
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
                            m.ID = UInt16.Parse(r["ID"].ToString());
                            m.Vorrat = UInt16.Parse(r["Vorrat"].ToString());
                            m.Beschreibung = r["Beschreibung"].ToString();
                            m.Kategorie = UInt16.Parse(r["Kategorie"].ToString());
                            m.Verfügbar = Convert.ToBoolean(r["Verfügbar"].ToString());
                        }
                    }
                }

                catch (Exception e)
                {
                    // Haltepunkt?
                    string ex = e.Message;
                    m.Beschreibung = e.Message;
                }
                // using schließt die Verbindung auch wieder ;)
            }
            return list; // letztlich die Liste zurückgeben, welche natürlich auch leer sein könnte!
        }
    }
}
