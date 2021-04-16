using Hsec.Domain.Common;
using Hsec.Domain.Entities.Simulacros;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Hsec.Infrastructure.Persistence.Configurations.Simulacros
{
    public class TObservacionSimulacroConfiguration : IEntityTypeConfiguration<TObservacionSimulacro>
    {
        public void Configure(EntityTypeBuilder<TObservacionSimulacro> builder)
        {
            builder.HasKey(k => new { k.CodSimulacro, k.Correlativo }).HasName("PK_TSimulacro_Observacion");
            builder.HasOne(o => o.Simulacro).WithMany(m => m.Observaciones).HasForeignKey(f => f.CodSimulacro).HasConstraintName("FK_1n_Simulacro_Observacion");
            builder.Property(t => t.CodSimulacro).HasMaxLength(20);
            builder.Property(t => t.Correlativo).UseIdentityColumn();
            builder.Property(t => t.Hora).HasMaxLength(10);
            builder.Property(t => t.Suceso).HasMaxLength(200);
        }
    }
}
