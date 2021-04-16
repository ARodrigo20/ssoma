using Hsec.Domain.Common;
using Hsec.Domain.Entities.Auditoria;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Hsec.Infrastructure.Persistence.Configurations.Auditoria
{
    public class TAuditoriaTregNoConfoObservaConfiguration: IEntityTypeConfiguration<TAuditoriaTregNoConfoObserva>
    {

        public void Configure(EntityTypeBuilder<TAuditoriaTregNoConfoObserva> builder)
        {
            builder.HasKey(t => new { t.CodAuditoria , t.CodNoConformidad });
            builder
                .HasOne(t => t.CodAuditoriaNavigation)
                .WithMany(t => t.TAuditoriaTregNoConfoObserva)
                .HasForeignKey(t => t.CodNoConformidad);
        }
    }
}
