using Hsec.Domain.Entities.Capacitaciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Capacitaciones
{
    public class AlternativasConfiguration : IEntityTypeConfiguration<TAlternativas>
    {
        public void Configure(EntityTypeBuilder<TAlternativas> builder)
        {    
            builder.HasKey(o => o.CodAlternativa);

            builder.HasOne(t => t.Preguntas)
               .WithMany(t => t.Alternativas).HasForeignKey(x => x.CodPregunta);
         
            builder.Property(f => f.CodPregunta)
              .HasMaxLength(50);

            builder.Property(f => f.CodAlternativa)
             .HasMaxLength(50).ValueGeneratedOnAdd();
        }
    }
}