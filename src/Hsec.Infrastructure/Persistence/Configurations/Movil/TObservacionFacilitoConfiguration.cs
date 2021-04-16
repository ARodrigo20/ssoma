using Hsec.Domain.Entities.Movil;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Movil
{
    public class TObservacionFacilitoConfiguration : IEntityTypeConfiguration<TObservacionFacilito>
    {
        public void Configure(EntityTypeBuilder<TObservacionFacilito> builder)
        {
            builder.HasKey(t => t.CodObsFacilito);
            builder.Property(t => t.CodObsFacilito)
                .HasMaxLength(20);

            builder.Property(t => t.Tipo)
                .HasMaxLength(1)
                .IsRequired(false);

            builder.Property(t => t.CodPosicionGer)
                .HasMaxLength(20)
                .IsRequired(false);
            builder.Property(t => t.CodPosicionSup)
                .HasMaxLength(20)
                .IsRequired(false);
            builder.Property(t => t.UbicacionExacta)
                .HasMaxLength(50)
                .IsRequired(false);
            builder.Property(t => t.Observacion)
                .HasMaxLength(500)
                .IsRequired(false);
            builder.Property(t => t.Accion)
                .HasMaxLength(500)
                .IsRequired(false);
            builder.Property(t => t.RespAuxiliar)
                .HasMaxLength(50)
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