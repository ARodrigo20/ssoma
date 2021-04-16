using Hsec.Domain.Common;
using Hsec.Domain.Entities.Simulacros;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Hsec.Infrastructure.Persistence.Configurations.Simulacros
{
    public class TSimulacroConfiguration : IEntityTypeConfiguration<TSimulacro>
    {
        public void Configure(EntityTypeBuilder<TSimulacro> builder)
        {
            builder.HasKey(t => new { t.CodSimulacro });

            builder.Property(t => t.CodSimulacro).HasMaxLength(20);
            builder.Property(t => t.CodTabla).HasMaxLength(10);
            builder.Property(t => t.CodUbicacion).HasMaxLength(20);
            builder.Property(t => t.CodPosicionGer).HasMaxLength(20);
            builder.Property(t => t.CodRespGerencia).HasMaxLength(20);
            builder.Property(t => t.CodPosicionSup).HasMaxLength(20);
            builder.Property(t => t.CodRespSuperint).HasMaxLength(20);
            builder.Property(t => t.CodContrata).HasMaxLength(20);
            builder.Property(t => t.Suceso).HasMaxLength(100);
            builder.Property(t => t.Hora).HasMaxLength(10);
            builder.Property(t => t.HoraInicio).HasMaxLength(10);
            builder.Property(t => t.HoraFinalizacion).HasMaxLength(10);
            builder.Property(t => t.TiempoRespuesta).HasMaxLength(15);
            builder.Property(t => t.Conclusiones).HasMaxLength(200);
        }
    }
}