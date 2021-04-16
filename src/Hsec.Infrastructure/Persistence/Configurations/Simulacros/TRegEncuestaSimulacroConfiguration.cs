using Hsec.Domain.Common;
using Hsec.Domain.Entities.Simulacros;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Hsec.Infrastructure.Persistence.Configurations.Simulacros
{
    public class TRegEncuestaSimulacroConfiguration : IEntityTypeConfiguration<TRegEncuestaSimulacro>
    {
        public void Configure(EntityTypeBuilder<TRegEncuestaSimulacro> builder)
        {
            builder.HasKey(k => new { k.CodSimulacro, k.CodPregunta, k.CodRespuesta }).HasName("PK_TSimulacro_Encuesta");
            builder.HasOne(o => o.Simulacro).WithMany(m => m.RegistroEncuesta).HasForeignKey(f => f.CodSimulacro).HasConstraintName("FK_1n_Simulacro_Encuesta");
            builder.Property(t => t.CodSimulacro).HasMaxLength(20);
            builder.Property(t => t.CodTabla).HasMaxLength(20);
            builder.Property(t => t.CodPregunta).HasMaxLength(20);
            builder.Property(t => t.CodRespuesta).HasMaxLength(20);
        }
    }
}