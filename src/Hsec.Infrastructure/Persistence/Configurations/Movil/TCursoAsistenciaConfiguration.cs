using Hsec.Domain.Entities.Movil;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Movil
{
    public class TCursoAsistenciaConfiguration : IEntityTypeConfiguration<TCursoAsistencia>
    {
        public void Configure(EntityTypeBuilder<TCursoAsistencia> builder)
        {
            builder.HasKey(k => new { k.CodPersona, k.CodCurso, k.Fecha })
                .HasName("XPKTCursoAsistencia");
            builder.Property(t => t.CodPersona)
                .HasMaxLength(20);
            builder.Property(t => t.CodCurso)
                .HasMaxLength(20);

            builder.Property(t => t.UsuCreacion)
                .HasMaxLength(20)
                .IsRequired(false);
            builder.Property(t => t.UsuModifica)
                .HasMaxLength(20)
                .IsRequired(false);
            builder.Property(t => t.Estado)
                .HasMaxLength(2)
                .IsRequired(false);
        }
    }
}