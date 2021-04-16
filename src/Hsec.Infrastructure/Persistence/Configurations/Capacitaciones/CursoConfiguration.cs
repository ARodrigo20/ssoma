using Hsec.Domain.Entities.Capacitaciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Capacitaciones
{
    public class CursoConfiguration : IEntityTypeConfiguration<TCurso>
    {
        public void Configure(EntityTypeBuilder<TCurso> builder)
        {
            builder.HasKey(o => o.CodCurso);

            builder.HasMany(t => t.Participantes)
               .WithOne(t => t.Curso)
               .HasForeignKey(t => t.CodCurso);

            builder.HasMany(t => t.Expositores)
               .WithOne(t => t.Curso)
                .HasForeignKey(t => t.CodCurso);

            builder.HasMany(t => t.Preguntas)
               .WithOne(t => t.Curso)
                .HasForeignKey(t => t.CodCurso);

            builder.Property(f => f.CodCurso)
              .HasMaxLength(50)
              .ValueGeneratedNever();
        }
    }
}
