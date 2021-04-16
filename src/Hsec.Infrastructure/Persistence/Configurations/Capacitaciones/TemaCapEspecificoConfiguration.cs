using Hsec.Domain.Entities.Capacitaciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Infrastructure.Persistence.Configurations.Capacitaciones
{
    public class TemaCapEspecificoConfiguration : IEntityTypeConfiguration<TTemaCapEspecifico>
    {
        public void Configure(EntityTypeBuilder<TTemaCapEspecifico> builder)
        {
            builder.HasKey(t => new { t.CodTemaCapacita , t.CodPeligro});          

            builder.HasOne(t => t.TemaCapacitacion)
               .WithMany(t => t.TemaCapEspecifico)
               .HasForeignKey(t => t.CodTemaCapacita);

            builder.Property(f => f.CodTemaCapacita)
             .HasMaxLength(50)
             .ValueGeneratedNever();
            builder.Property(f => f.CodPeligro)
             .HasMaxLength(50)
             .ValueGeneratedNever();
            builder.Property(f => f.CodRiesgo)
            .HasMaxLength(50)
            .ValueGeneratedNever();
        }
    }
}