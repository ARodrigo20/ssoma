using Hsec.Domain.Entities.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.General
{
    public class TUbicacionConfiguration : IEntityTypeConfiguration<TUbicacion>
    {
        public void Configure(EntityTypeBuilder<TUbicacion> builder)
        {
            builder.HasKey(t => t.CodUbicacion);
            //builder.HasNoKey();
            builder.HasOne(t => t.Padre)
                .WithMany(t => t.Hijos)
                .HasForeignKey(t => t.CodUbicacionPadre);
        }
    }
}