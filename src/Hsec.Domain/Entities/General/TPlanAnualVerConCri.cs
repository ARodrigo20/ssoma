namespace Hsec.Domain.Entities.General
{
    public class TPlanAnualVerConCri
    {
        public string Anio { get; set; }
        public string CodMes { get; set; }
        public string CodPersona { get; set; }
        public string CodReferencia { get; set; }
        public string Valor { get; set; }

        public TPlanAnualVerConCri setAnio(string Anio){
            this.Anio = Anio;
            return this;
        }
        public TPlanAnualVerConCri setCodMes(string CodMes){
            this.CodMes = CodMes;
            return this;
        }
        public TPlanAnualVerConCri setCodPersona(string CodPersona){
            this.CodPersona = CodPersona;
            return this;
        }
        public TPlanAnualVerConCri setCodReferencia(string CodReferencia){
            this.CodReferencia = CodReferencia;
            return this;
        }
        public TPlanAnualVerConCri setValor(string Valor){
            this.Valor = Valor;
            return this;
        }
    }
}