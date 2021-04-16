using Hsec.Domain.Entities.Capacitaciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Infrastructure.Persistence.Configurations.Capacitaciones
{
    public class PeligroConfiguration : IEntityTypeConfiguration<TPeligro>
    {
        public void Configure(EntityTypeBuilder<TPeligro> builder)
        {
            builder.HasKey(o => o.CodPeligro);
            builder.Property(f => f.CodPeligro)
              .HasMaxLength(50)
              .ValueGeneratedNever();
        }
    }
}
