using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicam.Paradigmi.Models.Entities;

namespace Unicam.Paradigmi.Test.Models
{
    public class Azienda
    {
        public int IdAzienda { get; set; }

        public string RagioneSociale { get; set; }

        public string Citta { get; set; }

        public string Cap {  get; set; }

        public virtual ICollection<Dipendente> Dipendenti {  get; set; }

        //virtual se uso lazyloading
        //public virtual ICollection<Dipendente> Dipendenti { get; set; }




    }
}
