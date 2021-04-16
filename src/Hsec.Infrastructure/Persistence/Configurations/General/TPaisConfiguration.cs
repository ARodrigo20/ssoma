using Hsec.Domain.Entities.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.General
{
    public class TPaisConfiguration : IEntityTypeConfiguration<TPais>
    {
        public void Configure(EntityTypeBuilder<TPais> builder)
        {
            builder.HasKey(t => t.CodPais);
        }
    }
}
