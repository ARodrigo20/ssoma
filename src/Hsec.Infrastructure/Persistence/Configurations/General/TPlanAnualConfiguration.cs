using Hsec.Domain.Entities.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.General
{
    public class TPlanAnualConfiguration : IEntityTypeConfiguration<TPlanAnual>
    {
        public void Configure(EntityTypeBuilder<TPlanAnual> builder)
        {
            builder.HasKey(t => new { t.Anio, t.CodMes, t.CodPersona, t.CodReferencia});
            builder.Property(t => t.Anio).HasMaxLength(20);
            builder.Property(t => t.CodMes).HasMaxLength(20);
            builder.Property(t => t.CodPersona).HasMaxLength(20);
            builder.Property(t => t.CodReferencia).HasMaxLength(20);
            builder.Property(t => t.Valor).HasMaxLength(20);
        }
    }
}