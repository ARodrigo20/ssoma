using Hsec.Domain.Entities.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.General
{
    public class TProveedorConfiguration : IEntityTypeConfiguration<TProveedor>
    {
        public void Configure(EntityTypeBuilder<TProveedor> builder)
        {
            builder.HasKey(p => p.CodProveedor);
            
        }
    }
}
