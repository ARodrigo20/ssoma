using Hsec.Domain.Entities.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.General
{
    public class TAccesoConfiguration : IEntityTypeConfiguration<TAcceso>
    {
        public void Configure(EntityTypeBuilder<TAcceso> builder)
        {
            builder.HasKey(k => k.CodAcceso)
                .HasName("PK_TAcceso_CodAcceso");
            builder.Property(t => t.Icono)
               .HasMaxLength(40)
               .IsRequired(false);
            builder.Property(t => t.Nombre)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(t => t.Descripcion)
               .HasMaxLength(200)
               .IsRequired(false);
            builder.Property(t => t.Componente)
               .HasMaxLength(100)
               .IsRequired();
               //.HasAnnotation("ColumnOrder",56);
            //builder.Property(t => t.CodPadre)
            //    .IsRequired(false);
            builder.HasOne(o => o.Padre)
               .WithMany(m => m.Hijos)
               .HasForeignKey(f => f.CodPadre)
               .IsRequired(false)
               .HasConstraintName("FK_1n_Acceso_Acceso");
               //.OnDelete(DeleteBehavior.SetNull);                                   
        }
    }   
}
