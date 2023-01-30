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
    public class StudentUI
    {
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
                        IspisiSveStudente();
                        break;
                    case 2:
                        UnosNovogStudenta();
                        break;
                    case 3:
                        IzmenaPodatakaOStudentu();
                        break;
                    case 4:
                        BrisanjePodatakaOStudentu();
                        break;
                    default:
                       Console.WriteLine("Nepostojeca komanda");
                        break;
                }
            }
        }

        public static void IspisiMenu()
        {
           Console.WriteLine("Rad sa studentima - opcije:");
           Console.WriteLine("\tOpcija broj 1 - ispis svih Studenata");
           Console.WriteLine("\tOpcija broj 2 - unos novog Studenta");
           Console.WriteLine("\tOpcija broj 3 - izmena Studenta");
           Console.WriteLine("\tOpcija broj 4 - brisanje Studenta");
           Console.WriteLine("\t\t ...");
           Console.WriteLine("\tOpcija broj 0 - IZLAZ");
        }

        /** METODE ZA ISPIS STUDENATA ****/
        // ispisi sve studente
        public static void IspisiSveStudente()
        {
            List<Student> sviStudenti = StudentDAO.GetAll(Program.conn);
            for (int i = 0; i < sviStudenti.Count; i++)
            {
               Console.WriteLine(sviStudenti[i]);
            }
        }

        /** METODE ZA PRETRAGU STUDENATA ****/
        // pronadji studenta
        public static Student PronadjiStudenta()
        {
            Student retVal = null;
            Console.Write("Unesi indeks studenta:");
            string stIndex = IO.OcitajTekst();
            retVal = PronadjiStudenta(stIndex);
            if (retVal == null)
               Console.WriteLine("Student sa indeksom " + stIndex
                        + " ne postoji u evidenciji");
            return retVal;
        }

        // pronadji studenta
        public static Student PronadjiStudenta(string stIndex)
        {
            Student retVal = StudentDAO.GetStudentByIndeks(Program.conn,
                    stIndex);
            return retVal;
        }

        /** METODE ZA UNOS, IZMENU I BRISANJE STUDENATA ****/
        // unos novog studenta
        public static void UnosNovogStudenta()
        {
           Console.Write("Unesi indeks:");
            string stIndex = IO.OcitajTekst();
            stIndex = stIndex.ToUpper();
            while (PronadjiStudenta(stIndex) != null)
            {
               Console.WriteLine("Student sa indeksom " + stIndex
                        + " vec postoji");
                stIndex = IO.OcitajTekst();
            }
           Console.Write("Unesi ime:");
            string stIme = IO.OcitajTekst();
           Console.Write("Unesi prezime:");
            string stPrezime = IO.OcitajTekst();
           Console.Write("Unesi grad:");
            string stGrad = IO.OcitajTekst();

            Student st = new Student(0, stIndex, stIme, stPrezime, stGrad);
            // ovde se moze proveravati i povratna vrednost i onda ispisivati poruka
            StudentDAO.Add(Program.conn, st);
        }

        // izmena studenta
        public static void IzmenaPodatakaOStudentu()
        {
            Student st = PronadjiStudenta();
            if (st != null)
            {
               Console.Write("Unesi novi indeks :");
                string stIndeks = IO.OcitajTekst();
                st.indeks = stIndeks;

               Console.Write("Unesi ime :");
                string stIme = IO.OcitajTekst();
                st.ime = stIme;

               Console.Write("Unesi prezime:");
                string stPrezime = IO.OcitajTekst();
                st.prezime = stPrezime;

               Console.Write("Unesi grad:");
                string stGrad = IO.OcitajTekst();
                st.grad = stGrad;

                StudentDAO.Update(Program.conn, st);
            }
        }

        // brisanje studenta
        public static void BrisanjePodatakaOStudentu()
        {
            Student st = PronadjiStudenta();
            if (st != null)
            {
                StudentDAO.Delete(Program.conn, st.id);
            }
        }
    }
}
