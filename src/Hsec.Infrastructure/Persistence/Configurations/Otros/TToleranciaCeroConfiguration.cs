using Hsec.Domain.Entities.Otros;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Otros
{
    public class ToleranciaCeroConfiguration : IEntityTypeConfiguration<TToleranciaCero>
    {
        public void Configure(EntityTypeBuilder<TToleranciaCero> builder)
        {
            builder.HasKey(k => k.CodTolCero);
            builder.Property(t => t.CodTolCero)
                .HasMaxLength(11);

        }
    }
}
