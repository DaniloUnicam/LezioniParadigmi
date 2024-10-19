using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Unicam.Paradigmi.Application.Abstractions.Services;
using Unicam.Paradigmi.Models;
using Unicam.Paradigmi.Models.Repositories;
using Unicam.Paradigmi.Test.Models;

namespace Unicam.Paradigmi.Application.Services
{
    public class AziendaService : IAziendaService
    {
        //la best practice sarebbe di utilizzare l'interfaccia invece dell'oggetto
        //IAziendaRepository
        private readonly AziendaRepository _aziendaRepository;
        public AziendaService(AziendaRepository aziendaRepository)
        {
            _aziendaRepository = aziendaRepository;
        }

        public List<Azienda> GetAziende()
        {
            return _aziendaRepository.OttieniTutti();
        }

        //potevo dichiarare void, per poi lanciare un'eccezione che avrà un costo, anche
        //in performance,
        //ma renderà il codice più leggibile
        //bool è meno costoso
        public void AddAzienda(Azienda azienda)
        {
            _aziendaRepository.Aggiungi(azienda);
            _aziendaRepository.Save();
        }

        public Azienda GetAzienda(int id)
        {
            return _aziendaRepository.Ottieni(id);
        }
    }
}
