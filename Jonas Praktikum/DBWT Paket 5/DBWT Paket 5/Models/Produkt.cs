using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace DBWT_Paket_5.Models
{
    public class Produkt
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public String Beschreibung { get; set; }
        public String text { get; set; }
        public  byte[] binaer {get;set;}
        public List<String> Kategorien { get; set; }

        public double Preis { get; set; }
        
        public static List<String> kats()
        {
            List<String> kategorien = null;
            const string constring = "Server=localhost;Database=praktikum;Uid=webapp;Pwd=webapp;";
            MySqlConnection kats = new MySqlConnection(constring);

            kats.Open();
            MySqlCommand kat;
            kat = kats.CreateCommand();
            kat.CommandText = "Select kategorie.Bezeichnung from kategorie;";
            MySqlDataReader z = kat.ExecuteReader();

            while (z.Read())
            {
                kategorien.Add(z["Bezeichnung"].ToString());
                            }
            z.Close();
            kat.Clone();
            return kategorien;
        }

        public static List<Produkt> produktnachkat(int katid)
        {
            const string constring = "Server=localhost;Database=praktikum;Uid=webapp;Pwd=webapp;";

            List<Produkt> result = new List<Produkt>();
            MySqlConnection con = new MySqlConnection(constring); // lässt sich per using(){} noch besser handhaben
            MySqlConnection con2 = new MySqlConnection(constring); // lässt sich per using(){} noch besser handhaben
         
                con.Open();
                con2.Open();

                MySqlCommand cmd;
                MySqlCommand cmd2;

                cmd2 = con2.CreateCommand();
                cmd = con.CreateCommand();
                if (katid > 0)
                {
                    cmd.CommandText = "with recursive kat as (select k.id, k.bezeichnung, k.oberkategorie from kategorie as k";

                    // einfachstes Beispiel für eine Anpassung der Query je nach Anwendungszustand (ist ein parameter vorhanden zB)
                    if (katid > 0)
                    {
                        cmd.CommandText += " WHERE id = " + katid;
                    }
                    cmd.CommandText += " union all select k1.id, k1.bezeichnung, k2.bezeichnung from kategorie as k1 join kat as k2 on k2.id = k1.Oberkategorie) select distinct p.ID, beschreibung, Bezeichnung, Binaerdaten from produkt as p join kat as k on k.ID=p.katid left join bild on bild.id=p.bildid where p.katid=k.ID order by k.id";


                // hier wird nun zum DBMS gesendet und die Antwortrelation vorbereitet

                MySqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    Produkt p = new Produkt();
                    p.ID = Convert.ToInt16(r["ID"]);
                    p.Name = r["beschreibung"].ToString();
                    p.Beschreibung = r["Bezeichnung"].ToString();
                    p.binaer = r["Binaerdaten"] as byte[]; 
                    result.Add(p);
                }



            }
                else
                {
                    cmd.CommandText = "select produkt.id, produkt.Beschreibung , bild.Alttext , bild.Binaerdaten from produkt inner join bild on produkt.bildid = bild.id order by RAND()";
                MySqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    Produkt p = new Produkt();
                    p.ID = Convert.ToInt16(r["ID"]);
                    p.Name = r["beschreibung"].ToString();
                    p.Beschreibung = r["Alttext"].ToString();
                    p.binaer = r["Binaerdaten"] as byte[];
                    result.Add(p);
                }
            }


            con.Close();
            con2.Close();
                
                return result;
            

           
        }
        public static Produkt detail(int produktid)
        {
            Produkt p = new Produkt();
            const string constring = "Server=localhost;Database=praktikum;Uid=webapp;Pwd=webapp;";

            MySqlConnection con = new MySqlConnection(constring);
            con.Open();
            MySqlCommand cmd;
            cmd = con.CreateCommand();
            cmd.CommandText = "select p.beschreibung, bild.titel, pr.Studentenbetrag, pr.Mitarbeiterbetrag, pr.Gastbetrag, bild.binaerdaten as bidat from produkt p join preis as pr on p.ID = pr.ID left join bild on bild.id = p.bildid";

            // einfachstes Beispiel für eine Anpassung der Query je nach Anwendungszustand (ist ein parameter vorhanden zB)
            if (produktid > 0)
            {
                cmd.CommandText += " WHERE p.id = " + produktid + " limit 1";
            }
            MySqlDataReader r = cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
            r.Read();
            p.binaer = r["bidat"] as byte[];
            p.Beschreibung = r["beschreibung"].ToString();

            p.Preis = Convert.ToDouble(r["Gastbetrag"]);
           
            p.text =r["titel"].ToString();
            con.Close();
            return p;
        }
    }
}