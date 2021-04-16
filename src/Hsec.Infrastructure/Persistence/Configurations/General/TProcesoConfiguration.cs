using Hsec.Domain.Entities.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.General
{
    public class TProcesoConfiguration : IEntityTypeConfiguration<TProceso>
    {
        public void Configure(EntityTypeBuilder<TProceso> builder)
        {
            builder.HasKey(t => t.CodProceso);
            builder.Property(t => t.CodProceso)
                .HasMaxLength(20);
            builder.Property(t => t.CadenaAprobacion)
                .HasMaxLength(1000);
            builder.Property(t => t.Descripcion)
                .HasMaxLength(1000);
            

        }
    }
}
