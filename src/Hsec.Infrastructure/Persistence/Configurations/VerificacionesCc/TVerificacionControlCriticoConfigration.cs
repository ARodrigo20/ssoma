using Hsec.Domain.Common;
using Hsec.Domain.Entities.VerficacionesCc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Hsec.Infrastructure.Persistence.Configurations.VerficacionesCc
{
    public class TVerificacionControlCriticoConfigration: IEntityTypeConfiguration<TVerificacionControlCritico>
    {

        public void Configure(EntityTypeBuilder<TVerificacionControlCritico> builder)
        {
            builder.HasKey(t => new { t.CodigoVCC });

            builder.Property(t => t.CodigoVCC).HasMaxLength(20);
            // builder.Property(t => t.Fecha).UseIdentityColumn();
            builder.Property(t => t.CodResponsable).HasMaxLength(20);
            builder.Property(t => t.Empresa).HasMaxLength(200);
            builder.Property(t => t.Gerencia).HasMaxLength(20);
            builder.Property(t => t.SuperIndendecnia).HasMaxLength(20);
            builder.Property(t => t.Cartilla).HasMaxLength(20);

        }
    }
}
