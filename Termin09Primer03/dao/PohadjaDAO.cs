using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Termin09Primer03.model;
using System.Data.SqlClient;

namespace Termin09Primer03.dao
{
    public class PohadjaDAO
    {
        public static List<Predmet> GetPredmetiByStudentId(SqlConnection conn, int id)
        {
            List<Predmet> retVal = new List<Predmet>();
            try
            {
                string queryString = "SELECT predmet_id FROM pohadja " +
                            "WHERE student_id = " + id;

                SqlCommand cmd = new SqlCommand(queryString, conn);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    int predmetId = (int)rdr["predmet_id"];
                    retVal.Add(PredmetDAO.GetPredmetById(conn, predmetId));
                }
                rdr.Close();                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return retVal;
        }

        public static List<Student> GetStudentiByPredmetId(SqlConnection conn, int id)
        {
            List<Student> retVal = new List<Student>();
            try
            {
                string queryString = "SELECT student_id FROM pohadja WHERE " +
                                "predmet_id = " + id;
                SqlCommand cmd = new SqlCommand(queryString, conn);
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    int studentId = (int)rdr["student_id"];
                    retVal.Add(StudentDAO.GetStudentById(conn, studentId));
                }
                rdr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return retVal;
        }

        public static bool Add(SqlConnection conn, int studentId, int predmetId)
        {
            bool retVal = false;
            try
            {
                string update = "INSERT INTO pohadja " +
                        "(student_id, predmet_id) values (@student_id, @predmet_id)";
                SqlCommand cmd = new SqlCommand(update, conn);
                cmd.Parameters.AddWithValue("@student_id", studentId);
                cmd.Parameters.AddWithValue("@predmet_id", predmetId);

                if (cmd.ExecuteNonQuery() == 1)
                    retVal = true;                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return retVal;
        }


        public static bool Delete(SqlConnection conn, int studentId, int predmetId)
        {
            bool retVal = false;
            try
            {
                string update = "DELETE FROM pohadja WHERE student_id = " + studentId + " AND predmet_id = " + predmetId;
                SqlCommand cmd = new SqlCommand(update, conn);
                
                if (cmd.ExecuteNonQuery() == 1)
                    retVal = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return retVal;
        }

        public static bool DeletePohadjanjaStudenta(SqlConnection conn, int studentId)
        {
            bool retVal = false;
            try
            {
                string update = "DELETE FROM pohadja WHERE student_id = " + studentId;
                SqlCommand cmd = new SqlCommand(update, conn);
                if (cmd.ExecuteNonQuery() == 1)
                    retVal = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return retVal;
        }

        public static bool DeletePohadjanjaPredmeta(SqlConnection conn, int predmetId)
        {
            bool retVal = false;
            try
            {
                string update = "DELETE FROM pohadja WHERE predmet_id = " + predmetId;
                SqlCommand cmd = new SqlCommand(update, conn);
                if (cmd.ExecuteNonQuery() == 1)
                    retVal = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return retVal;
        }

        //update svih pohadjanja jednog studenta
        public static bool Update(SqlConnection conn, Student student)
        {
            bool retVal = false;
            try
            {
                //obrisemo prethodna pohadjaja
                retVal = DeletePohadjanjaStudenta(conn, student.id);
                //ako je brisanje uspelo idemo na dodavanje
                if (retVal)
                {
                    foreach (Predmet predmet in student.predmeti)
                    {
                        retVal = Add(conn, student.id, predmet.id);
                        if (retVal == false)
                            throw new Exception("Dodavanje nije valjalo");
                    }
                }
                else
                {
                    throw new Exception("Brisanje nije valjalo");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return retVal;
        }

        //update svih pohadjanja jednog predmeta
        public static bool Update(SqlConnection conn, Predmet predmet)
        {
            bool retVal = false;
            try
            {
                //obrisemo prethodna pohadjaja
                retVal = DeletePohadjanjaPredmeta(conn, predmet.id);
                //ako je brisanje uspelo idemo na dodavanje
                if (retVal)
                {
                    foreach (Student student in predmet.studenti)
                    {
                        retVal = Add(conn, student.id, predmet.id);
                        if (retVal == false)
                            throw new Exception("Dodavanje nije valjalo");
                    }
                }
                else
                {
                    throw new Exception("Brisanje nije valjalo");
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
