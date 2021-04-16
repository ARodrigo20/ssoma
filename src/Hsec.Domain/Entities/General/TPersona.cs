using Hsec.Domain.Common;
using System;

namespace Hsec.Domain.Entities.General
{
    public class TPersona : AuditableEntity
    {
        //public TipoPersona CodTipoPersona { get; set; } 
        public string CodTipoPersona { get; set; }
        public string CodPersona { get; set; }
        public string CodProveedor { get; set; } 
        public string CodPais { get; set; } 
        public string CodRrhh { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Direccion1 { get; set; }
        public string Direccion2 { get; set; }
        public string Email { get; set; }
        public string GrupoSanguineo { get; set; }
        public string Ocupacion { get; set; }
        public string Profesion { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        //public EstadoCivil EstadoCivil { get; set; }
        public string EstadoCivil { get; set; }
        //public Sexo Sexo { get; set; }
        public string Sexo { get; set; }
        public decimal? Sueldo { get; set; }
        public DateTime? FechaCese { get; set; }
        public string ObsPersona { get; set; }
        public string Turno { get; set; }
        public string Guardia { get; set; }
        public string Empresa { get; set; }
        public string CodCargo { get; set; }
        public string FlagPersistente { get; set; }
        public string NroDocumento { get; set; }
        //public TipoDocumento CodTipDocIden { get; set; }
        public string CodTipDocIden { get; set; }
        //public ICollection<TJerarquiaPersona> JerarquiaPersona { get; set; }
        public TProveedor Proveedor { get; set; }
        public TPais Pais { get; set; }
//        public ICollection<TTipoDocPersona> TipoDocPersonas { get; set; }
      //  public ICollection<TUsuario> Usuarios { get; set; } 
    }
}
