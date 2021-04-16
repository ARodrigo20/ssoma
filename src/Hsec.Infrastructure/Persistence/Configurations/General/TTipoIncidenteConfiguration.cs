using Hsec.Domain.Entities.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.General
{
    public class TTipoIncidenteConfiguration : IEntityTypeConfiguration<TTipoIncidente>
    {
        public void Configure(EntityTypeBuilder<TTipoIncidente> builder)
        {
            builder.HasKey(t => t.CodTipoIncidente);

            builder.HasOne(t => t.Padre)
                .WithMany(t => t.Hijos)
                .HasForeignKey(t => t.CodPadreTipoIncidente);
        }
    }
}
