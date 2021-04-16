namespace Hsec.Domain.Entities.General
{
    public class TPlanAnualGeneral
    {
        public string Anio { get; set; }
        public string CodMes { get; set; }
        public string CodPersona { get; set; }
        public string CodReferencia { get; set; }
        public string Valor { get; set; }

        public TPlanAnualGeneral setAnio(string Anio){
            this.Anio = Anio;
            return this;
        }
        public TPlanAnualGeneral setCodMes(string CodMes){
            this.CodMes = CodMes;
            return this;
        }
        public TPlanAnualGeneral setCodPersona(string CodPersona){
            this.CodPersona = CodPersona;
            return this;
        }
        public TPlanAnualGeneral setCodReferencia(string CodReferencia){
            this.CodReferencia = CodReferencia;
            return this;
        }
        public TPlanAnualGeneral setValor(string Valor){
            this.Valor = Valor;
            return this;
        }
    }
}