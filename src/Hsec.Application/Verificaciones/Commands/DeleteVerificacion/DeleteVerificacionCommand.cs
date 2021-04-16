using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities;
using Hsec.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.Files.Commands.DeleteFileDocRef;
using Hsec.Application.PlanAccion.Commands.DeleteDocRefHsec;

namespace Hsec.Application.Verificaciones.Commands.DeleteVerificacion
{

    public class DeleteVerificacionCommand : IRequest<int>
    {        
        public string Codigo { get; set; }
        public class DeleteVerificacionCommandHandler : IRequestHandler<DeleteVerificacionCommand, int>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public DeleteVerificacionCommandHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<int> Handle(DeleteVerificacionCommand request, CancellationToken cancellationToken)
            {
                var item = _context.TVerificaciones.Where(t => t.Estado == true && t.CodVerificacion.Equals(request.Codigo)).FirstOrDefault();
                if(item==null) throw new NotFoundException("TVerificaciones",request.Codigo);
                else{
                    item.Estado = false;
                    _context.TVerificaciones.Update(item);
                }

                var iperc = _context.TVerificacionIPERC.Where(t => t.Estado == true && t.CodVerificacion.Equals(request.Codigo)).FirstOrDefault();
                if(iperc != null){
                    iperc.Estado = false;
                    _context.TVerificacionIPERC.Update(iperc);
                }

                var ptar = _context.TVerificacionPTAR.Where(t => t.Estado == true && t.CodVerificacion.Equals(request.Codigo)).FirstOrDefault();
                if(ptar != null){
                    ptar.Estado = false;
                    _context.TVerificacionPTAR.Update(ptar);
                }

                var r1 = await _context.SaveChangesAsync(cancellationToken);

                //var r2 = await _imagen.Delete(request.Codigo);
                var r2 = await _mediator.Send(new DeleteFileDocRefCommand() { NroDocReferencia = request.Codigo });

                //var r3 = await _planAccion.Delete(request.Codigo);
                var r3 = await _mediator.Send(new DeleteDocRefCommand() { NroDocReferencia = request.Codigo });

                return 0;
            }
        }

    }
}