using Hsec.Domain.Entities.Otros;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Otros
{
    public class TListaParticipantesComiteConfiguration : IEntityTypeConfiguration<TListaParticipantesComite>
    {
        public void Configure(EntityTypeBuilder<TListaParticipantesComite> builder)
        {
            builder.HasKey(t => new { t.CodComite, t.CodPersona }).HasName("PK_TListaParticipantesComite_Id");
            builder.Property(t => t.CodPersona)
                .HasMaxLength(20);
            builder.HasOne(o => o.Comite)
                .WithMany(m => m.ListaParticipantes)
                .HasForeignKey(f => f.CodComite)
                .HasConstraintName("fk_in_TComite_TListaParticipantesComite");

        }
    }
}
