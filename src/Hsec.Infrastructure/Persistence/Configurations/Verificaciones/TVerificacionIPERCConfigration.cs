using Hsec.Domain.Common;
using Hsec.Domain.Entities.Verficaciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Hsec.Infrastructure.Persistence.Configurations.Verficaciones
{
    public class TVerificacionIPERCConfigration: IEntityTypeConfiguration<TVerificacionIPERC>
    {

        public void Configure(EntityTypeBuilder<TVerificacionIPERC> builder)
        {
            builder.HasKey(t => new { t.CodVerificacion, t.Correlativo });
            
            builder.Property(t => t.CodVerificacion).HasMaxLength(20);
            builder.Property(t => t.Correlativo).UseIdentityColumn();
            builder.Property(t => t.CodTabla).HasMaxLength(10);
            builder.Property(t => t.StopWork).HasMaxLength(20);

        }
    }
}
