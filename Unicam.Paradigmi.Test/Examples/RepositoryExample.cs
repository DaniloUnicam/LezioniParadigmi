using Unicam.Paradigmi.Abstractions;
using Unicam.Paradigmi.Models.Context;
using Unicam.Paradigmi.Models.Repositories;

namespace Unicam.Paradigmi.Test.Examples
{
    public class RepositoryExample : IExample
    {

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
        }


    }
}
