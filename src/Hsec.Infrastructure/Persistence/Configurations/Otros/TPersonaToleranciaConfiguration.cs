using Hsec.Domain.Entities.Otros;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace Hsec.Infrastructure.Persistence.Configurations.Otros
{
    public class TPersonaToleranciaConfiguration : IEntityTypeConfiguration<TPersonaTolerancia>
    {
        public void Configure(EntityTypeBuilder<TPersonaTolerancia> builder)
        {
            builder.HasKey(t => new { t.CodTolCero, t.CodPersona }).HasName("PK_TPersonaTolerancia_Id");
            builder.Property(t => t.CodPersona)
                .HasMaxLength(20);
            builder.HasOne(o => o.ToleranciaCero)
                .WithMany(m => m.ToleranciaPersonas)
                .HasForeignKey(f => f.CodTolCero)
                .HasConstraintName("fk_in_TToleranciaCero_TPersonaTolerancia");

        }
    }
}
