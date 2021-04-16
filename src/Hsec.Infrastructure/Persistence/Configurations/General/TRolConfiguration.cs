using Hsec.Domain.Entities.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.General
{
   public class TRolConfiguration : IEntityTypeConfiguration<TRol>
    {
        public void Configure(EntityTypeBuilder<TRol> builder)
        {
            builder.HasKey(k => k.CodRol)
                .HasName("PK_TRol_CodRol");
                
            builder.Property(t => t.Descripcion)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
