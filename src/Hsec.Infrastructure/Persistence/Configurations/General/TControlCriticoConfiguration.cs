using Hsec.Domain.Entities.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.General
{
    public class TControlCriticoConfiguration : IEntityTypeConfiguration<TControlCritico>
    {
        public void Configure(EntityTypeBuilder<TControlCritico> builder)
        {
            builder.HasKey(k => k.CodCC);
            builder.Property(t => t.CodCC)
               .HasMaxLength(50)
               .IsRequired(true);
            builder.Property(t => t.CodRiesgo)
               .HasMaxLength(40)
               .IsRequired(false);
            builder.Property(t => t.DesCC)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(t => t.TipoCC)
               .HasMaxLength(50)
               .IsRequired(false);
            builder.Property(t => t.PeligroFatal)
               .HasMaxLength(50)
               .IsRequired(false);
            builder.Property(t => t.Modulo)
                .HasMaxLength(2)
                .IsRequired(false);

        }
    }   
}
