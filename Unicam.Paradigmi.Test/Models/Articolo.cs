﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Unicam.Paradigmi.Test.Delegates.MyDelegates;

namespace Unicam.Paradigmi.Test.Models
{
    internal class Articolo
    {
        public string CodiceArticolo { get; set; }

        public string Descrizione { get; set; }

        private int _qtaMagazzino;

        public int QtaMagazzino
        {
            get { return _qtaMagazzino; }
            set
            {
                _qtaMagazzino = value;
                if (_qtaMagazzino < LivelloScortaMinima)
                {
                    RaiseSottoScortaEvent();
                }
            }
        }

        public int LivelloScortaMinima { get; set; }

        public event SottoScortaHandler SottoScortaEvent;

        private void RaiseSottoScortaEvent()
        {
            if (SottoScortaEvent != null)
            {
                SottoScortaEvent(this, DateTime.Now);
            }
        }


    }
}
