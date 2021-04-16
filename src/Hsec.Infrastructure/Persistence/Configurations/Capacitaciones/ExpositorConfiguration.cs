using Hsec.Domain.Entities.Capacitaciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Infrastructure.Persistence.Configurations.Capacitaciones
{
    public class ExpositorConfiguration : IEntityTypeConfiguration<TExpositor>
    {
        public void Configure(EntityTypeBuilder<TExpositor> builder)
        {
            builder.HasKey(o => new { o.CodPersona, o.CodCurso });

            builder.HasOne(t => t.Curso)
                .WithMany(t => t.Expositores)
                .HasForeignKey(o => o.CodCurso);

            builder.Property(f => f.CodCurso)
              .HasMaxLength(50)
              .ValueGeneratedNever();

            builder.Property(f => f.CodPersona)
              .HasMaxLength(50)
              .ValueGeneratedNever();
        }
    }
}