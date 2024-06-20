using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicam.Paradigmi.Abstractions;
using Unicam.Paradigmi.Models.Context;
using Unicam.Paradigmi.Models.Entities;
using Unicam.Paradigmi.Models.Repositories;
using Unicam.Paradigmi.Test.Models;

namespace Unicam.Paradigmi.Test.Examples
{
    public class LinqExample : IExample
    {
        public async Task RunExampleAsync()
        {

        }
        public void RunExample()
        {
            var ctx = new MyDbContext();

            Func<Dipendente, bool> queryPerCognome =
                (dipendente) => dipendente.Cognome == "Paoloni";

            ctx.Dipendenti.Where(queryPerCognome);

            var maxDateNascita = ctx.Dipendenti
                .Max(m => m.DataNascita);

            var minDateNascita = ctx.Dipendenti
                .Min(m => m.DataNascita);

            var queryResult = ctx.Dipendenti.ToList()
                .GroupBy(g => g.IdAzienda);
            //all'interno del queryresult ci viene restituito
            //un oggetto che ci raggruppa una lista di elementi che vengono
            //raggruppati per l'id azienda
            foreach(var item in queryResult)
            {
                Console.WriteLine($"Azienda con codice {item.Key}");
                foreach(var dipendenti in item.ToList())
                {
                    Console.WriteLine($"{dipendenti.Cognome}" +
                        $"{dipendenti.Nome}");
                }
            }
        }

        //Esempio di enumerazione
        public void Enumerazione(MyDbContext ctx)
        {
            //IQueryable è un qualcosa di dinamico che permette di fare chaining
            IQueryable<Dipendente> query =
                ctx.Dipendenti.Where(w => w.IdDipendente == 1);
            //La query non viene fatta quando faccio la where, ma dopo quando vado ad enumerarla


            bool filterBycompany = true;
            if (filterBycompany)
            {
                query = query.Where(w => w.IdAzienda == 1);
            }
            //Restituisce il primo dipendente
            Dipendente dp01 = query.First();
            //Restituisce il primo dipendente trovato con id == 1, oppure un oggetto nullo di default
            //Dipendente dp01 = result.FirstOrDefault();

            //Questi metodi sono delle extension method, delle estensioni
            //First, FirstOrDefault, ToList, Single, etc.
            //sono dei metodi di estensione che si possono applicare
            //a tutti gli oggetti di tipo IQueryable
            //sono tutti all'interno di System.Linq

            //Restituisce tutti i dipendenti che trova nella query
            List<Dipendente> listaDipendente = query.ToList();
            //Restituisce il primo, assicurandosi che nella query ci sia un solo risultato
            Dipendente dip02 = query.Single();

        }
    }
}

