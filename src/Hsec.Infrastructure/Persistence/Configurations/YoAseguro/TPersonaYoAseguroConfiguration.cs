using Hsec.Domain.Entities.YoAseguro;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.YoAseguro
{
    public class TPersonaYoAseguroConfiguration : IEntityTypeConfiguration<TPersonaYoAseguro>
    {
        public void Configure(EntityTypeBuilder<TPersonaYoAseguro> builder)
        {
            builder.HasKey(k => new { k.CodYoAseguro, k.CodPersona })
                .HasName("PK_TYoAseguro_CodPersona");
            builder.HasOne(o => o.YoAseguro)
                .WithMany(m => m.PersonasReconocidas)
                .HasForeignKey(f => f.CodYoAseguro)
                .HasConstraintName("FK_1n_YoAseguro_Persona");
        }
    }
}
