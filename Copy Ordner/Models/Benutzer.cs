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
        public DateTime? LetzterLogin { get; set; }
        [Required]
        public string Nutzername { get; set; }
        public DateTime? Geburtsdatum { get; set; }
        [Required]
        public bool Aktiv { get; set; }
        [Required]
        public DateTime? Anlegedatum { get; set; }
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
        public string Rolle { get; set; }
        public string Grund { get; set; }
        public DateTime? Ablaufdatum { get; set; }
        private short Matrikelnummer { get; set; }
        public string Studiengang { get; set; }
        public string Büro { get; set; }
        public string Telefon { get; set; }

        /*ENUM("Gast" , "FH-Angehöriger")
         ENUM("ET", "INF", "ISE", "MCD", "WI")
         */
        public static Benutzer GetByNutzername(string Nutzername)
        {
            Benutzer m = new Benutzer();
            using (MySqlConnection con =
                new MySqlConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString))
            {
                try
                {
                    
                    using (MySqlCommand cmd = new MySqlCommand("", con))
                    {
                        cmd.CommandText = "SELECT Nummer, LetzterLogin, Nutzername, Aktiv, Vorname , Nachname , Salt , Hash ,ISA FROM Benutzer WHERE Nutzername=@mid ";
                        cmd.Parameters.AddWithValue("mid", Nutzername);
                        con.Open();
                        var r = cmd.ExecuteReader();
                        if (r.Read())
                        {
							if(! r.HasRows)
							{ return null;}
                            m.Nummer = Int16.Parse(r["Nummer"].ToString());
                            m.Vorname = r["Vorname"].ToString();
                            m.Nachname = r["Nachname"].ToString();
                            m.Nutzername = r["Nutzername"].ToString();
                            
                            if (r["LetzterLogin"].ToString() != "")
                            {
                                m.LetzterLogin = Convert.ToDateTime(r["LetzterLogin"].ToString());
                            }
                            else
                            {
                                m.LetzterLogin = null;
                            }
                            m.Aktiv = Convert.ToBoolean(r["Aktiv"]);
                            m.Salt = r["Salt"].ToString();
                            m.Hash = r["Hash"].ToString();
                            m.Rolle = r["ISA"].ToString();
                        }
                    }
                    con.Close();
                    con.Open();
                    using (MySqlCommand cmd = new MySqlCommand("", con))
                    {
                        if (m.Rolle == "Gast")
                        {
                            cmd.CommandText = "SELECT Grund,Ablaufdatum FROM `gäste` WHERE Nummer =@mid ";
                        }
                        else
                        {
                            cmd.CommandText = "SELECT s.id, m.Telefon, m.`büro`, s.Studiengang  FROM Benutzer b LEFT JOIN Mitarbeiter m on m.id = b.nummer LEFT JOIN Student s on s.id = b.nummer WHERE Nummer =@mid ";
                        }
                        cmd.Parameters.AddWithValue("mid", m.Nummer);

                        var r = cmd.ExecuteReader();
                        if (r.Read())
                        {
                            if (m.Rolle == "Gast")
                            {
                                m.Grund = r["Grund"].ToString();
                                m.Rolle = "Gast";
                                m.Ablaufdatum = Convert.ToDateTime(r["Ablaufdatum"]);
                            }
                            else
                            {
                                if (String.IsNullOrEmpty(r["id"].ToString()))
                                {
                                    m.Rolle = "Mitarbeiter";
                                    m.Büro = r["büro"].ToString();
                                    m.Telefon = r["Telefon"].ToString();
                                }
                                else
                                {
                                    m.Rolle = "Student";
                                    m.Studiengang = r["Studiengang"].ToString();
                                }
                            }
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
            return m;
        }
        public static string VerifyBenutzer(string pw, string Nutzername)
        {
            Benutzer m = new Benutzer();
            m = Benutzer.GetByNutzername(Nutzername);
			if(m.Nutzername == null)
			{
				return "False";
			}
            if (m.Aktiv)
            {
                bool testhash = PasswordSecurity.PasswordStorage.VerifyPassword(pw, "sha1:64000:18:" + m.Salt + ":" + m.Hash);
                return testhash.ToString();
            }
            else
            {
                return "Account Inaktiv bitte wenden sie sich an einen Admin.";
            }

            //  Benutzer.SetLogin(test);
            // setzte login zeit.
            //Create 
            //Bei Create Type String 
            // muss grund zurück geben.
            //string result = PasswordSecurity.PasswordStorage.CreateHash(reader["salt"].ToString() + Request.Form["password"]);

        }




        /*
@using System.Configuration;
@helper LoginIndex()
{

try
{
    if (Request.Form["logout"] == "1")
    {
        Session.Remove("User"); // Diese Daten sollen aus
        Session.Remove("Role"); // der Datenbank kommen
    }

    if (string.IsNullOrEmpty(Session["User"] as string))
    {
        if (Request.Form["name"] != "" && Request.Form["password"] != "" && Request.Form["name"] != null && Request.Form["password"] != null)
        {
            MySqlConnection con = null;
            try
            {

                con = new MySqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString);
                var name = Request.Form["name"];
                var pw = Request.Form["password"];
                string query = "SELECT be.Nummer ,be.`hash` , be.`Salt` , be.`Nutzername` , be.`ISA` FROM benutzer be WHERE be.`Nutzername` ='" + name + "' ;";
                con.Open();
                MySqlCommand command = new MySqlCommand(query, con);
                MySqlDataReader reader = command.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    string teststring = "sha1: 64000:18:" + reader["salt"].ToString() + ":" + reader["hash"].ToString();


                    //string result = PasswordSecurity.PasswordStorage.CreateHash(reader["salt"].ToString() + Request.Form["password"]);

                    bool testhash = PasswordSecurity.PasswordStorage.VerifyPassword(Request.Form["password"], "sha1:64000:18:" + reader["salt"].ToString() + ":" + reader["hash"].ToString());
                    string nummer = reader["`Nutzername`"].ToString();
                    string isa = reader["ISA"].ToString();
                    con.Close();

                    if (testhash)
                    {
                        Session["User"] = nummer;
                        Session["Role"] = isa;
                @TrueLogin()
                        }
                        else
                        {
                    @FalseLogin(Request.Form["name"], Request.Form["password"])
                        }
                    }
                    else
                    {
                        @FalseLogin(Request.Form["name"], Request.Form["password"])
                    }
                }
                catch (Exception ex)
                {
                    if (con != null)
                    {
                        con.Close();
                    }
                <p> @ex </p>
                }

            }
            else
            {
                @NOLogin()
            }
        }
        else
        {
            @TrueLogin()
    }

}
catch (Exception ex)
{
        <p>@ex</p>
}
}
@helper NOLogin()
{
 
        <div class="col-6">
            <form action="~/M2Git/Login.cshtml" method="post">
                <fieldset class="border align-content-center">
                    <legend class="scheduler-border">Login</legend>
                    <div>
                        <label for="name">Name:</label>
                        <input type="text" name="name" placeholder="Benutzer" required title="Min 1" class="form-control">
                        <label for="password">Password:</label>
                        <input type="password" name="password" placeholder="password" required title="Min 1" class="form-control">
                        <button type="submit" class="form-control">Anmelden</button>
                    </div>
                </fieldset>
            </form>
        </div>

}

@helper FalseLogin(string altname,string altpw)
{
    <p><font color="red">Daten sind Falsch</font></p>
    <div class="col-6">
        <form action="~/M2Git/Login.cshtml" method="post">
            <fieldset class="border align-content-center">
                <legend class="scheduler-border">Login</legend>
                <div>
                    <label for="name" class="dangerlabel">Name:</label>
                    <input value=@altname type="text" name="name" placeholder="Benutzer" required title="Min 1" class="form-control dangerinput">
                    <label for="password" class="dangerlabel">Password:</label>
                    <input value=@altpw type="password" name="password" placeholder="password" required title="Min 1" class="form-control dangerinput">
                    <button type="submit" class="form-control">Anmelden</button>
                </div>
            </fieldset>
        </form>
    </div>

}

@helper TrueLogin()
{
    <p>Hallo @Session["user"], Sie sind angemeldet als @Session["role"]</p>
    <form action="~/M2Git/Login.cshtml" method="post">
        <input type="hidden" name="logout" value="1" />
        <button type="submit" class="form-control logoutbutton">Abmelden</button>
    </form>

}*/







    }
    }

 