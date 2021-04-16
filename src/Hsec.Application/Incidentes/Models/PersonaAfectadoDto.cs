using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Incidentes;
using System.Collections.Generic;

namespace Hsec.Application.Incidentes.Models
{
    public class PersonaAfectadoDto : IMapFrom<TInvestigaAfectado>
    {
        public int Correlativo { get; set; }
        public string DocAfectado { get; set; }
        public string Empresa { get; set; }
        public string Cargo { get; set; }
        public string Sexo { get; set; }
        public string FechaIngreso { get; set; }
        public string CodRegimen { get; set; }
        public string DiasDeTrabajo { get; set; }
        public string CodSisTrabajo { get; set; }
        public string PorcenDiasTrabajados { get; set; }
        public string HorasLaboradas { get; set; }
        public string CodGuardia { get; set; }
        public string CodExperiencia { get; set; }
        public string DesExperiencia { get; set; }
        public string CodTipoPersona { get; set; }
        public string CodZonasDeLesion { get; set; }
        public string ZonasDeLesion { get; set; }
        public string CodMecLesion { get; set; }
        public string CodNatLesion { get; set; }
        public string CodClasificaInci { get; set; }
        public string NroAtencionMedia { get; set; }
        public IList<MenAfectadoPersonaDto> Men { get; set; }
        //public IList<AtencionMedicaAfectadoPersonaDto> AtencionMedica { get; set; }
        //public IList<ManifestacionDto> Manifestacion { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PersonaAfectadoDto, TInvestigaAfectado>();
            profile.CreateMap<TInvestigaAfectado, PersonaAfectadoDto>();
        }
    }
}