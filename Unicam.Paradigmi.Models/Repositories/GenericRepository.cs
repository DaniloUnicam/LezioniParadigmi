using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicam.Paradigmi.Models.Context;

namespace Unicam.Paradigmi.Models.Repositories
{
    public abstract class GenericRepository<T> where T : class
    {

        protected MyDbContext _ctx;
        public GenericRepository(MyDbContext ctx) {
            _ctx = ctx;
        }

        public void Aggiungi<T>(T entity)
        {
            _ctx.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
        }

        public void Modifica<T>(T entity)
        {
            _ctx.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
        

        public T Ottieni(Object id)
        {
            return _ctx.Set<T>().Find(id);
        }

        public void Elimina(Object id)
        {
            var entity = Ottieni(id);
            _ctx.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
        }

        public void Save()
        {
            _ctx.SaveChanges();
        }


    }
}
