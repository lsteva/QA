using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Termin09Primer03.model
{
    public class Student
    {
        public int id { set; get; }
        public string ime { set; get; }
        public string prezime { set; get; }
        public string grad { set; get; }
        public string indeks { set; get; }
        public List<IspitnaPrijava> prijave { set; get; }
        public List<Predmet> predmeti { set; get; }

        public Student(int id, string indeks, string ime, string prezime,
                string grad)
        {
            prijave = new List<IspitnaPrijava>();
            predmeti = new List<Predmet>();

            this.id = id;
            this.indeks = indeks;
            this.ime = ime;
            this.prezime = prezime;
            this.grad = grad;
        }

        // prebacivanje objekta Student u string reprezentaciju
        public override string ToString()
        {
            string s = "Student [" + indeks + " " + ime + " " + prezime + " "
                    + grad + "]";
            foreach (Predmet p in predmeti)
                s += p.naziv + ",";

            return s;
        }
    }
}
