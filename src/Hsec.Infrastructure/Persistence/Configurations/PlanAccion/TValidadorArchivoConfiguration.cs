using Hsec.Domain.Entities.PlanAccion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Infrastructure.Persistence.Configurations.PlanAccion
{
    public class TValidadorArchivoConfiguration : IEntityTypeConfiguration<TValidadorArchivo>
    {
        public void Configure(EntityTypeBuilder<TValidadorArchivo> builder)
        {
            builder.HasKey(t => t.Correlativo);
            builder.Property(f => f.Correlativo)
              .HasMaxLength(25)
              .ValueGeneratedOnAdd();
        }
    }
}