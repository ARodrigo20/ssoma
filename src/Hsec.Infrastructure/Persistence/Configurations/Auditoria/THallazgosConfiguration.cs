using Hsec.Domain.Common;
using Hsec.Domain.Entities.Auditoria;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Hsec.Infrastructure.Persistence.Configurations.Auditoria
{
    public class THallazgosConfiguration : IEntityTypeConfiguration<THallazgos>
    {
        public void Configure(EntityTypeBuilder<THallazgos> builder)
        {
            // builder.Property(t => t.Correlativo).UseIdentityColumn(1,1);
            builder.HasKey(t => new { t.CodHallazgo , t.CodAuditoria });

            builder.Property(t => t.CodHallazgo).HasMaxLength(20);
            builder.Property(t => t.CodAuditoria).HasMaxLength(20);
            builder.Property(t => t.CodTabla).HasMaxLength(20);
            builder.Property(t => t.CodTipoHallazgo).HasMaxLength(20);
            
            builder
                .HasOne(t => t.CodNavigateAuditoria)
                .WithMany(t => t.THallazgos)
                .HasForeignKey(t => t.CodAuditoria);
        }
    }
}