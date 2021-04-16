using Hsec.Domain.Entities.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.General
{
    public class TRolAccesoConfiguration : IEntityTypeConfiguration<TRolAcceso>
    {
        public void Configure(EntityTypeBuilder<TRolAcceso> builder)
        {
            builder.HasKey(k=>new { k.CodRol, k.CodAcceso })
                .HasName("PK_TRolAcesso_CodRol");
            builder.HasOne(o => o.Acceso)
                .WithMany(m => m.RolAccesos)
                .HasForeignKey(f => f.CodAcceso)
                .HasConstraintName("FK_1n_Acceso_RolAcceso");
            builder.HasOne(o => o.Rol)
                .WithMany(m => m.RolAccesos)
                .HasForeignKey(f => f.CodRol)
                .HasConstraintName("FK_1n_Rol_RolAcceso");
        }
    }
}
