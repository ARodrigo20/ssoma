using Hsec.Domain.Entities.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.General
{
    public class TAprobacionPlanConfiguration : IEntityTypeConfiguration<TAprobacionPlan>
    {
        public void Configure(EntityTypeBuilder<TAprobacionPlan> builder)
        {
            builder.HasKey(t => t.CodAprobacion);

            builder.Property(f => f.CodAprobacion)
            .ValueGeneratedOnAdd();

            builder.Property(t => t.DocReferencia)
                .HasMaxLength(50);
            builder.Property(t => t.EstadoDoc)
                .HasMaxLength(1);
            builder.Property(t => t.CodTabla)
                .HasMaxLength(10);
        }
    }
}