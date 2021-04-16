using Hsec.Domain.Entities.PlanAccion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.PlanAccion
{
    public class TResponsableConfiguration : IEntityTypeConfiguration<TResponsable>
    {
        public void Configure(EntityTypeBuilder<TResponsable> builder)
        {
            builder.HasKey(o => new { o.CodAccion, o.CodPersona });
            //builder.HasKey(t => t.CodAccion);
            builder.HasOne(t => t.Accion)
                .WithMany(t => t.RespPlanAccion)
                .HasForeignKey(t => t.CodAccion);
        }
    }
}
