namespace Hsec.Application.General.PlanAnualVerifiacionCC.Queries.GetCartillasPorPersonFiltro
{
    public class FiltroVerCCPorPersonasFiltro
    {
        public string CodReferencia {get;set;}
        public string CodPersona { get; set; }
        public int Gerencia { get; set; }
        // public string Superintendencia { get; set; }w
        public string Anio { get; set; }
        public string CodMes { get; set; }
        public int Pagina { get; set; }
        public int PaginaTamanio { get; set; }
    }
}