using Hsec.Domain.Common;
using Hsec.Domain.Entities.Auditoria;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Hsec.Infrastructure.Persistence.Configurations.Auditoria
{
    public class TAnalisisHallazgoConfiguration : IEntityTypeConfiguration<TAnalisisHallazgo>
    {

        public void Configure(EntityTypeBuilder<TAnalisisHallazgo> builder)
        {
            builder.HasKey(t => new { t.Correlativo, t.CodHallazgo });

            
            builder.Property(t => t.CodHallazgo).HasMaxLength(20);
            builder.Property(t => t.CodTipoHallazgo).HasMaxLength(20);
            builder.Property(t => t.CodTipoNoConfor).HasMaxLength(20);
            builder.Property(t => t.DescripcionNoConf).HasMaxLength(8000);
            builder.Property(t => t.CodRespAccInmediata).HasMaxLength(20);
            builder.Property(t => t.DescripcionAcc).HasMaxLength(8000);
            builder.Property(t => t.CodRespVeriSegui).HasMaxLength(20);
            builder.Property(t => t.CodAceptada).HasMaxLength(20);
            builder.Property(t => t.DescripcionVerSegui).HasMaxLength(8000);
            builder.Property(t => t.CodRespConEfec).HasMaxLength(20);
            builder.Property(t => t.CodEfectividadAc).HasMaxLength(20);
            builder.Property(t => t.DescripcionConEfec).HasMaxLength(8000);
            builder.Property(t => t.CodRespCierNoConfor).HasMaxLength(20);
            builder.Property(t => t.DescripcionCierNoConfor).HasMaxLength(8000);

            // builder
            //     .HasOne(t => t.CodIncidenteNavigation)
            //     .WithMany(t => t.TafectadoComunidad)
            //     .HasForeignKey(t => t.CodIncidente);
        }
        //public virtual TMotivo CodMotivoNavigation { get; set; }
    }
}
