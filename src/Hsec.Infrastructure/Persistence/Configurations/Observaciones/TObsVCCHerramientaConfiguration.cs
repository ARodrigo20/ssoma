using Hsec.Domain.Entities.Observaciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Observaciones
{
    public class TObsVCCHerramientaConfiguration : IEntityTypeConfiguration<TObsVCCHerramienta>
    {
        public void Configure(EntityTypeBuilder<TObsVCCHerramienta> builder)
        {
            builder.HasKey(t => t.CodHerram);

            builder.Property(t => t.CodHerram)
            .HasMaxLength(50);
            builder.Property(t => t.CodVcc)
            .HasMaxLength(50);
            builder.Property(t => t.CodDesHe)
            .HasMaxLength(50);
        }
    }
}