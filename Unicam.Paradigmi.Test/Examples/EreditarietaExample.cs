using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicam.Paradigmi.Abstractions;
using Unicam.Paradigmi.Test.Models;

namespace Unicam.Paradigmi.Test.Examples
{
    public class EreditarietaExample : IExample
    {
        public async Task RunExampleAsync()
        {

        }
        public void RunExample()
        {
            List<Veicolo> veicoli = new List<Veicolo>
            {
                new Bicicletta("Graziella", 10),
                new Macchina("Golf", 0, 1)
            };

            foreach(var veicolo in veicoli)
            {
                veicolo.VisualizzaVelocita();
            }
        }
    }
}
