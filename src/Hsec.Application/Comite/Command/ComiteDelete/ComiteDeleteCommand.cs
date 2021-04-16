using AutoMapper;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.ToleranciaCero.Models;
using Hsec.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.PlanAccion.Commands.DeleteDocRefHsec;

namespace Hsec.Application.Comite.Command.ComiteDelete
{
    public class ComiteDeleteCommand : IRequest<Unit>
    {
        public string CodComite { get; set; }

        public class ComiteDeleteCommandHandler : IRequestHandler<ComiteDeleteCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public ComiteDeleteCommandHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<Unit> Handle(ComiteDeleteCommand request, CancellationToken cancellationToken)
            {
                var _comite = _context.TComite.Include(p => p.ListaParticipantes).Where(u => u.CodComite == request.CodComite)
                    .FirstOrDefault();

                _comite.Estado = false;

                foreach (var persona in _comite.ListaParticipantes)
                {
                    persona.Estado = false;
                }

                _context.TComite.Update(_comite);
                await _context.SaveChangesAsync(cancellationToken);

                //var r2 = await _planAccion.DeleteByCodRef(request.CodComite);
                var r2 = await _mediator.Send(new DeleteDocRefCommand() { NroDocReferencia = request.CodComite });

                return Unit.Value;
            }
        }
    }
}
