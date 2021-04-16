using Hsec.Domain.Entities.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.General
{
    public class TAnalisisCausaConfiguration : IEntityTypeConfiguration<TAnalisisCausa>
    {
        public void Configure(EntityTypeBuilder<TAnalisisCausa> builder)
        {
            builder.HasKey(ac => ac.CodAnalisis);
            builder.Property(t => t.CodAnalisis).HasMaxLength(10);
            builder.Property(t => t.CodPadre).HasMaxLength(10);
            builder.Property(t => t.CodAnterior).HasMaxLength(50);
            builder.HasOne(t => t.Padre)
               .WithMany(t => t.Hijos)
               .HasForeignKey(t => t.CodPadre).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
