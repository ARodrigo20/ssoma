using Hsec.Domain.Enums;

namespace Hsec.Application.Incidentes.Models
{
    public class DetalleAfectadoDto
    {
        public TipoAfectado tipoAfectado { get; set; }
        public PersonaAfectadoDto Persona {get;set;}
        public PropiedadAfectadoDto Propiedad {get;set;}
        public MedioAmbienteAfectadoDto MedioAmbiente { get;set;}
        public ComunidadAfectadoDto Comunidad {get;set;}

    }
}