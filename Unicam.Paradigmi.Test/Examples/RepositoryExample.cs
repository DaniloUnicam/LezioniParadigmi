using Unicam.Paradigmi.Abstractions;
using Unicam.Paradigmi.Models.Context;
using Unicam.Paradigmi.Models.Entities;
using Unicam.Paradigmi.Models.Repositories;
using Unicam.Paradigmi.Test.Models;

namespace Unicam.Paradigmi.Test.Examples
{
    public class RepositoryExample : IExample
    {
        public async Task RunExampleAsync()
        {

        }

        public void RunExample()
        {
            var ctx = new MyDbContext();

            var dipendenteRepository = new DipendenteRepository(ctx);
            var aziendaRepository = new AziendaRepository(ctx);

            var dipendente = dipendenteRepository.Ottieni(1);
            var azienda = aziendaRepository.Ottieni(1);
            azienda.Citta = "nuova";
            aziendaRepository.Modifica(azienda);
            aziendaRepository.Save();

            var nuovoDipendente = new Dipendente();
            nuovoDipendente.DataNascita = new DateTime(2000, 1, 1);
            nuovoDipendente.Nome = "Pippo";
            nuovoDipendente.Cognome = "Rossi";
            //l'entity framework ci permette di non specificare la chiave e creare un nuovo dipendente in essa
            //nuovoDipendente.IdAzienda = 10;

            nuovoDipendente.AziendaDoveLavora = new Azienda();
            
            nuovoDipendente.AziendaDoveLavora.RagioneSociale = "Nuova Azienda";
            nuovoDipendente.AziendaDoveLavora.Cap = "0123";
            nuovoDipendente.AziendaDoveLavora.Citta = "Camerino";
            

            dipendenteRepository.Aggiungi(nuovoDipendente);
            dipendenteRepository.Save();
        }


    }
}
