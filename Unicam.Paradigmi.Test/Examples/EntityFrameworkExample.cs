using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicam.Paradigmi.Abstractions;
using Unicam.Paradigmi.Models.Context;
using Unicam.Paradigmi.Test.Models;

namespace Unicam.Paradigmi.Test.Examples
{
    public class EntityFrameworkExample : IExample
    {
        public void RunExample()
        {
            var ctx = new MyDbContext();
            //LoadWithExplicitLoading(ctx);
            //LoadWithEagerLoading(ctx);
            //LoadWithLazyLoading(ctx);
            //questa è una query che farà il select di tutte le aziende con i loro record
            //var aziende = ctx.Aziende.ToList();

            //QueryDiFiltro(ctx);
            //AddAzienda(ctx);
            //EditAziendaCompleta(ctx);
            //EditProprietaAzienda(ctx);
            //DeleteAzienda(ctx);
            //UpdateConLettura(ctx);
        }

        private void LoadWithExplicitLoading(MyDbContext ctx)
        {
            var dipendente = ctx.Dipendenti.ToList()
                .Where(w => w.IdDipendente == 1).First();

            ctx.Entry(dipendente)
                .Reference(i => i.AziendaDoveLavora)
                .Load();
        }

        private void LoadWithEagerLoading(MyDbContext ctx)
        {
            var dipendente = ctx.Dipendenti
                .Include(x => x.AziendaDoveLavora)
                .Where(w => w.IdDipendente == 1).First();
        }

        private void LoadWithLazyLoading(MyDbContext ctx)
        {
            var dipendente = ctx.Dipendenti
                .Where(w => w.IdDipendente == 1).First();

            var nomeCittaAzienda = dipendente.AziendaDoveLavora.Citta;
        }

        private void UpdateConLettura(MyDbContext ctx)
        {
            var azienda = ctx.Aziende.Where(w => w.IdAzienda == 1).FirstOrDefault();
            azienda.RagioneSociale = "PCSNET MODIFICATA 3";
            ctx.SaveChanges();
        }

        private void DeleteAzienda(MyDbContext ctx)
        {
            var deleteAzienda = new Azienda();
            deleteAzienda.IdAzienda = 3;
            //prima prendiamo la entry, per dire che l'elemento è tracciato
            var entry = ctx.Entry(deleteAzienda);
            entry.State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            ctx.SaveChanges();
        }

        private void EditProprietaAzienda(MyDbContext ctx)
        {
            var editAzienda = new Azienda();
            editAzienda.IdAzienda = 1;
            editAzienda.RagioneSociale = "PCSNET MODIFICATA 2";
            var entry = ctx.Entry(editAzienda);
            //in questo modo, viene marcata soltanto la proprietà come modificata
            //non l'intero record
            entry.Property(p => p.RagioneSociale).IsModified = true;
            ctx.SaveChanges();
        }

        private void EditAziendaCompleta(MyDbContext ctx)
        {
            var editAzienda = new Azienda();
            editAzienda.IdAzienda = 1;
            editAzienda.RagioneSociale = "PCSNET MODIFICATA";
            editAzienda.Citta = "Tolentino";
            editAzienda.Cap = "12345";
            //in questa maniera, l'oggetto sarà tracciato e verranno effettuate le modifiche
            //quando un elemento viene marcato come modified, vengono modificate tutte le colonne
            //senza questo modified, non avviene nulla, perchè non sa quale oggetto deve tracciare
            ctx.Entry(editAzienda).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            ctx.SaveChanges();
        }

        private void AddAzienda(MyDbContext ctx)
        {
            var newAzienda = new Azienda();
            newAzienda.Citta = "Civitanova Marche";
            newAzienda.Cap = "11111";
            newAzienda.RagioneSociale = "AZIENDA TEST";
            /*se non metto citta, automaticamente sarebbe nullabile
             *nel database ho messo l'impostazione che non deve essere nullabile
             *quindi non salverà le modifiche */
            //ctx.Aziende.Add(newAzienda);
            //serve a persistere le modifiche sul database
            ctx.SaveChanges();

            Console.WriteLine($"Creata azienda con id {newAzienda.IdAzienda}");

        }

        private void QueryDiFiltro(MyDbContext ctx)
            {
                //Aziende che iniziano con la lettera p
                var aziendeConPInizialeSintassiSqlLite = from a in ctx.Aziende
                                                         where a.RagioneSociale.StartsWith("p")
                                                         select a;
                var aziendeConPSintassiFluida
                    = ctx.Aziende.Where(w => w.RagioneSociale.StartsWith("p"));

                foreach (var azienda in aziendeConPSintassiFluida)
                {
                    Console.WriteLine(azienda);
                }
            }
        }
    }

