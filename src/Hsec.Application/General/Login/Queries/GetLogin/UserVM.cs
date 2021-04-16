using System.Collections.Generic;

namespace Hsec.Application.General.Login.Queries.GetLogin
{
    public class UserVM
    {
        public int? codUsuario { get; set; }
        public string usuario { get; set; }
        public string password { get; set; }
        public bool tipoLogueo { get; set; }
        public string token { get; set; }
        public string dominio { get; set; }
        public string codRol { get; set; }
        //data PErsona
        public string codPersona { get; set; }
        public int codTipoPersona { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string nroDocumento { get; set; }
        public string email { get; set; }
        public string cargo { get; set; }
        public string sexo { get; set; }
        public string rol { get; set; }
        public string empresa { get; set; }
       
        //public ICollection<TUsuarioRol> UsuarioRoles { get; set; }
        public ICollection<INavData> barMenu { get; set; }

    }
}
