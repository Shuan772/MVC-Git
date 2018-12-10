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
    public class Mitarbeiter : DbContext
    {
        [ScaffoldColumn(false)]
        public uint ID { get; set; }

        [DisplayName("Büro")]
        [StringLength(20)]
        public string Büro { get; set; }

        [DisplayName("Telefon")]
        [StringLength(20)]
        public string Telefon { get; set; }

        public static bool create(Mitarbeiter a)
        {
            return true;
        }
    }

}