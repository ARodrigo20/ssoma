using Hsec.Domain.Entities.Capacitaciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Infrastructure.Persistence.Configurations.Capacitaciones
{
    public class RiesgoConfiguration : IEntityTypeConfiguration<TRiesgo>
    {
        public void Configure(EntityTypeBuilder<TRiesgo> builder)
        {
            builder.HasKey(o => o.CodRiesgo);
            builder.Property(f => f.CodRiesgo)
              .HasMaxLength(50)
              .ValueGeneratedNever();
        }
    }
}
