using Hsec.Domain.Entities.Capacitaciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Infrastructure.Persistence.Configurations.Capacitaciones
{
    public class PreguntasConfiguration : IEntityTypeConfiguration<TPreguntas>
    {
        public void Configure(EntityTypeBuilder<TPreguntas> builder)
        {
            builder.HasKey(o => o.CodPregunta); //listo                       

            builder.HasOne(t => t.Curso)
                .WithMany(t => t.Preguntas)
                .HasForeignKey(o => o.CodCurso);

            builder.HasMany(t => t.Alternativas)
              .WithOne(t => t.Preguntas)
              .HasForeignKey(t => t.CodPregunta);

            builder.Property(f => f.CodCurso).HasMaxLength(50);
            builder.Property(f => f.CodPregunta).HasMaxLength(50).ValueGeneratedOnAdd();      
        }
    }
    
}