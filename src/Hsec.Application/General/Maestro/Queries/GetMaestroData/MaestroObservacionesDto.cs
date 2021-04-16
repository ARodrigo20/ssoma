using System.Collections.Generic;

namespace Hsec.Application.General.Maestro.Queries.GetMaestroData
{
    public class MaestroObservacionesDto
    {
        public IList<CampoDto> AreaHSEC { get; set; }
        public IList<CampoDto> NivelRiesgo { get; set; }
        public IList<CampoDto> Correcion { get; set; }
        public IList<CampoDto> ActividadRelacionada { get; set; }
        public IList<CampoDto> HHARelacionada { get; set; }
        public IList<CampoDto> ActoSubEstandar { get; set; }
        public IList<CampoDto> Estado { get; set; }
        public IList<CampoDto> Error { get; set; }
        public IList<CampoDto> CondicionSubEstandar { get; set; }
        public IList<CampoDto> StopWork { get; set; }
        public IList<CampoDto> Empresa { get; set; }
    }
}
