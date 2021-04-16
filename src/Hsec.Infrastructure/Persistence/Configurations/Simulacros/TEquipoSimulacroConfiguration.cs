using Hsec.Domain.Common;
using Hsec.Domain.Entities.Simulacros;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Hsec.Infrastructure.Persistence.Configurations.Simulacros
{
    public class TEquipoSimulacroConfiguration : IEntityTypeConfiguration<TEquipoSimulacro>
    {
        public void Configure(EntityTypeBuilder<TEquipoSimulacro> builder)
        {
            builder.HasKey(k => new { k.CodSimulacro, k.Correlativo }).HasName("PK_TSimulacro_Equipo");
            builder.HasOne(o => o.Simulacro).WithMany(m => m.EquipoSimulacro).HasForeignKey(f => f.CodSimulacro).HasConstraintName("FK_1n_Simulacro_Equipo");
            builder.Property(t => t.CodSimulacro).HasMaxLength(20);
            builder.Property(t => t.Correlativo).UseIdentityColumn();
            builder.Property(t => t.CodTabla).HasMaxLength(10);
            builder.Property(t => t.CodPersona).HasMaxLength(20);
            builder.Property(t => t.Lider).HasMaxLength(20);
        }
    }
}
