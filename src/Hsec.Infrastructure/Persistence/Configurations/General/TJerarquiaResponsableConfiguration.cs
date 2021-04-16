using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Hsec.Domain.Entities.General;

namespace Hsec.Infrastructure.Persistence.Configurations.General
{
    public class TJerarquiaResponsableConfiguration : IEntityTypeConfiguration<TJerarquiaResponsable>
    {
        public void Configure(EntityTypeBuilder<TJerarquiaResponsable> builder)
        {
            builder.HasKey(k => new { k.CodPosicion, k.CodPersona })
                 .HasName("PK_TJerarquiaResponsable_CodPosicion_CodPersona");

            builder.Property(f => f.CodPersona)
             .HasMaxLength(50);
        }
    }
}
