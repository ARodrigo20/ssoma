using Hsec.Domain.Entities.Inspecciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Inspecciones
{
    public class TInspeccionConfiguration : IEntityTypeConfiguration<TInspeccion>
    {
        public void Configure(EntityTypeBuilder<TInspeccion> builder)
        {
            builder.HasKey(t => t.CodInspeccion);
            builder.Property(t => t.CodTabla)
                .HasMaxLength(20)
                .IsRequired(false);
            builder.Property(t => t.CodTipo)
                .HasMaxLength(20)
                .IsRequired(false);
            builder.Property(t => t.CodContrata)
                .HasMaxLength(20)
                .IsRequired(false);
            builder.Property(t => t.Gerencia)
                .HasMaxLength(10)
                .IsRequired(false);
            builder.Property(t => t.SuperInt)
                .HasMaxLength(10)
                .IsRequired(false);
            builder.Property(t => t.Hora)
                .HasMaxLength(10)
                .IsRequired(false);
            builder.Property(t => t.CodUbicacion)
                .HasMaxLength(10)
                .IsRequired(false);
            builder.Property(t => t.CodSubUbicacion)
                .HasMaxLength(10)
                .IsRequired(false);
            builder.Property(t => t.Objetivo)
                .HasMaxLength(2000)
                .IsRequired(false);
            builder.Property(t => t.Conclusion)
                .HasMaxLength(2000)
                .IsRequired(false);
            builder.Property(t => t.Dispositivo)
                .HasMaxLength(10)
                .IsRequired(false);
        }
    }
}
