using Hsec.Domain.Entities.PlanAccion;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.PlanAccion
{
    public class TFileConfiguration
    {
        public void Configure(EntityTypeBuilder<TFile> builder)
        {
            builder.HasKey(i => i.CorrelativoArchivos);

        }
    }
}
