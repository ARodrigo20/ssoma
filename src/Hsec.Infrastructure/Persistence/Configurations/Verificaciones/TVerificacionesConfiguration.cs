using Hsec.Domain.Common;
using Hsec.Domain.Entities.Verficaciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Hsec.Infrastructure.Persistence.Configurations.Verficaciones
{
    public class TVerificacionesConfiguration : IEntityTypeConfiguration<TVerificaciones>
    {

        public void Configure(EntityTypeBuilder<TVerificaciones> builder)
        {
            builder.HasKey(t => new { t.CodVerificacion });
            
            builder.Property(t => t.CodVerificacion).HasMaxLength(20);
            builder.Property(t => t.CodTabla).HasMaxLength(10);
            builder.Property(t => t.CodPosicionGer).HasMaxLength(20);
            builder.Property(t => t.CodPosicionSup).HasMaxLength(20);
            builder.Property(t => t.CodAreaHSEC).HasMaxLength(20);
            builder.Property(t => t.CodTipoVerificacion).HasMaxLength(10);
            builder.Property(t => t.CodNivelRiesgo).HasMaxLength(4);
            builder.Property(t => t.CodVerificacionPor).HasMaxLength(20);
            builder.Property(t => t.HoraVerificacion).HasMaxLength(20);
            builder.Property(t => t.CodUbicacion).HasMaxLength(20);
            builder.Property(t => t.CodSubUbicacion).HasMaxLength(20);
            builder.Property(t => t.CodUbicacionEspecifica).HasMaxLength(20);
            builder.Property(t => t.DesUbicacion).HasMaxLength(500);
            builder.Property(t => t.Dispositivo).HasMaxLength(10);
        }
        //public virtual TMotivo CodMotivoNavigation { get; set; }
    }
}
