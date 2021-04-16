using Hsec.Domain.Entities.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Persistence.Configurations.General
{
    public class TModulosConfiguration : IEntityTypeConfiguration<TModulo>
    {
        public void Configure(EntityTypeBuilder<TModulo> builder)
        {
            builder.HasKey(t => t.CodModulo);
            //builder.HasNoKey();
            builder.HasOne(t => t.Padre)
                .WithMany(t => t.Hijos)
                .HasForeignKey(t => t.CodModuloPadre);
        }
    }
}