using Hsec.Domain.Common;
using Hsec.Domain.Entities.Auditoria;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Hsec.Infrastructure.Persistence.Configurations.Auditoria
{
    public class TAuditoriaConfiguration : IEntityTypeConfiguration<TAuditoria>
    {

        public void Configure(EntityTypeBuilder<TAuditoria> builder)
        {
            builder.HasKey(t => new { t.CodAuditoria });

            builder.Property(t => t.CodAuditoria).HasMaxLength(20);
            builder.Property(t => t.AuditoriaDescripcion).HasMaxLength(8000);
            builder.Property(t => t.CodPosicionGer).HasMaxLength(20);
            builder.Property(t => t.CodPosicionSup).HasMaxLength(20);
            builder.Property(t => t.CodContrata).HasMaxLength(20);
            builder.Property(t => t.CodRespAuditoria).HasMaxLength(20);
            builder.Property(t => t.CodTipoAuditoria).HasMaxLength(20);
            builder.Property(t => t.CodAreaAlcance).HasMaxLength(20);
            // builder
            //     .HasOne(t => t.CodIncidenteNavigation)
            //     .WithMany(t => t.TdetalleAfectado)
            //     .HasForeignKey(t => t.CodIncidente);
        }
    }
}
