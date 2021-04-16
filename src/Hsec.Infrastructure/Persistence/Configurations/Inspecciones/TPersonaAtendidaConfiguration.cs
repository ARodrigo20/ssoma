using Hsec.Domain.Entities.Inspecciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Inspecciones
{
    class TPersonaAtendidaConfiguration : IEntityTypeConfiguration<TPersonaAtendida>
    {
        public void Configure(EntityTypeBuilder<TPersonaAtendida> builder)
        {
            builder.HasKey(k => new { k.CodInspeccion, k.CodPersona })
                .HasName("PK_TInspeccion_PersonaAtendida");
            builder.HasOne(o => o.Inspeccion)
                .WithMany(m => m.PersonasAtendidas)
                .HasForeignKey(f => f.CodInspeccion)
                .HasConstraintName("FK_1n_Inspeccion_Persona");
        }
    }
}
