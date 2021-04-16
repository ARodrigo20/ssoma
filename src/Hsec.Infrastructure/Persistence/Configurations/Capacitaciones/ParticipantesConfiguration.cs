using Hsec.Domain.Entities.Capacitaciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Infrastructure.Persistence.Configurations.Capacitaciones
{
    public class ParticipantesConfiguration : IEntityTypeConfiguration<TParticipantes>
    {
        public void Configure(EntityTypeBuilder<TParticipantes> builder)
        {
            builder.HasKey(o => new { o.CodPersona, o.CodCurso });

            builder.HasOne(t => t.Curso)
                .WithMany(t => t.Participantes)
                .HasForeignKey(o => o.CodCurso);

            builder.Property(f => f.CodPersona)
              .HasMaxLength(50)
              .ValueGeneratedNever();

            builder.Property(f => f.CodCurso)
              .HasMaxLength(50)
              .ValueGeneratedNever();
        }
    }
}
