using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Exceptions;
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


namespace Hsec.Application.ToleranciaCero.Command.ToleranciaCeroDelete
{
    public class ToleranciaCeroDeleteCommand : IRequest<Unit>
    {
        public string CodTolCero { get; set; }

        public class ToleranciaCeroDeleteCommandHandler : IRequestHandler<ToleranciaCeroDeleteCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public ToleranciaCeroDeleteCommandHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<Unit> Handle(ToleranciaCeroDeleteCommand request, CancellationToken cancellationToken)
            {
                var _tolerancia = _context.ToleranciaCero.Include(p => p.ToleranciaPersonas).Include(c => c.ToleranciaAnalisisCausa).Include(r => r.ToleranciaReglas).Where(u => u.CodTolCero == request.CodTolCero)
                    .FirstOrDefault();

                _tolerancia.Estado = false;

                foreach(var persona in _tolerancia.ToleranciaPersonas)
                {
                    persona.Estado = false;
                }

                foreach(var causa in _tolerancia.ToleranciaAnalisisCausa)
                {
                    causa.Estado = false;
                }

                foreach(var regla in _tolerancia.ToleranciaReglas)
                {
                    regla.Estado = false;
                }

                _context.ToleranciaCero.Update(_tolerancia);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
