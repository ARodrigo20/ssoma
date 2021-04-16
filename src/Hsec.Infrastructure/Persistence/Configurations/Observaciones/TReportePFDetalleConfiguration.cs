using Hsec.Domain.Entities.Observaciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Observaciones
{
    public class TReportePFDetalleConfiguration : IEntityTypeConfiguration<TReportePFDetalle>
    {
        public void Configure(EntityTypeBuilder<TReportePFDetalle> builder)
        {
            builder.HasKey(k => new { k.CodReportePF, k.CodCC })
                .HasName("PK__TReporte__F758CEC892D8B74D");
        }
    }
}