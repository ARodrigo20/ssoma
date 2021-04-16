using Hsec.Domain.Entities.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.General
{
    public class TGestionRiesgoConfiguration : IEntityTypeConfiguration<TGestionRiesgo>
    {
        public void Configure(EntityTypeBuilder<TGestionRiesgo> builder)
        {
            builder.HasKey(t => t.CodGestionRiesgo);
            //builder.HasNoKey();
            builder.HasOne(t => t.Padre)
                .WithMany(t => t.Hijos)
                .HasForeignKey(t => t.CodGestionRiesgoPadre);
        }
    }
}