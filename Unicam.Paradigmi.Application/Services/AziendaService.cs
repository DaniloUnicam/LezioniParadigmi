using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Unicam.Paradigmi.Application.Abstractions.Services;
using Unicam.Paradigmi.Models;
using Unicam.Paradigmi.Test.Models;

namespace Unicam.Paradigmi.Application.Services
{
    public class AziendaService : IAziendaService
    {
        public List<Azienda> GetAziende()
        {
            return new List<Azienda>();
        }

    }
}
