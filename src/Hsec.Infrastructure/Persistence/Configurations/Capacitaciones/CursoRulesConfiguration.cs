using Hsec.Domain.Entities.Capacitaciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Capacitaciones
{
    public class CursoRulesConfiguration : IEntityTypeConfiguration<TCursoRules>
    {
        public void Configure(EntityTypeBuilder<TCursoRules> builder)
        {
            builder.HasKey(o => o.RecurrenceID);

            builder.HasMany(t => t.TCurso)
               .WithOne(t => t.TCursoRules)
               .HasForeignKey(t => t.RecurrenceID);

            builder.Property(f => f.RecurrenceID)
              .HasMaxLength(50)
              .ValueGeneratedNever();
        }
    }
}