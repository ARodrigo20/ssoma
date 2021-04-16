using Hsec.Domain.Common;
using Hsec.Domain.Entities.Auditoria;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Hsec.Infrastructure.Persistence.Configurations.Auditoria
{
    public class TEquipoAuditorConfiguration: IEntityTypeConfiguration<TEquipoAuditor>
    {

        public void Configure(EntityTypeBuilder<TEquipoAuditor> builder)
        {
            builder.HasKey(t => new { t.Correlativo, t.CodAuditoria });
            
            builder.Property(t => t.CodAuditoria).HasMaxLength(20);
            builder.Property(t => t.CodTabla).HasMaxLength(20);
            builder.Property(t => t.CodPersona).HasMaxLength(20);
            builder.Property(t => t.Lider).HasMaxLength(20);
            builder
                .HasOne(t => t.CodAuditoriaNavigation)
                .WithMany(t => t.TEquipoAuditor)
                .HasForeignKey(t => t.CodAuditoria);
        }
    }
}
