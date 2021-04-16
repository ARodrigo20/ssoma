using Hsec.Domain.Common;
using Hsec.Domain.Entities.Auditoria;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Hsec.Infrastructure.Persistence.Configurations.Auditoria
{
    public class TAudCCCriterioConfiguration : IEntityTypeConfiguration<TAudCCCriterio>
    {
        public void Configure(EntityTypeBuilder<TAudCCCriterio> builder)
        {
            // builder.Property(t => t.Correlativo).UseIdentityColumn(1,1);
            builder.HasKey(t => new { t.CodAuditoria , t.CodCartilla, t.CodCC, t.CodCriterio });

            builder.Property(t => t.CodAuditoria).HasMaxLength(20);
            builder.Property(t => t.CodCartilla).HasMaxLength(20);
            builder.Property(t => t.CodCC).HasMaxLength(20);
            builder.Property(t => t.CodCriterio).HasMaxLength(20);
            
            builder
                .HasOne(t => t.CodNavigateTAudCartilla)
                .WithMany(t => t.TAudCCCriterio)
                .HasForeignKey(t => new {t.CodAuditoria,t.CodCartilla});
        }
    }
}