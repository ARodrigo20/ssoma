using Hsec.Domain.Common;
using Hsec.Domain.Entities.Verficaciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Hsec.Infrastructure.Persistence.Configurations.Verficaciones
{
    public class TVerificacionPTARConfigration: IEntityTypeConfiguration<TVerificacionPTAR>
    {

        public void Configure(EntityTypeBuilder<TVerificacionPTAR> builder)
        {
            builder.HasKey(t => new { t.CodVerificacion, t.Correlativo });
            
            builder.Property(t => t.CodVerificacion).HasMaxLength(20);
			builder.Property(t => t.Correlativo).UseIdentityColumn();
			builder.Property(t => t.CodTabla).HasMaxLength(10);
			builder.Property(t => t.StopWork).HasMaxLength(20);
            builder.Property(t => t.P1_IdentificoRelacionados)
                .IsRequired(true);
            builder.Property(t => t.P2_ControlesImplementados)
                .IsRequired(true);
            builder.Property(t => t.P3_ReviseElContenido)
                .IsRequired(true);
            builder.Property(t => t.P4_CorrespondeAlEjecutado)
                .IsRequired(true);
            builder.Property(t => t.P5_NoSeEjecutaElTrabajo)
                .IsRequired(true);
            builder.Property(t => t.P6_RevisadoyFirmado)
                .IsRequired(true);
    }
    }
}
