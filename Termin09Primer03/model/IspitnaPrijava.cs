using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Termin09Primer03.model
{
    public class IspitnaPrijava
    {
        public Predmet predmet { set; get; }
        public IspitniRok rok { set; get; }
        Student student { set; get; }
        public int teorija { set; get; }
        public int zadaci { set; get; }

        public IspitnaPrijava(Predmet predmet, Student student, IspitniRok rok,
                int teorija, int zadaci)
        {
            this.predmet = predmet;
            this.student = student;
            this.rok = rok;
            this.teorija = teorija;
            this.zadaci = zadaci;
        }

        public override string ToString()
        {
            return "IspitnaPrijava [predmet=" + predmet + ", rok=" + rok
                    + ", teorija=" + teorija + ", zadaci=" + zadaci + "]";
        }

        public int SracunajOcenu()
        {
            int bodovi = teorija + zadaci;
            int ocena;
            if (bodovi >= 95)
                ocena = 10;
            else if (bodovi >= 85)
                ocena = 9;
            else if (bodovi >= 75)
                ocena = 8;
            else if (bodovi >= 65)
                ocena = 7;
            else if (bodovi >= 55)
                ocena = 6;
            else
                ocena = 5;

            return ocena;
        }
    }
}
