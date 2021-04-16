namespace Hsec.Application.General.Login.Commands.ChangePassword
{
    public class UserPassVM
    {
        public int codUsuario { get; set; }
        public string usuario { get; set; }
        public string password { get; set; }
        public string newPassword { get; set; }
        public int? tipoPersona { get; set; }
    }
}
