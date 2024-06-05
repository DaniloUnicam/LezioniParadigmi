using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicam.Paradigmi.Abstractions;
using Unicam.Paradigmi.Models.Context;

namespace Unicam.Paradigmi.Test.Examples
{
    public class EntityFrameworkExample : IExample
    {
        public void RunExample()
        {
            var ctx = new MyDbContext();
            //questa è una query che farà il select di tutte le aziende con i loro record
            var aziende = ctx.Aziende.ToList();
        }
    }
}
