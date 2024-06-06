using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicam.Paradigmi.Models.Entities;

namespace Unicam.Paradigmi.Models.Configurations
{
    public class DipendenteConfiguration : IEntityTypeConfiguration<Dipendente>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Dipendente> builder)
        {
            builder.ToTable("Dipendenti");
            builder.HasKey(k => k.IdDipendente);
            //Il dipendente ha un'azienda dove lavora
            builder.HasOne(x => x.Azienda)
                //WithMany è una relazione uno a molti
                //L'azienda contiene più dipendenti
                .WithMany(x => x.Dipendenti);
                //L'azienda avrà una chiave esterna sull'id dell'azienda
                //.HasForeignKey(x => x.IdAzienda);
        }
    }
}
