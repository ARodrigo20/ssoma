using Hsec.Domain.Entities.Otros;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Otros
{
    public class TReunionConfiguration : IEntityTypeConfiguration<TReunion>
    {
        public void Configure(EntityTypeBuilder<TReunion> builder)
        {
            builder.HasKey(k => k.CodReunion);
            builder.Property(t => t.CodReunion)
                .HasMaxLength(20);

        }
    }
}
