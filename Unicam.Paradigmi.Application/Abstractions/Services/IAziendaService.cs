using Unicam.Paradigmi.Test.Models;

namespace Unicam.Paradigmi.Application.Abstractions.Services
{
    public interface IAziendaService
    {
        List<Azienda> GetAziende();

        void AddAzienda(Azienda azienda);

        Azienda GetAzienda(int id);
    }

}
