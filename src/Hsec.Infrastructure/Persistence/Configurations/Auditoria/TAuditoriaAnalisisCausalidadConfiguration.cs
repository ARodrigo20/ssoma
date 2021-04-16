using Hsec.Domain.Common;
using Hsec.Domain.Entities.Auditoria;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;

namespace Hsec.Infrastructure.Persistence.Configurations.Auditoria
{
    public class TAuditoriaAnalisisCausalidadConfiguration : IEntityTypeConfiguration<TAuditoriaAnalisisCausalidad>
    {

        public void Configure(EntityTypeBuilder<TAuditoriaAnalisisCausalidad> builder)
        {
            builder.HasKey(t => new { t.CodAnalisis, t.CodHallazgo });

            builder.Property(t => t.CodAnalisis).HasMaxLength(20);
            builder.Property(t => t.CodHallazgo).HasMaxLength(20);
            builder.Property(t => t.Comentario).HasMaxLength(600);
            builder.Property(t => t.CodCondicion).HasMaxLength(20);

            // builder
            //     .HasOne(t => t.CodIncidenteNavigation)
            //     .WithMany(t => t.TdiasPerdidosAfectado)
            //     .HasForeignKey(t => t.CodIncidente);
        }
    }

    
}
