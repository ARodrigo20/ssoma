using Hsec.Domain.Entities.Movil;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Movil
{
    public class TFeedbackConfiguration : IEntityTypeConfiguration<TFeedback>
    {
        public void Configure(EntityTypeBuilder<TFeedback> builder)
        {
            builder.HasKey(t => t.Correlativo);
            builder.Property(t => t.Correlativo).UseIdentityColumn();
            builder.Property(t => t.CodUsuario)
                .HasMaxLength(20)
                .IsRequired(false);
            builder.Property(t => t.Asunto)
                .HasMaxLength(100)
                .IsRequired(false);
            builder.Property(t => t.Mensaje)
                .HasColumnType("nvarchar(MAX)")
                .IsRequired(false);
        }
    }
}