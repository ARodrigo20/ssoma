using Hsec.Domain.Entities.Capacitaciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Capacitaciones
{
    public class TemaCapacitacionConfiguration : IEntityTypeConfiguration<TTemaCapacitacion>
    {
        public void Configure(EntityTypeBuilder<TTemaCapacitacion> builder)
        {
            builder.HasKey(t => t.CodTemaCapacita);        //o => new { o.CodPersona, o.CodCurso }

            builder.HasMany(t => t.PlanTema)
               .WithOne(t => t.TemaCapacitacion)
               .HasForeignKey(t => t.CodTemaCapacita);

            builder.HasMany(t => t.TemaCapEspecifico)
               .WithOne(t => t.TemaCapacitacion)
                .HasForeignKey(t => t.CodTemaCapacita);

            builder.Property(f => f.CodTemaCapacita)
              .HasMaxLength(50)
              .ValueGeneratedNever();
        }
    }
}
