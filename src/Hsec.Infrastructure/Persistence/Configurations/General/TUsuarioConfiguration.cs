using Hsec.Domain.Entities.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.General
{
    public class TUsuarioConfiguration : IEntityTypeConfiguration<TUsuario>
    {
        public void Configure(EntityTypeBuilder<TUsuario> builder)
        {
            builder.HasKey(k => k.CodUsuario)
                .HasName("PK_TUsuario_CodUsuario");

            builder.Property(t => t.Usuario)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(t => t.Password)
                .HasMaxLength(50)
                .IsRequired(false);
            //builder.Property(t => t.CodPersona)
            //   .HasMaxLength(50)
            //   .IsRequired();
            builder.Property(t => t.Token)
              .HasMaxLength(200)
              .IsRequired(false);
            builder.Property(t => t.TipoLogueo)             
             .IsRequired();
            //builder.HasOne(o => o.Persona)
            //  .WithMany(m => m.Usuarios) // lista de la tabla referida 
            //  .HasForeignKey(f => f.CodPersona) // clave de la clase
            //  .HasConstraintName("FK_1n_Persona_Usuarios");
        }
    }
}
