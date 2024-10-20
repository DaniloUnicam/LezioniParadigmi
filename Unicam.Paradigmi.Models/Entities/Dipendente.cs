﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicam.Paradigmi.Test.Models;

namespace Unicam.Paradigmi.Models.Entities
{
    public class Dipendente
    {

        public Dipendente() 
        {
            Nome = string.Empty;
            Cognome = string.Empty;
            DataNascita = new DateTime();
            AziendaDoveLavora = new Azienda();
        }

        public Dipendente(Azienda aziendaDoveLavora)
        {
            AziendaDoveLavora = aziendaDoveLavora;
        }

        public int IdDipendente { get; set; }

        public int IdAzienda { get; set; }

        public string Nome { get; set; }

        public string Cognome { get; set; }

        public DateTime DataNascita { get; set; }

        public virtual Azienda AziendaDoveLavora { get; set; }

        //virtual se uso LazyLoading
        //public virtual Azienda AziendaDoveLavora { get; set; }

    }
}
