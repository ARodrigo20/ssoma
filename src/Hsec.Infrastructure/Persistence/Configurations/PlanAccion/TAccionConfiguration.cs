using Hsec.Domain.Entities.PlanAccion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Persistence.Configurations.PlanAccion
{
    public class TAccionConfiguration : IEntityTypeConfiguration<TAccion>
    {
        public void Configure(EntityTypeBuilder<TAccion> builder)
        {
            builder.HasKey(t => t.CodAccion);

            builder.HasMany(t => t.RespPlanAccion)
                .WithOne(t => t.Accion)
                .HasForeignKey(t => t.CodAccion);

            builder.HasMany(t => t.LevantamientoPlan)
                .WithOne(t => t.Accion)
                .HasForeignKey(t => t.CodAccion);

            //builder.HasOne(t => t.CodActiRelacionadaNavigation)
            //    .WithMany(t => t.TAccion)
            //    .HasForeignKey(t => t.CodActiRelacionada);

            //builder.HasOne(t => t.CodAreaHsecNavigation)
            //    .WithMany(t => t.TAccion)
            //    .HasForeignKey(t => t.CodAreaHsec);

            //builder.HasOne(t => t.CodEstadoAccionNavigation)
            //    .WithMany(t => t.TAccion)
            //    .HasForeignKey(t => t.CodEstadoAccion);

            //builder.HasOne(t => t.CodNivelRiesgoNavigation)
            //    .WithMany(t => t.TAccion)
            //    .HasForeignKey(t => t.CodNivelRiesgo);

            //builder.HasOne(t => t.CodTipoAccionNavigation)
            //    .WithMany(t => t.TAccion)
            //    .HasForeignKey(t => t.CodTipoAccion);


        }
    }
}
