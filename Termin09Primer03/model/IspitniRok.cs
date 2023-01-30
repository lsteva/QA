using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Termin09Primer03.model
{
    public class IspitniRok
    {
        public int id { set; get; }
        public string naziv { set; get; }
        public DateTime pocetak { set; get; }
        public DateTime kraj { set; get; }

        public IspitniRok(int id, string naziv, DateTime pocetak, DateTime kraj)
        {
            this.id = id;
            this.naziv = naziv;
            this.pocetak = pocetak;
            this.kraj = kraj;
        }

        public override string ToString()
        {
            return "Rok [id=" + id + ", naziv=" + naziv + ", pocetak=" + pocetak
                    + ", kraj=" + kraj + "]";
        }
    }
}
