using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.General
{
    public class TCriterio : AuditableEntity
    {
        public string CodCrit { get; set; }
        public string CodCC { get; set; }
        public string Criterio { get; set; }
        public TControlCritico ControlCritico { get; set; }
        
        //        public ICollection<TTipoDocPersona> TipoDocPersonas { get; set; }
        //  public ICollection<TUsuario> Usuarios { get; set; } 
    }
}
