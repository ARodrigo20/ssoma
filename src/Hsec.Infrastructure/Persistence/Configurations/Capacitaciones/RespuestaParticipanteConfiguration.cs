using Hsec.Domain.Entities.Capacitaciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Capacitaciones
{
    public class RespuestaParticipanteConfiguration : IEntityTypeConfiguration<TRespuestaParticipante>
    {
        public void Configure(EntityTypeBuilder<TRespuestaParticipante> builder)
        {
            builder.HasKey(o => new {o.CodCurso, o.CodPersona,o.CodPregunta});

            builder.Property(f => f.CodCurso)
             .HasMaxLength(50)
             .ValueGeneratedNever();
            builder.Property(f => f.CodPersona)
             .HasMaxLength(50)
             .ValueGeneratedNever();
            builder.Property(f => f.CodPregunta)
             .ValueGeneratedNever();
        }
    }
}
