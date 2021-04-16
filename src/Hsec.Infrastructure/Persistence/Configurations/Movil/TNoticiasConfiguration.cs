using Hsec.Domain.Entities.Movil;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Movil
{
    public class TNoticiasConfiguration : IEntityTypeConfiguration<TNoticias>
    {
        public void Configure(EntityTypeBuilder<TNoticias> builder)
        {
            builder.HasKey(t => t.CodNoticia);
            builder.Property(t => t.CodNoticia)
                .HasMaxLength(20);

            builder.Property(t => t.Titulo)
                .HasMaxLength(200)
                .IsRequired(false);
            builder.Property(t => t.Autor)
                .HasMaxLength(20)
                .IsRequired(false);
            builder.Property(t => t.Tipo)
                .HasMaxLength(10)
                .IsRequired(false);
            builder.Property(t => t.Descripcion)
                .HasColumnType("nvarchar(MAX)")
                .IsRequired(false);
            builder.Property(t => t.DescripcionCorta)
                .HasMaxLength(200)
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