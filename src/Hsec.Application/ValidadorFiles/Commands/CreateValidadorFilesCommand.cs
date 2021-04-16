using AutoMapper;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.ValidadorFiles.Commands.VMs;
using Hsec.Domain.Entities.PlanAccion;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.ValidadorFiles.Commands
{
    public class CreateValidadorFilesCommand : IRequest<FileVM>
    {
        public string nroDocReferencia { get; set; }
        public string codPersona { get; set; }
        public int codArchivo { get; set; }
        public class CreateValidadorFilesCommandHandler : IRequestHandler<CreateValidadorFilesCommand, FileVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public CreateValidadorFilesCommandHandler(IApplicationDbContext context, IMapper mapper)
            {
                this._context = context;             
                this._mapper = mapper;            
            }

            public async Task<FileVM> Handle(CreateValidadorFilesCommand request, CancellationToken cancellationToken)
            {
                //var busquedaTValFile = _context.TValidadorArchivo.Where(i => i.Estado && 
                //i.NroDocReferencia == codCurso &&
                //i.CodArchivo == codArchivo &&
                //i.CodPersona == codPersona);
                var codCurso = request.nroDocReferencia;
                var codPersona = request.codPersona;
                var codArchivo = request.codArchivo;
                
                var validacion = _context.TValidadorArchivo.Any(i => i.Estado && i.NroDocReferencia == codCurso && i.CodPersona == codPersona && i.CodArchivo == codArchivo);

                if (!validacion) {

                    TValidadorArchivo valFile = new TValidadorArchivo();
                    valFile.CodArchivo = codArchivo;
                    valFile.CodPersona = codPersona;
                    valFile.NroDocReferencia = codCurso;
                    valFile.Estado = true;
                    valFile.EstadoAccion = 1;
                    _context.TValidadorArchivo.Add(valFile);
                    await _context.SaveChangesAsync(cancellationToken);
                }

                var user = _context.TFile.FirstOrDefault(a => a.CorrelativoArchivos == codArchivo && a.Estado);
                if (user != null)
                {
                    FileVM fileVM = new FileVM();
                    byte[] imgbyte = user.ArchivoData;
                    if (user == null)
                    {
                        throw new ExceptionGeneral(user.ToString());
                    }
                    fileVM.archivoData = user.ArchivoData;
                    fileVM.nombre = user.Nombre;
                    fileVM.tipoArchivo = user.TipoArchivo;
                    return fileVM;
                }
                else
                {
                    throw new ExceptionGeneral("EL ARCHIVO FUE ELIMINADO !");
                }
            }
        }
    }
}