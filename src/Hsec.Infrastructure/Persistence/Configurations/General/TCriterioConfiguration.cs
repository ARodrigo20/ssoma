using Hsec.Domain.Entities.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.General
{
    public class TCriterioConfiguration : IEntityTypeConfiguration<TCriterio>
    {
        public void Configure(EntityTypeBuilder<TCriterio> builder)
        {
            builder.HasKey(k => new { k.CodCC, k.CodCrit });
            builder.Property(t => t.CodCrit)
               .HasMaxLength(50)
               .IsRequired(true);
            builder.Property(t => t.CodCC)
                .HasMaxLength(50)
                .IsRequired(true);
            builder.Property(t => t.Criterio)
               .HasMaxLength(200)
               .IsRequired(false);

            builder.HasOne(o => o.ControlCritico)
               .WithMany(m => m.Criterios)
               .HasForeignKey(f => f.CodCC);
            //.IsRequired(false)
            //.OnDelete(DeleteBehavior.SetNull);
            //builder.Property(t => t.Modulo)
            //    .HasMaxLength(2)
            //    .IsRequired(false);
        }
    }   
}
