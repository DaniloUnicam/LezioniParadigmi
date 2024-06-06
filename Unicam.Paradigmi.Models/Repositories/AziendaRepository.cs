using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicam.Paradigmi.Models.Context;
using Unicam.Paradigmi.Models.Entities;
using Unicam.Paradigmi.Test.Models;

namespace Unicam.Paradigmi.Models.Repositories
{
    public class AziendaRepository : GenericRepository<Azienda>
    {

        public AziendaRepository(MyDbContext ctx) : base(ctx)
        {
        }

    }
}
