﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicam.Paradigmi.Test.Models
{
    public class Alunno
    {

        public Alunno()
        {

        }

        public Alunno(string csvRow)
        {
            //Logica per leggere l'alunno del csv
            string[] values = csvRow.Split(";".ToCharArray());
            Nome = values[0];
            Cognome = values[1];
            Matricola = values[2];
            DataNascita = DateTime.ParseExact(values[3],"dd/MM/yyyy",CultureInfo.InvariantCulture);
        }
        public string Nome {  get; set; }
        public string Cognome { get; set;}
        public string Matricola { get; set;}
        [JsonProperty("data_nascita")]
        public DateTime DataNascita { get; set; }
        //nullvaluehandling ignora i risultati con null nell'indirizzo residenza
        [JsonProperty("indirizzo_residenza",NullValueHandling = NullValueHandling.Ignore)]
        public Indirizzo IndirizzoResidenza { get; set; } 

    }
}
