using Hsec.Domain.Entities.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.General
{
    public class TMaestroConfiguration : IEntityTypeConfiguration<TMaestro>
    {
        public void Configure(EntityTypeBuilder<TMaestro> builder)
        {
            builder.HasKey(k => new { k.CodTabla, k.CodTipo })
                .HasName("PK_TMaestro_TablaTipo");
            builder.Property(t => t.CodTabla)
               .HasMaxLength(50)
               .IsRequired();
            builder.Property(t => t.CodTipo)
              .HasMaxLength(50)
              .IsRequired();
            builder.Property(t => t.Descripcion)
               .HasMaxLength(800)
               .IsRequired();
            builder.Property(t => t.DescripcionCorta)
               .HasMaxLength(600)
               .IsRequired(false);
        }
          
    }
}
