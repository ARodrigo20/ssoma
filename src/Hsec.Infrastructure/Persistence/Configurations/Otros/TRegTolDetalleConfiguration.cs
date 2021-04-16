using Hsec.Domain.Entities.Otros;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Otros
{
    public class TRegTolDetalleConfiguration : IEntityTypeConfiguration<TRegTolDetalle>
    {
        public void Configure(EntityTypeBuilder<TRegTolDetalle> builder)
        {
            builder.HasKey(t => new { t.CodTolCero, t.CodRegla }).HasName("PK_ToleranciaRegla_Id");
            builder.Property(t => t.CodRegla)
                .HasMaxLength(20);
            builder.HasOne(o => o.ToleranciaCero)
                .WithMany(m => m.ToleranciaReglas)
                .HasForeignKey(f => f.CodTolCero)
                .HasConstraintName("fk_in_TToleranciaCero_ToleranciaRegla");

        }
    }
}
