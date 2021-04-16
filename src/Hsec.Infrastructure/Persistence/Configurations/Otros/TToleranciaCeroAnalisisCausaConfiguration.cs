using Hsec.Domain.Entities.Otros;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace Hsec.Infrastructure.Persistence.Configurations.Otros
{
    public class TToleranciaCeroAnalisisCausaConfiguration : IEntityTypeConfiguration<TToleranciaCeroAnalisisCausa>
    {
        public void Configure(EntityTypeBuilder<TToleranciaCeroAnalisisCausa> builder)
        {
            builder.HasKey(t => new { t.CodTolCero, t.CodAnalisis }).HasName("PK_TToleranciaCeroAnalisisCausa_Id");
            builder.Property(t => t.CodAnalisis)
                .HasMaxLength(20);
            builder.HasOne(o => o.ToleranciaCero)
                .WithMany(m => m.ToleranciaAnalisisCausa)
                .HasForeignKey(f => f.CodTolCero)
                .HasConstraintName("fk_in_TToleranciaCero_TToleranciaCeroAnalisisCausa");

        }
    }
}
