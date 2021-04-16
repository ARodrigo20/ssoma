using Hsec.Domain.Entities.Capacitaciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Capacitaciones
{       
    public class PlanTemaConfiguration : IEntityTypeConfiguration<TPlanTema>
    {
        public void Configure(EntityTypeBuilder<TPlanTema> builder)
        {
            builder.HasKey(o => new { o.CodTemaCapacita, o.CodReferencia });                 

            builder.HasOne(t => t.TemaCapacitacion)
                .WithMany(t => t.PlanTema)
                .HasForeignKey(k => k.CodTemaCapacita);

            builder.Property(f => f.CodTemaCapacita)
              .HasMaxLength(50)
              .ValueGeneratedNever();

            builder.Property(f => f.CodReferencia)
              .HasMaxLength(50)
              .ValueGeneratedNever();

            //builder.HasIndex(b => new { b.CodReferencia, b.CodTemaCapacita})
            //.IsUnique();
        }
    }    
}
