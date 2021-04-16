using Hsec.Domain.Entities.Otros;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace Hsec.Infrastructure.Persistence.Configurations.Otros
{
    public class TAusentesReunionConfiguration : IEntityTypeConfiguration<TAusentesReunion>
    {
        public void Configure(EntityTypeBuilder<TAusentesReunion> builder)
        {
            builder.HasKey(t => new { t.CodReunion, t.CodPersona }).HasName("PK_TAusentesReunion_Id");
            builder.Property(t => t.CodPersona)
                .HasMaxLength(20);
            builder.HasOne(o => o.Reunion)
                .WithMany(m => m.ReunionAusentes)
                .HasForeignKey(f => f.CodReunion)
                .HasConstraintName("fk_in_TReunion_TAusentesReunion");

        }
    }
}
