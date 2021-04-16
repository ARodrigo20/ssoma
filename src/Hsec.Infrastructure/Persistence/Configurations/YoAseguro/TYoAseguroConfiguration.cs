using Hsec.Domain.Entities.YoAseguro;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.YoAseguro
{
    public class TYoAseguroConfiguration : IEntityTypeConfiguration<TYoAseguro>
    {
        public void Configure(EntityTypeBuilder<TYoAseguro> builder)
        {
            builder.HasKey(t => t.CodYoAseguro);
            builder.Property(t => t.CodPosGerencia)
                .HasMaxLength(20)
                .IsRequired(false);
            builder.Property(t => t.CodPersonaResponsable)
                .HasMaxLength(20)
                .IsRequired(false);
            builder.Property(t => t.ObsCriticaDia)
                .HasMaxLength(2000)
                .IsRequired(false);
            builder.Property(t => t.Calificacion)
                .HasMaxLength(10)
                .IsRequired(false);
            builder.Property(t => t.Comentario)
                .HasMaxLength(2000)
                .IsRequired(false);
            builder.Property(t => t.Reunion)
                .HasMaxLength(500)
                .IsRequired(false);
            builder.Property(t => t.Recomendaciones)
                .HasMaxLength(2000)
                .IsRequired(false);
            builder.Property(t => t.TituloReunion)
                .HasMaxLength(2000)
                .IsRequired(false);
            builder.Property(t => t.TemaReunion)
                .HasMaxLength(4000)
                .IsRequired(false);
        }
    }
}
