using Hsec.Domain.Entities.Auditoria;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Hsec.Infrastructure.Persistence.Configurations.Auditoria
{
    public class TDatosHallazgoConfiguration : IEntityTypeConfiguration<TDatosHallazgo>
    {
  
        public void Configure(EntityTypeBuilder<TDatosHallazgo> builder)
        {
            builder.HasKey(t => new { t.Correlativo, t.CodHallazgo });

            builder.Property(t => t.Correlativo).HasMaxLength(20);
            builder.Property(t => t.CodHallazgo).HasMaxLength(20);
            builder.Property(t => t.CodTipoHallazgo).HasMaxLength(20);
            builder.Property(t => t.Descripcion).HasMaxLength(2000);

            // builder
            //     .HasOne(t => t.CodIncidenteNavigation)
            //     .WithMany(t => t.Ticam)
            //     .HasForeignKey(t => t.CodIncidente);
        }
    }
}
