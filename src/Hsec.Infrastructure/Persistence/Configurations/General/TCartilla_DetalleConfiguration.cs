using Hsec.Domain.Entities.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.General
{
    public class TCartilla_DetalleConfiguration : IEntityTypeConfiguration<TCartillaDetalle>
    {
        public void Configure(EntityTypeBuilder<TCartillaDetalle> builder)
        {
            builder.HasKey(cd => new {cd.CodCartillaDet });
            builder.Property(t => t.CodCartilla)
               .HasMaxLength(50)
               .IsRequired(true);
            builder.Property(t => t.CodCartillaDet)
                .HasMaxLength(50)
                .IsRequired(true);
            builder.Property(t => t.CodCC)
               .HasMaxLength(50)
               .IsRequired(false);

            builder.HasOne(c => c.Cartilla)
               .WithMany(m => m.Detalle)
               .HasForeignKey(f => f.CodCartilla)
               .IsRequired(true);

            builder.HasOne(c => c.CC)
                .WithMany(m => m.CartillaDetalles)
                .HasForeignKey(f => f.CodCC)
                .IsRequired(false);
        }
    }   
}
