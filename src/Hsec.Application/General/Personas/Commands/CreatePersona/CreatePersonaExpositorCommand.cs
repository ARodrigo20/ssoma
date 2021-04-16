using Hsec.Application.Common.Interfaces;
using Hsec.Domain.Entities.General;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Hsec.Application.General.Personas.Commands.CreatePersona
{
    public class CreatePersonaExpositorCommand : IRequest<string>
    {
        //public Sexo Sexo { get; set; }
        public string Sexo { get; set; }
        public string NroDocumento { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Ocupacion { get; set; }
        public string Empresa { get; set; }

        public class CreatePersonaExpositorCommandHandler : IRequestHandler<CreatePersonaExpositorCommand, string>
        {
            private readonly IApplicationDbContext _context;
            public IConfiguration _configuration { get; }

            public CreatePersonaExpositorCommandHandler(IApplicationDbContext context, IConfiguration configuration)
            {
                _context = context;
                _configuration = configuration;
            }

            public async Task<string> Handle(CreatePersonaExpositorCommand request, CancellationToken cancellationToken)
            {
               // try {
                    string codPersona = _context.TPersona.Where(p => p.NroDocumento == request.NroDocumento).Select(r => r.CodPersona).FirstOrDefault();
                    if (codPersona == null) codPersona = _context.TPersona.Where(p => p.ApellidoPaterno == request.ApellidoPaterno && p.ApellidoMaterno == request.ApellidoMaterno && p.Nombres == request.Nombres).Select(r => r.CodPersona).FirstOrDefault();
                    if (codPersona == null)
                    {
                        codPersona = "E0" + request.NroDocumento;
                        var entityP = new TPersona();
                        entityP.CodPersona = codPersona;
                        entityP.NroDocumento = request.NroDocumento;
                        entityP.ApellidoPaterno = request.ApellidoPaterno;
                        entityP.ApellidoMaterno = request.ApellidoMaterno;
                        entityP.Nombres = request.Nombres;
                        entityP.Sexo = request.Sexo;
                        //entityP.CodTipoPersona = TipoPersona.Contratista;
                        entityP.CodTipoPersona = "2";
                        entityP.Ocupacion = request.Ocupacion;
                        entityP.Empresa = request.Empresa;
                        _context.TPersona.Add(entityP);
                        //  await _context.SaveChangesAsync(cancellationToken);                            
                    }
                    var Usuario = _context.TUsuario.Where(u => u.CodPersona == codPersona).FirstOrDefault();
                    if (Usuario == null)
                    {
                        var entityU = new TUsuario();
                        entityU.Usuario = request.NroDocumento;
                        entityU.CodPersona = codPersona;
                        entityU.Password = request.NroDocumento;
                        entityU.TipoLogueo = false;

                        var entityUR = new TUsuarioRol();
                        entityUR.CodUsuario = entityU.CodUsuario;
                        entityUR.CodRol = int.Parse(_configuration["appSettings:RolCapExterno"]);

                       entityU.UsuarioRoles.Add(entityUR);
                        _context.TUsuario.Add(entityU);

                    }
                    await _context.SaveChangesAsync(cancellationToken);
                    return codPersona;
                //} catch (Exception e) {                      
                //    new GeneralFailureException("Nodo padre no fue encontrado");
                //    throw;
                //}                
            }            

        }
    }
}
