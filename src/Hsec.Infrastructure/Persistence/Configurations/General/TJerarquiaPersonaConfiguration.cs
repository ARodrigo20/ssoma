

using Hsec.Domain.Entities.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Persistence.Configurations.General
{
    public class TJerarquiaPersonaConfiguration : IEntityTypeConfiguration<TJerarquiaPersona>
    {
        public void Configure(EntityTypeBuilder<TJerarquiaPersona> builder)
        {
            builder.HasKey(t => new { t.CodPosicion, t.CodPersona});
            //builder.HasNoKey();
            // Uno a muchos -> Persona a Jerarquia persona
            //builder.HasOne(t => t.Persona)
            //    .WithMany(t => t.JerarquiaPersona)
            //    .HasForeignKey(t => t.CodPersona);

            // Uno a muchos -> Jerarquias a Jerarquia persona
            builder.HasOne(t => t.Jerarquia)
                .WithMany(t => t.JerarquiaPersona)
                .HasForeignKey(t => t.CodPosicion);
        }
    }
}