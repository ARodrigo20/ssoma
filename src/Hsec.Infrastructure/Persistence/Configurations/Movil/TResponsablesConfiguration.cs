using Hsec.Domain.Entities.Movil;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Movil
{
    public class TResponsablesConfiguration : IEntityTypeConfiguration<TResponsables>
    {
        public void Configure(EntityTypeBuilder<TResponsables> builder)
        {
            builder.HasKey(t => t.Correlativo);
            builder.Property(t => t.Correlativo).UseIdentityColumn();


            builder.Property(t => t.CodPersona)
                .HasMaxLength(20)
                .IsRequired(true);

            builder.Property(t => t.CodTipo)
                .HasMaxLength(2)
                .IsRequired(true);
            builder.Property(t => t.UsuCreacion)
                .HasMaxLength(20)
                .IsRequired(false);
            builder.Property(t => t.UsuModifica)
                .HasMaxLength(20)
                .IsRequired(false);
            builder.Property(t => t.Estado)
                .HasMaxLength(2)
                .IsRequired(false);
        }
    }
}