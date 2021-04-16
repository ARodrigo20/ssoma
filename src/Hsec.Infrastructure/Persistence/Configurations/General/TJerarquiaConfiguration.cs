using Hsec.Domain.Entities.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Persistence.Configurations.General
{
    public class TJerarquiaConfiguration : IEntityTypeConfiguration<TJerarquia>
    {
        public void Configure(EntityTypeBuilder<TJerarquia> builder)
        {
            builder.Property(t => t.CodPosicion).ValueGeneratedNever();
            builder.HasKey(t => t.CodPosicion);
            //builder.HasNoKey();
            builder.HasOne(t => t.Padre)
                .WithMany(t => t.Hijos)
                .HasForeignKey(t => t.CodPosicionPadre).OnDelete(DeleteBehavior.Restrict);
        }
    }
}