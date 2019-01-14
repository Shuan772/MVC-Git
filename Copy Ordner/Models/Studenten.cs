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
    public class Studenten : DbContext
    {
        [ScaffoldColumn(false)]
        public uint ID { get; set; }

        [DisplayName("Matrikelnummer")]
        public int Matrikelnummer { get; set; }

        [DisplayName("Studiengang")]
        public string Studiengang { get; set; }


        public static bool exists(int matnr)
        {
            bool r = true;
            using (MySqlConnection con =
               new MySqlConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString))
            {
                try
                {

                    con.Open();
                    MySqlCommand commandSession = con.CreateCommand();

                    commandSession.CommandText = "SELECT ID , Matrikelnummer From Student WHERE Matrikelnummer =@matnr;";
                    commandSession.Parameters.AddWithValue("matnr", matnr);

                    MySqlDataReader readerSession = commandSession.ExecuteReader();
                    //Rolle in Session übernehmen
                    if (readerSession.Read())
                    {
                        if(readerSession["ID"].ToString() == "")
                        {
                            r = false;
                            return r;
                        }
                        else
                        { 
                            return r;
                        }
                    }
                }
                catch (Exception e)
                {
                    // Haltepunkt?
                    string ex = e.Message;

                }
                return r;
            }
        }
    }

}