using Hsec.Domain.Entities.YoAseguro;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.YoAseguro
{
    public class TYoAseguroLugarConfiguration : IEntityTypeConfiguration<TYoAseguroLugar>
    {
        public void Configure(EntityTypeBuilder<TYoAseguroLugar> builder)
        {
            builder.HasKey(k => k.CodUbicacion);
            builder.Property(t => t.CodUbicacion)
                .HasMaxLength(20)
                .IsRequired();
            builder.Property(t => t.CodUbicacionPadre)
                .HasMaxLength(20)
                .IsRequired(false);
            builder.Property(t => t.Descripcion)
                .HasMaxLength(100)
                .IsRequired(false);
        }
    }
}
