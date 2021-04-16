using Hsec.Domain.Entities.Observaciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Observaciones
{
    public class TReportePFConfiguration : IEntityTypeConfiguration<TReportePF>
    {
        public void Configure(EntityTypeBuilder<TReportePF> builder)
        {
            builder.HasKey(t => t.CodReportePF);
        }
    }
}