using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Termin09Primer03.model;

namespace Termin09Primer03.dao
{
    public class PredmetDAO
    {
        public static Predmet GetPredmetById(SqlConnection conn, int id)
        {
            Predmet predmet = null;
            try
            {
                string query = "SELECT naziv FROM predmeti WHERE predmet_id = " + id;
                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader rdr = cmd.ExecuteReader();                        

                if (rdr.Read())
                {
                    string naziv = (string)rdr["naziv"];                    
                    predmet = new Predmet(id, naziv);
                }
                rdr.Close();                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return predmet;
        }

        public static Predmet GetPredmetByNaziv(SqlConnection conn, string naziv)
        {
            Predmet predmet = null;
            try
            {
                string query = "SELECT predmet_id FROM predmeti WHERE naziv = '"
                               + naziv + "'";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    int id = (int)rdr["predmet_id"];
                    predmet = new Predmet(id, naziv);
                }
                rdr.Close();                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return predmet;
        }

        public static List<Predmet> GetAll(SqlConnection conn)
        {
            List<Predmet> retVal = new List<Predmet>();
            try
            {
                string query = "SELECT predmet_id, naziv FROM predmeti ";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    int id = (int)rdr["predmet_id"];
                    string naziv = (string)rdr["naziv"];

                    Predmet predmet = new Predmet(id, naziv);
                    retVal.Add(predmet);
                }
                rdr.Close();                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return retVal;
        }

        public static bool Add(SqlConnection conn, Predmet predmet)
        {
            bool retVal = false;
            try
            {
                string update = "INSERT INTO predmeti (naziv) values (@naziv)";
                SqlCommand cmd = new SqlCommand(update, conn);
                cmd.Parameters.AddWithValue("@naziv", predmet.naziv);
                
                if (cmd.ExecuteNonQuery() == 1)
                {
                    retVal = true;
                }                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return retVal;
        }

        public static bool Update(SqlConnection conn, Predmet predmet)
        {
            bool retVal = false;
            try
            {
                string update = "UPDATE predmeti SET naziv=@naziv WHERE predmet_id=@predmet_id";
                SqlCommand cmd = new SqlCommand(update, conn);
                cmd.Parameters.AddWithValue("@naziv", predmet.naziv);
                cmd.Parameters.AddWithValue("@predmet_id", predmet.id);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    retVal = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return retVal;
        }

        public static bool Delete(SqlConnection conn, int id)
        {
            bool retVal = false;
            try
            {
                string update = "DELETE FROM predmeti WHERE predmet_id = " + id;
                SqlCommand cmd = new SqlCommand(update, conn);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    retVal = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return retVal;
        }
    }
}
