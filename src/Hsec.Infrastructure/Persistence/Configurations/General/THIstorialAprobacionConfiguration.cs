using Hsec.Domain.Entities.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.General
{
    public class THistorialAprobacionConfiguration : IEntityTypeConfiguration<THistorialAprobacion>
    {
        public void Configure(EntityTypeBuilder<THistorialAprobacion> builder)
        {
            builder.HasKey(t => t.Correlativo);
            builder.HasOne(o => o.Aprobacion)
                .WithMany(m => m.Historial) // lista de la tabla referida 
                .HasForeignKey(f => f.CodAprobacion) // clave de la clase
                .HasConstraintName("FK_Aprobacion_Historial");
            builder.Property(t => t.CodAprobacion)
                .HasMaxLength(20);
            builder.Property(t => t.CodAprobador)
                .HasMaxLength(20);
            builder.Property(t => t.Comentario)
                .HasMaxLength(500);
            builder.Property(t => t.EstadoAprobacion)
                .HasMaxLength(1);
            
        }
    }
}
