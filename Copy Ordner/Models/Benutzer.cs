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
    

    public class Benutzer 
    {
        [Key]
        public short Nummer { get; set; }
        [Required]
 	    public string E_Mail { get; set; }

        public DateTime LetzterLogin { get; set; }
        [Required]
        public string Nutzername { get; set; }
        public Date Geburtsdatum { get; set; }
        [Required]
        public bool Aktiv { get; set; }
        [Required]
        public Date Anlegedatum  { get; set; }
        public short Alter { get; set; }
        [Required]
        public string Vorname { get; set; }
        [Required]
        public string Nachname { get; set; }
        [Required]
        public string Salt { get; set; }
        [Required]
        public string Hash { get; set; }
        [Required]
        public ENUM("Gast" , "FH-Angehöriger") ISA { get; set; }
        public string Grund { get; set; }
        public Date Ablaufdatum { get; set; }
        private short Matrikelnummer { get; set; }
        public ENUM("ET", "INF", "ISE", "MCD", "WI") Studiengang { get; set; }

    }

 