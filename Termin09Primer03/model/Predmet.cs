using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Termin09Primer03.model
{
    public class Predmet
    {
        public int id { set; get; }
        public string naziv { set; get; }

        public List<Student> studenti { set; get; }

        public Predmet(int id, string naziv)
        {
            this.id = id;
            this.naziv = naziv;
            studenti = new List<Student>();
        }

        public override string ToString()
        {
            return "Predmet [id=" + id + ", naziv=" + naziv + "]";
        }
    }
}
