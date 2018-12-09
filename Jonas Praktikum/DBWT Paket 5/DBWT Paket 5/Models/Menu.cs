using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LinqToDB;
using System.Web.Mvc;
using DBWT_Paket_5.Models;
using DataModels;
using System.Xml.Linq;
using System.Xml.XPath;

namespace DBWT_Paket_5.Models
{
    public class Menu
    {
        public DateTime Tag { get; set; }
        public int KW { get; set; }
        public string Motto { get; set; }
        public Dictionary<string , Produkt> Produkte { get; set; }
        public Bild Highlight { get; set; }



    }
}