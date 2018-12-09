using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using MySql.Data.MySqlClient;
namespace DBWT_Paket_5.Models
{
    public class kategorien
    {
        const string constring = "Server=localhost;Database=praktikum;Uid=webapp;Pwd=webapp;";
        public int id { get; set; }
        public string kategorie { get; set; }
        public List<kategorien> getkats()
        {
            List<kategorien> kateg = new List<kategorien>();
            MySqlConnection kats = new MySqlConnection(constring);

            kats.Open();
            MySqlCommand kat;
            kat = kats.CreateCommand();
            kat.CommandText = "Select kategorie.ID , kategorie.Bezeichnung from kategorie;";
            MySqlDataReader z = kat.ExecuteReader();
          //  kategorien k = new kategorien();
            while (z.Read())
            {
                kategorien k = new kategorien();
                k.id = Convert.ToInt32(z["ID"].ToString());
                k.kategorie = z["Bezeichnung"].ToString();
                kateg.Add(k);
                      //          < li >< a href = "~/Produkte/IndexProdukte/@z["ID"]" > @z["Bezeichnung"] </ a ></ li >
                            }
            z.Close();
            kat.Clone();
            return kateg;
        }
    }
}