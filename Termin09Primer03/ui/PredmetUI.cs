﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Termin09Primer03.dao;
using Termin09Primer03.model;
using Termin09Primer03.utils;

namespace Termin09Primer03.ui
{
    public class PredmetUI
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
                        IspisiSvePredmete();
                        break;
                    case 2:
                        UnosNovogPredmeta();
                        break;
                    case 3:
                        IzmenaPodatakaOPredmetu();
                        break;
                    case 4:
                        BrisanjePodatakaOPredmetu();
                        break;
                    default:
                        Console.WriteLine("Nepostojeca komanda");
                        break;
                }
            }
        }

        public static void IspisiMenu()
        {
            Console.WriteLine("Rad sa predmetima - opcije:");
            Console.WriteLine("\tOpcija broj 1 - ispis svih Predmeta");
            Console.WriteLine("\tOpcija broj 2 - unos novog Predmeta");
            Console.WriteLine("\tOpcija broj 3 - izmena naziva Predmeta");
            Console.WriteLine("\tOpcija broj 4 - brisanje Predmeta");
            Console.WriteLine("\t\t ...");
            Console.WriteLine("\tOpcija broj 0 - IZLAZ");
        }

        /** METODE ZA ISPIS PREDMETA ****/
        // ispisi sve predmete
        public static void IspisiSvePredmete()
        {
            List<Predmet> sviPredmeti = PredmetDAO.GetAll(Program.conn);
            for (int i = 0; i < sviPredmeti.Count; i++)
            {
                Console.WriteLine(sviPredmeti[i]);
            }
        }

        /** METODE ZA PRETRAGU PREDMETA ****/
        // pronadji predmet
        public static Predmet PronadjiPredmet()
        {
            Predmet retVal = null;
            Console.Write("Unesi id predmeta:");
            int id = IO.OcitajCeoBroj();
            retVal = PronadjiPredmet(id);
            if (retVal == null)
                Console.WriteLine("Predmet sa id-om " + id
                        + " ne postoji u evidenciji");
            return retVal;
        }

        // pronadji predmet
        public static Predmet PronadjiPredmet(int id)
        {
            Predmet retVal = PredmetDAO.GetPredmetById(Program.conn, id);
            return retVal;
        }

        /** METODE ZA UNOS, IZMENU I BRISANJE PREDMETA ****/
        // unos novog predmeta
        public static void UnosNovogPredmeta()
        {
            Console.Write("Naziv:");
            string prNaziv = IO.OcitajTekst();

            Predmet pred = new Predmet(0, prNaziv);
            //ovde se moze proveravati i povratna vrednost i onda ispisivati poruka
            PredmetDAO.Add(Program.conn, pred);
        }

        // izmena predmeta
        public static void IzmenaPodatakaOPredmetu()
        {
            Predmet pred = PronadjiPredmet();
            if (pred != null)
            {
                Console.Write("Unesi novi naziv :");
                string prNaziv = IO.OcitajTekst();
                pred.naziv = prNaziv;
                PredmetDAO.Update(Program.conn, pred);
            }
        }

        //brisanje predmeta
        public static void BrisanjePodatakaOPredmetu()
        {
            Predmet pred = PronadjiPredmet();
            if (pred != null)
            {
                PredmetDAO.Delete(Program.conn, pred.id);
            }
        }
    }
}
