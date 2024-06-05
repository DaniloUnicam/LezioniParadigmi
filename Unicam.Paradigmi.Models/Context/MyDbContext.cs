using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicam.Paradigmi.Models.Configurations;
using Unicam.Paradigmi.Models.Entities;
using Unicam.Paradigmi.Test.Models;

namespace Unicam.Paradigmi.Models.Context
{
    public class MyDbContext : DbContext
    {
        public DbSet<Azienda> Aziende {  get; set; }

        public DbSet<Dipendente> Dipendenti { get; set; }

        //in questo modo, quando verrà creato un contesto, gli sarà detto di utilizzare sqlserver
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("data source=localhost;Initial catalog= paradigmi;User Id=paradigmi;Password=paradigmi;")
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //in questo modo applica la configurazione a tutto l'assembly,
            //prendendo tutte le classi che implementano IEntityTypeConfiguration
            //e caricandole automaticamente
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            /*senza l'applyconfigurationfromassembly, dovrei farlo per ogni classe
             *come sotto indicato
             *modelBuilder.ApplyConfiguration<Azienda>(new AziendaConfiguration());
             */
            base.OnModelCreating(modelBuilder);
        }
    }
}
