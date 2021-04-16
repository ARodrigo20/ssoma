using System;
using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.Auditoria
{
    public class TAnalisisHallazgo : AuditableEntity
    {
        public int Correlativo { get; set; }
        public string CodHallazgo { get; set; }
        public string CodTipoHallazgo { get; set; }
        public string CodTipoNoConfor { get; set; }
        public string DescripcionNoConf { get; set; }
        public string CodRespAccInmediata { get; set; }
        public DateTime? FecAccInmediata { get; set; }
        public string DescripcionAcc { get; set; }
        public string CodRespVeriSegui { get; set; }
        public string CodAceptada { get; set; }
        public DateTime? FecVeriSegui { get; set; }
        public string DescripcionVerSegui { get; set; }
        public string CodRespConEfec { get; set; }
        public string CodEfectividadAc { get; set; }
        public DateTime? FecConEfec { get; set; }
        public string DescripcionConEfec { get; set; }
        public string CodRespCierNoConfor { get; set; }
        public DateTime? FecCierNoConfor { get; set; }
        public string DescripcionCierNoConfor { get; set; }
    }
}