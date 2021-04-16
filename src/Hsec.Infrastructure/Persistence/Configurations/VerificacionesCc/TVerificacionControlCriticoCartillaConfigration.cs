using Hsec.Domain.Common;
using Hsec.Domain.Entities.VerficacionesCc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Hsec.Infrastructure.Persistence.Configurations.VerficacionesCc
{
    public class TVerificacionControlCriticoCartillaConfigration: IEntityTypeConfiguration<TVerificacionControlCriticoCartilla>
    {

        public void Configure(EntityTypeBuilder<TVerificacionControlCriticoCartilla> builder)
        {
            builder.HasKey(t => new { t.CodigoVCC, t.CodCartilla, t.CodCC });
            
            builder.Property(t => t.CodigoVCC).HasMaxLength(20);
            builder.Property(t => t.CodCartilla).HasMaxLength(20);
            builder.Property(t => t.CodCC).HasMaxLength(20);
            builder.Property(t => t.Cumplimiento).HasMaxLength(10);
            builder.Property(t => t.Efectividad).HasMaxLength(10);
            builder.Property(t => t.Justificacion).HasMaxLength(20);

        }
    }
}
