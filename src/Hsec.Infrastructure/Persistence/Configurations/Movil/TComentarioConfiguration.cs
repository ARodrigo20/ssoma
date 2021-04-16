using Hsec.Domain.Entities.Movil;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Movil
{
    public class TComentarioConfiguration : IEntityTypeConfiguration<TComentario>
    {
        public void Configure(EntityTypeBuilder<TComentario> builder)
        {
            builder.HasKey(t => t.Correlativo);
            builder.Property(t => t.Correlativo).UseIdentityColumn();
            builder.Property(t => t.NroReferencia)
                .HasMaxLength(20)
                .IsRequired(false);
            builder.Property(t => t.CodPersona)
                .HasMaxLength(20)
                .IsRequired(false);
            builder.Property(t => t.Comentario)
                .HasColumnType("nvarchar(MAX)")
                .IsRequired(false);
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