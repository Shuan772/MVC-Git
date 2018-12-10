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
    public class Gast : DbContext
    {
        [ScaffoldColumn(false)]
        public uint ID { get; set; }

        [DisplayName("Ablaufdatum")]
        public DateTime? Ablaufdatum { get; set; }

        [DisplayName("Grund")]
        [StringLength(255)]
        public string Grund { get; set; }



        public static bool create(Gast a)
        {
            return true;
        }
    }

}