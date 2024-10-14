using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicam.Paradigmi.Models.Entities;

namespace Unicam.Paradigmi.Test.Models
{
    public class Azienda
    {
        //Per evitare problemi con oggetti nulli, li costruiamo in un costruttore default
        public Azienda()
        {
            RagioneSociale = string.Empty;
            Citta = string.Empty;
            Cap = string.Empty;
            Dipendenti = new List<Dipendente>();
        }

        //l'owner del server non creerà manualmente l'id dell'azienda, ma lo farà il database stesso
        public int IdAzienda { get; set; }

        public string RagioneSociale { get; set; }

        public string Citta { get; set; }

        public string Cap {  get; set; }

        //posso dichiarare public virtual ICollection<Dipendente> Dipendenti {  get; set; } = null!;
        //il null! bypassa il warning che il contenuto possa essere nullo.
        public virtual ICollection<Dipendente> Dipendenti {  get; set; }

        //virtual se uso lazyloading
        //public virtual ICollection<Dipendente> Dipendenti { get; set; }




    }
}
