using Hsec.Domain.Entities.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.General
{
    public class TUsuarioRolConfiguration : IEntityTypeConfiguration<TUsuarioRol>
    {
        public void Configure(EntityTypeBuilder<TUsuarioRol> builder)
        {
            builder.HasKey(k => new { k.CodRol, k.CodUsuario })
                 .HasName("PK_TUsuarioRol_CodUsuario_CodRol");
     
            builder.HasOne(o => o.Rol)
                .WithMany(m => m.UsuarioRoles) // lista de la tabla referida 
                .HasForeignKey(f => f.CodRol) // clave de la clase
                .HasConstraintName("FK_1n_Rol_UsuarioRol");
            builder.HasOne(o => o.Usuario)
               .WithMany(m => m.UsuarioRoles) // lista de la tabla referida 
               .HasForeignKey(f => f.CodUsuario) // clave de la clase
               .HasConstraintName("FK_1n_Usuario_UsuarioRol");
        }
    }
}
