using Hsec.Domain.Entities.Otros;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Otros
{
    class TAgendaConfiguration : IEntityTypeConfiguration<TAgenda>
    {
        public void Configure(EntityTypeBuilder<TAgenda> builder)
        {
            builder.HasKey(t => new { t.CodReunion, t.Correlativo }).HasName("PK_TAgenda_Id");
            builder.Property(t => t.Correlativo)
                .HasMaxLength(20);
            builder.HasOne(o => o.Reunion)
                .WithMany(m => m.ReunionAgendas)
                .HasForeignKey(f => f.CodReunion)
                .HasConstraintName("fk_in_TReunion_TAgenda");
        }
    }
}
