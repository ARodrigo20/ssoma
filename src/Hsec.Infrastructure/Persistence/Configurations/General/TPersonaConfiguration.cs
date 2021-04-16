using Hsec.Domain.Entities.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.General
{
    public class TPersonaConfiguration : IEntityTypeConfiguration<TPersona>
    {
        public void Configure(EntityTypeBuilder<TPersona> builder)
        {
            builder.HasKey(t => t.CodPersona);
            builder.ToTable("TPersona");
            builder.Property(t => t.CodPersona)
              .HasMaxLength(50)
              .IsRequired();
            builder
                .HasOne<TProveedor>(p => p.Proveedor)
                .WithMany(p => p.Personas)
                .HasForeignKey(p => p.CodProveedor);
            //builder.HasOne<Pais>(s => s.CodPais);

            builder
                .HasOne<TPais>(p => p.Pais)
                .WithMany(p => p.Personas)
                .HasForeignKey(p => p.CodPais);
        }
    }
}
