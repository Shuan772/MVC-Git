using System;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace DBWT_Paket_5
{
    class Datenverarbeitung
    {
        const string constring = "Server=localhost;Database=praktikum;Uid=webapp;Pwd=webapp;";
        MySqlConnection con = new MySqlConnection(constring);
        private MySqlCommand cmd;
        private MySqlDataReader r;

        internal object execute_scalar(string s)
        {
            try
            {
                cmd = con.CreateCommand();
                cmd.CommandText = s;

                con.Open();

                object o = cmd.ExecuteScalar();
                con.Close();
                return o;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        internal DataTable execute_reader(string s)
        {
            try
            {
                DataTable t = new DataTable();

                cmd = con.CreateCommand();
                cmd.CommandText = s;

                con.Open();
                r = cmd.ExecuteReader();

                t.Load(r);
                r.Close();
                con.Close();

                return t;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        internal void execute_nonquery(string s)
        {
            try
            {
                cmd = con.CreateCommand();
                cmd.CommandText = s;

                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();

                if (i == 0)
                    throw new Exception();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string blobToBase64(object o)
        {
            byte[] bytes = (byte[])o;

            if (bytes != null)
                return Convert.ToBase64String(bytes);

            return "";
        }
        public DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}