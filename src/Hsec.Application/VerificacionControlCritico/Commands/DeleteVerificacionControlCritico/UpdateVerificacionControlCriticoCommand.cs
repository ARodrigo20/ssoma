using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Common.Mappings;
using Hsec.Application.Common.Models;
using Hsec.Domain.Entities;
using Hsec.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.PlanAccion.Commands.DeleteDocRefHsec;
using Hsec.Application.Files.Commands.DeleteFileDocRef;

namespace Hsec.Application.VerificacionControlCritico.Commands.DeleteVerificacionControlCritico
{
    public class DeleteVerificacionControlCriticoCommand : IRequest<Unit>
    {
        public string CodVCC {get;set;}
        public class DeleteVerificacionControlCriticoCommandHandler : IRequestHandler<DeleteVerificacionControlCriticoCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;            

            public DeleteVerificacionControlCriticoCommandHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<Unit> Handle(DeleteVerificacionControlCriticoCommand request, CancellationToken cancellationToken)
            {
                try{
                    var CODIGO = request.CodVCC;

                    var data = _context.TVerificacionControlCritico.Where(t => t.Estado == true && t.CodigoVCC.Equals(CODIGO)).FirstOrDefault();

                    if(data==null) throw new Exception("error no existe el dato "+CODIGO);
                    data.Estado = false;

                    _context.TVerificacionControlCritico.Update(data);

                    var ListItemControlCritico = _context.TVerificacionControlCriticoCartilla.Where(t => t.CodigoVCC.Equals(CODIGO) );

                    foreach(var item in ListItemControlCritico){
                        item.Estado = false;
                        _context.TVerificacionControlCriticoCartilla.Update(item);
                    }

                    var r1 = await _context.SaveChangesAsync(cancellationToken);


                    //var r2 = await _imagenes.Delete(CODIGO);
                    var r2 = await _mediator.Send(new DeleteFileDocRefCommand() { NroDocReferencia = CODIGO });


                    //var r3 = await _planAccion.Delete(CODIGO);
                    var r3 = await _mediator.Send(new DeleteDocRefCommand() { NroDocReferencia = CODIGO });

                    return Unit.Value;
                }
                catch(Exception ex){
                    throw ex;
                }
            }

            
        }
    }
}