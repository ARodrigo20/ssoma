namespace Hsec.Application.General.Jerarquias.Queries.GetJerarquiasPersonaAll
{
    public class PersonaPosVM
    {
        public PersonaPosVM(int CodPosicion, string Path, string CodPersona, string Nombre, string ApellidoPaterno, string ApellidoMaterno)
        {
            this.CodPersona = CodPersona;
            this.CodPosicion = CodPosicion;
            this.Path = Path;
            this.Nombre = Nombre;
            this.ApellidoPaterno = ApellidoPaterno;
            this.ApellidoMaterno = ApellidoMaterno;
        }
        public string CodPersona { get; set; }
        public int CodPosicion { get; set; }
        public string Path { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
    }
}
    
