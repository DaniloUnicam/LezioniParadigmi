using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicam.Paradigmi.Abstractions;
using Unicam.Paradigmi.Models.Context;

namespace Unicam.Paradigmi.Test.Examples
{
    public class TaskExample : IExample
    {
        public async Task RunExampleAsync()
        {
            //prima chiamata asincrona, potrebbe essere una query
            var content = await LeggiContenutoAsync();
            //await serve ad aspettare i metodi asincroni a cascata
            //come finisce un metodo asincrono, parte il prossimo
            await System.IO.File.WriteAllTextAsync(@"C:\tmp\test.log", "Prova");
            Console.WriteLine("File scritto");

            var ctx = new MyDbContext();
            var dipendenti = ctx.Dipendenti.Where(w => w.Nome == "Federico").ToListAsync(); ;
        }

        //se async, ciò che deve essere restituito, sarà racchiuso tra <>
        //altrimenti, senza async, dovrà essere restituito Task<string>
        private async Task<string> LeggiContenutoAsync()
        {
            await Task.Delay(10000);
            return "Mio contenuto";
        }

        public void RunExample()
        {
            //Action e Func esempi
            int risultatoSomma = Somma(2, 3);
            //nelle func, il primo parametro è l'input, l'ultimo è l'output
            Func<int,int,int> sommaAction = (a,b) => a + b;
            Func<int, int, string> sommaActionString = (a, b) => $"La somma è {a + b}";


            
            string risultato1 = sommaActionString(2, 3);
            string risultato2 = sommaActionString(5, 6);


            CreazioneTaskConRisultato();

            
        }      

        public void CreazioneTaskConRisultato()
        {
            int a = 2;
            int b = 5;
            var taskSomma = new Task<int>(() =>
            {
                Console.WriteLine("Sto eseguendo la somma");
                return a + b;
            });

            var taskMoltiplicazione = new Task<int>(() =>
            {
                Console.WriteLine("Sto eseguendo la moltiplicazione");
                System.Threading.Thread.Sleep(5000);
                return a * b;
            });
            Console.WriteLine("Ho finito la creazione dei task");
            Console.WriteLine("Avvio i task");
            taskSomma.Start();
            taskMoltiplicazione.Start();
            //Task.WaitAll(taskSomma,taskMoltiplicazione);

            while(!taskMoltiplicazione.IsCompleted)
            {
                Console.WriteLine("Aspetto il completamento della moltiplicazione");
                System.Threading.Thread.Sleep(2000);
            }


            //Una volta eseguite le task, potrò stampare i risultati
            Console.WriteLine("Risultato somma : " + taskSomma.Result);
            Console.WriteLine("Risultato moltiplicazione : " + taskMoltiplicazione.Result);

        }

        public void CreazioneTaskSemplici()
        {
            //La lambda expression descrive una action, che non ha argomenti in ingresso
            //() => implica 0 argomenti
            //(dipendenti) => implica l'argomento dipendenti
            var task1 = new Task(() =>
            {
                Console.WriteLine("Sto eseguendo il primo task");
            });

            var task2 = new Task(() =>
            {
                //Nel namespace system.threading.thread, ho l'opportunità di lavorare
                //col thread in cui mi trovo, 10000 ms = 10 secondi 
                System.Threading.Thread.Sleep(10000);
                Console.WriteLine("Sto eseguendo il secondo task");
            });

            task1.Start();
            task2.Start();
            Console.WriteLine("Ho eseguito tutti e due i task");

        }

        public int Somma(int a, int b)
        {
            return a + b;
        }
    }
}
