using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicam.Paradigmi.Test.Models;

namespace Unicam.Paradigmi.Models.Configurations
{
    /*invece di utilizzare [Table("Aziende")]
     *seguito da [Key] public int IdAzienda {} per specificare che questa è la tabella
     *che si sta interrogando con le proprie colonne con la propria chiave,
     *per evitare cambiamenti futuri riguardo Entity Framework, quindi utilizzare altri approcci
     *questo è il metodo meno veloce e più efficace, una classe per configurare il tutto.
     */
    public class AziendaConfiguration : IEntityTypeConfiguration<Azienda>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Azienda> builder)
        {
            /*queste informazioni ci permettono di gestire la configurazione
             *tra la classe Azienda
             *e ciò che è persistito sul nostro ATTUALE database (qualsiasi quindi) */

            builder.ToTable("Aziende");
            builder.HasKey(p => p.IdAzienda);
            builder.Property(p => p.RagioneSociale).HasMaxLength(100);
        }
    }
}
