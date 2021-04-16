namespace Hsec.Application.VerificacionControlCritico.Queries.AvancePorCartilla
{
    public class AvenceDto
    {
        public string CodVerificacionControlCritico { get; set; }
        public string CodCartilla { get; set; }
        public string Anio { get; set; }
        public string CodMes { get; set; }
        public string CodigoPersona {get;set;}

        public AvenceDto(){}
        public AvenceDto(string codVerificacionControlCritico, string codCartilla, string anio, string codMes, string codigoPersona)
        {
            CodVerificacionControlCritico = codVerificacionControlCritico;
            CodCartilla = codCartilla;
            Anio = anio;
            CodMes = codMes;
            CodigoPersona = codigoPersona;
        }
    }
}