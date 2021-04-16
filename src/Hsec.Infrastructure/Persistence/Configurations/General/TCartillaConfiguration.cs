using Hsec.Domain.Entities.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.General
{
    public class TCartillaConfiguration : IEntityTypeConfiguration<TCartilla>
    {
        public void Configure(EntityTypeBuilder<TCartilla> builder)
        {
            builder.HasKey(c => c.CodCartilla);
            builder.Property(t => t.CodCartilla)
               .HasMaxLength(50)
               .IsRequired(true);
            builder.Property(t => t.DesCartilla)
               .HasMaxLength(350)
               .IsRequired(false);
            builder.Property(t => t.TipoCartilla)
               .HasMaxLength(40)
               .IsRequired(true);
            builder.Property(t => t.PeligroFatal)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(t => t.Modulo)
                .HasMaxLength(2)
                .IsRequired(false);

        }
    }   
}
