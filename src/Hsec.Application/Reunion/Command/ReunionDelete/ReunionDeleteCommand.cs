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

namespace Hsec.Application.Reunion.Command.ReunionDelete
{
    public class ReunionDeleteCommand : IRequest<Unit>
    {
        public string CodReunion { get; set; }

        public class ReunionDeleteCommandHandler : IRequestHandler<ReunionDeleteCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public ReunionDeleteCommandHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<Unit> Handle(ReunionDeleteCommand request, CancellationToken cancellationToken)
            {
                var _reunion = _context.TReunion.Include(p => p.ReunionAsistentes).Include(c => c.ReunionAusentes).Include(r => r.ReunionJustificados).Include(a => a.ReunionAgendas).Where(u => u.CodReunion == request.CodReunion)
                    .FirstOrDefault();

                _reunion.Estado = false;

                foreach (var persona in _reunion.ReunionAsistentes)
                {
                    persona.Estado = false;
                }

                foreach (var persona in _reunion.ReunionAusentes)
                {
                    persona.Estado = false;
                }

                foreach (var persona in _reunion.ReunionJustificados)
                {
                    persona.Estado = false;
                }

                _context.TReunion.Update(_reunion);
                await _context.SaveChangesAsync(cancellationToken);

                //var r2 = await _planAccion.DeleteByCodRef(request.CodReunion);
                var r2 = await _mediator.Send(new DeleteDocRefCommand() { NroDocReferencia = request.CodReunion });

                return Unit.Value;
            }
        }
    }
}
