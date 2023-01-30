using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Termin09Primer03.dao;
using Termin09Primer03.model;
using Termin09Primer03.utils;

namespace Termin09Primer03.ui
{
    public class PohadjaUI
    {
        private static void IspisiMenu()
        {
            Console.WriteLine("Rad sa predmetima studenta - opcije:");
            Console.WriteLine("\tOpcija broj 1 - predmeti koje student pohadja");
            Console.WriteLine("\tOpcija broj 2 - studenti koji pohadjaju predmet");
            Console.WriteLine("\tOpcija broj 3 - dodavanje studenta na predmet");
            Console.WriteLine("\tOpcija broj 4 - uklanjanje studenta sa predmeta");
            Console.WriteLine("\t\t ...");
            Console.WriteLine("\tOpcija broj 0 - IZLAZ");
        }

        public static void Menu()
        {
            int odluka = -1;
            while (odluka != 0)
            {
                IspisiMenu();
                Console.Write("opcija:");
                odluka = IO.OcitajCeoBroj();
                switch (odluka)
                {
                    case 0:
                        Console.WriteLine("Izlaz");
                        break;
                    case 1:
                        IspisiPredmeteZaStudenta();
                        break;
                    case 2:
                        IspisiStudenteZaPredmet();
                        break;
                    case 3:
                        dodajStudentaNaPredmet();
                        break;
                    case 4:
                        ukloniStudentaSaPredmeta();
                        break;
                    default:
                        Console.WriteLine("Nepostojeca komanda");
                        break;
                }
            }
        }

        private static void IspisiPredmeteZaStudenta()
        {
            // najpre pronadjemo studenta za kojeg zelimo ispis predmeta
            Student student = StudentUI.PronadjiStudenta();
            if (student != null)
            {
                List<Predmet> predmeti = PohadjaDAO.GetPredmetiByStudentId(
                        Program.conn, student.id);

                foreach(Predmet p in predmeti)
                {
                    Console.WriteLine(p);
                }
            }
        }

        private static void IspisiStudenteZaPredmet()
        {
            // najpre pronadjemo predmet za koji zelimo ispis studenata
            Predmet predmet = PredmetUI.PronadjiPredmet();
            if (predmet != null)
            {
                List<Student> studenti = PohadjaDAO.GetStudentiByPredmetId(
                        Program.conn, predmet.id);

                foreach(Student s in studenti)
                {
                    Console.WriteLine(s);
                }
            }
        }

        private static void dodajStudentaNaPredmet()
        {
            // najpre pronadjemo studenta kojeg zelimo da dodamo na predmet
            Student student = StudentUI.PronadjiStudenta();
            //pronadjemo predmet na koji zelimo da dodamo studenta
            Predmet predmet = PredmetUI.PronadjiPredmet();

            if (student != null && predmet != null)
            {
                //uspostavimo vezu izmedju studenta i predmeta
                PohadjaDAO.Add(Program.conn, student.id, predmet.id);
            }
        }

        private static void ukloniStudentaSaPredmeta()
        {
            // najpre pronadjemo studenta kojeg zelimo da uklonimo sa predmeta
            Student student = StudentUI.PronadjiStudenta();
            //pronadjemo predmet sa kojeg zelimo da ukloniko studenta
            Predmet predmet = PredmetUI.PronadjiPredmet();

            if (student != null && predmet != null)
            {
                //uspostavimo vezu izmedju studenta i predmeta
                PohadjaDAO.Delete(Program.conn, student.id, predmet.id);
            }
        }
    }
}
