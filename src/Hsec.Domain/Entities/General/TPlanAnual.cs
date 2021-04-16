namespace Hsec.Domain.Entities.General
{
    public class TPlanAnual
    {
        public string Anio { get; set; }
        public string CodMes { get; set; }
        public string CodPersona { get; set; }
        public string CodReferencia { get; set; }
        public string Valor { get; set; }

        public TPlanAnual setAnio(string Anio){
            this.Anio = Anio;
            return this;
        }
        public TPlanAnual setCodMes(string CodMes){
            this.CodMes = CodMes;
            return this;
        }
        public TPlanAnual setCodPersona(string CodPersona){
            this.CodPersona = CodPersona;
            return this;
        }
        public TPlanAnual setCodReferencia(string CodReferencia){
            this.CodReferencia = CodReferencia;
            return this;
        }
        public TPlanAnual setValor(string Valor){
            this.Valor = Valor;
            return this;
        }
    }
}