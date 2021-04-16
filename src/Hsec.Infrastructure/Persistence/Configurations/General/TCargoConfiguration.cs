using Hsec.Domain.Entities.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.General
{
   public class TCargoConfiguration : IEntityTypeConfiguration<TCargo>
    {
        public void Configure(EntityTypeBuilder<TCargo> builder)
        {
            builder.HasKey(t => t.Ocupacion)
                .HasName("PK_TCargo_Ocupacion");
                
            builder.Property(t => t.Ocupacion)
                .HasMaxLength(35);
            builder.Property(t => t.CodWebControl)
                .HasMaxLength(100);
            builder.Property(t => t.Descripcion)
                .HasMaxLength(300);
        }
    }
}
