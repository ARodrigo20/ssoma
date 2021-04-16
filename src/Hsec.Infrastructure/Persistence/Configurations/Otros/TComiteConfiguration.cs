using Hsec.Domain.Entities.Otros;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Otros
{
    class TComiteConfiguration : IEntityTypeConfiguration<TComite>
    {
        public void Configure(EntityTypeBuilder<TComite> builder)
        {
            builder.HasKey(k => k.CodComite);
            builder.Property(t => t.CodComite)
                .HasMaxLength(20);

        }
    }
}
