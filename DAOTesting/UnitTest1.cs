using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using Termin09Primer03.model;
using Termin09Primer03.dao;
using Termin09Primer03.ui;

namespace DAOTesting
{
    [TestClass]
    public class UnitTest1
    {
        static string connectionStringNaKursu = "Data Source=.\\SQLEXPRESS;Initial Catalog=DotNetKurs;User ID=sa;Password=SqlServer2016;MultipleActiveResultSets=True";
        static string connectionStringZaPoKuci = "Data Source=192.168.100.19,6433\\SQLEXPRESS;Initial Catalog=DotNetKurs;User ID=sa;Password=1;MultipleActiveResultSets=True";

        private SqlConnection OpenConnection()
        {
            SqlConnection conn = new SqlConnection(connectionStringZaPoKuci);
            conn.Open();
            return conn;
        }

        [TestMethod]
        public void StudentTestAddAndUpdate()
        {
            SqlConnection conn = null;
            string ind = "test_ind";
            try
            {
                // Konekcija na bazu
                conn = OpenConnection();

                // Ako slucajno postoji ovakav student u bazi, javiti gresku
                // Ne zelimo da test obrise studenta iz baze
                Student s = StudentDAO.GetStudentByIndeks(conn, ind);
                if (s != null)
                {
                    Assert.Fail("Vec postoji student sa indeksom {0} u bazi. Promeniti testni indeks!", ind);
                }

                // Testiramo da li dodavanje vraca true
                s = new Student(-1, ind, "Test_ime", "Test_prezime", "Test_grad");
                Assert.IsTrue(StudentDAO.Add(conn, s), "Dodavanje studenta ne radi!");

                // Testiramo da li updatovanje vraca true
                s = StudentDAO.GetStudentByIndeks(conn, ind);
                s.prezime = "Test_prezime2";
                Assert.IsTrue(StudentDAO.Update(conn, s), "Azuriranje studenta ne radi!");
            }
            finally
            {
                if(conn != null)
                {
                    // Zelimo da obrisemo studenta ako smo ga dodali
                    // Ne zelimo da nas test menja bazu podataka
                    Student s = StudentDAO.GetStudentByIndeks(conn, ind);
                    StudentDAO.Delete(conn, s.id);

                    conn.Close();
                }                
            }
        }

        [TestMethod]
        public void PredmetTestAdd()
        {
            SqlConnection conn = null;
            string testNaziv = "Test_Naziv";
            try
            {
                // Konekcija na bazu
                conn = OpenConnection();

                // Ako slucajno postoji ovakav predmet u bazi, javiti gresku
                // Ne zelimo da test obrise predmet iz baze
                Predmet p = PredmetDAO.GetPredmetByNaziv(conn, testNaziv);
                if (p != null)
                {
                    Assert.Fail("Vec postoji predmet sa nazivom {0}. Promeniti testni naziv!", testNaziv);
                }

                // Probamo da dodamo predmet i proverimo da li funkcija vraca true
                p = new Predmet(-1, testNaziv);
                Assert.IsTrue(PredmetDAO.Add(conn, p), "Dodavanje predmeta nije uspelo!");
            }
            finally
            {
                if (conn != null)
                {
                    Predmet p = PredmetDAO.GetPredmetByNaziv(conn, testNaziv);
                    PredmetDAO.Delete(conn, p.id);
                    conn.Close();
                }
            }
        }
    }
}
