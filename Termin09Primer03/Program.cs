using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Termin09Primer03.ui;
using Termin09Primer03.utils;

namespace Termin09Primer03
{
    class Program
    {
        public static SqlConnection conn;

        static void LoadConnection()
        {
            string connectionStringNaKursu = "Data Source=.\\SQLEXPRESS;Initial Catalog=DotNetKurs;User ID=sa;Password=SqlServer2016;MultipleActiveResultSets=True";
            string connectionStringZaPoKuci = "Data Source=192.168.100.19,6433\\SQLEXPRESS;Initial Catalog=DotNetKurs;User ID=sa;Password=1;MultipleActiveResultSets=True";
            try
            {
                // konekcija
                conn = new SqlConnection(connectionStringZaPoKuci);
                conn.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static void Main(string[] args)
        {
            LoadConnection();

            int odluka = -1;
            while (odluka != 0)
            {
                IspisiMenu();

                Console.Write("opcija:");
                odluka = IO.OcitajCeoBroj();

                switch (odluka)
                {
                    case 0:
                        Console.WriteLine("Izlaz iz programa");
                        break;
                    case 1:
                        StudentUI.Menu();
                        break;
                    case 2:
                        PredmetUI.Menu();
                        break;
                    case 3:
                        PohadjaUI.Menu();
                        break;
                    default:
                        Console.WriteLine("Nepostojeca komanda");
                        break;
                }
            }
        }

        // ispis teksta osnovnih opcija
        public static void IspisiMenu()
        {
            Console.WriteLine("Studentska Sluzba - Osnovne opcije:");
            Console.WriteLine("\tOpcija broj 1 - Rad sa studentima");
            Console.WriteLine("\tOpcija broj 2 - Rad sa predmetima");
            Console.WriteLine("\tOpcija broj 3 - Rad sa pohadjanjem predmeta");
            Console.WriteLine("\t\t ...");
            Console.WriteLine("\tOpcija broj 0 - IZLAZ IZ PROGRAMA");
        }
    }
}
