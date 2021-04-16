using Hsec.Domain.Common;
using Hsec.Domain.Entities.Auditoria;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Hsec.Infrastructure.Persistence.Configurations.Auditoria
{
    public class TAudCartillaConfiguration : IEntityTypeConfiguration<TAudCartilla>
    {
        public void Configure(EntityTypeBuilder<TAudCartilla> builder)
        {
            // builder.Property(t => t.Correlativo).UseIdentityColumn(1,1);
            builder.HasKey(t => new { t.CodAuditoria , t.CodCartilla });

            builder.Property(t => t.CodAuditoria).HasMaxLength(20);
            builder.Property(t => t.CodCartilla).HasMaxLength(20);
            builder.Property(t => t.Descripcion).HasMaxLength(2000);
            
        }
    }
}