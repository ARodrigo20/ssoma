using Hsec.Domain.Entities.PlanAccion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.PlanAccion
{
    public class TLevantamientoTareaConfiguration : IEntityTypeConfiguration<TLevantamientoPlan>
    {

        public void Configure(EntityTypeBuilder<TLevantamientoPlan> builder)
        {
            builder.HasKey(o => new { o.CodAccion, o.CodPersona, o.Correlativo });

            //builder.HasKey(t => t.CodAccion);
            builder.HasOne(t => t.Accion)
                .WithMany(t => t.LevantamientoPlan)
                .HasForeignKey(t => t.CodAccion);
            builder.Property(f => f.Correlativo)
            .ValueGeneratedOnAdd();
            builder.Property(f => f.Rechazado)
                .HasDefaultValue(false);
        }
    }
}
