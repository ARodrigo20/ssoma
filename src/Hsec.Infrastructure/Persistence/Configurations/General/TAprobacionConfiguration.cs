using Hsec.Domain.Entities.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.General
{
    public class TAprobacionConfiguration : IEntityTypeConfiguration<TAprobacion>
    {
        public void Configure(EntityTypeBuilder<TAprobacion> builder)
        {
            builder.HasKey(t => t.CodAprobacion);

            builder.Property(t => t.DocReferencia)
                .HasMaxLength(20);
            builder.Property(t => t.ProcesoAprobacion)
                .HasMaxLength(400);
            builder.Property(t => t.Version)
                .HasMaxLength(5);
            builder.Property(t => t.CadenaAprobacion)
                .HasMaxLength(400);
            builder.Property(t => t.EstadoDoc)
                .HasMaxLength(1);

        }
    }
}
