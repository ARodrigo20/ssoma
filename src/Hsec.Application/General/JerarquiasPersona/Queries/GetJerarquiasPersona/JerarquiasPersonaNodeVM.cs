using System;

namespace Hsec.Application.General.JerarquiasPersona.Queries.GetJerarquiaPersona
{
    public class JerarquiasPersonaNodeVM
    {
        public JerarquiasPersonaNodeVM(string CodPersona, string Nombre, string ApellidoPaterno, string ApellidoMaterno,
            DateTime? FecInicio, DateTime? FecFin, string Asignacion) {
            this.CodPersona = CodPersona;
            this.Nombre = Nombre;
            this.ApellidoPaterno = ApellidoPaterno;
            this.ApellidoMaterno = ApellidoMaterno;
            this.FecInicio = FecInicio;
            this.FecFin = FecFin;
            if (Asignacion == "1")
            {
                //this.Asignacion = JerarquiaPersonaEnum.Primaria.ToString();
                this.Asignacion = "0";
            }
            else
            {
                this.Asignacion = Asignacion;
            }
        }
        public string CodPersona { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public DateTime? FecInicio { get; set; }
        public DateTime? FecFin { get; set; }
        public string Asignacion { get; set; }
      
    }
}
