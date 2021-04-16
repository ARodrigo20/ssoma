using Hsec.Domain.Entities.Capacitaciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Infrastructure.Persistence.Configurations.Capacitaciones
{
    public class AcreditacionCursoConfiguration : IEntityTypeConfiguration<TAcreditacionCurso>
    {
        public void Configure(EntityTypeBuilder<TAcreditacionCurso> builder)
        {
            builder.HasKey(o => new { o.CodCurso, o.CodPersona });

            builder.Property(f => f.CodCurso)
             .HasMaxLength(50)
             .ValueGeneratedNever();

            builder.Property(f => f.CodPersona)
             .HasMaxLength(50)
             .ValueGeneratedNever();
        }
    }
}