using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.ToleranciaCero.Command.ToleranciaCeroInsert;
using Hsec.Application.ToleranciaCero.Models;
using Hsec.Domain.Entities.Otros;
using Hsec.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.General.Personas.Queries.GetPersona;

namespace Hsec.Application.ToleranciaCero.Queries.ToleranciaCeroGetCod
{
    public class ToleranciaCeroGetCodQuery : IRequest<ToleranciaCeroGetDto>
    {
        public string CodTolCero { get; set; }

        public class ToleranciaCeroGetCodQueryHandler : IRequestHandler<ToleranciaCeroGetCodQuery, ToleranciaCeroGetDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public ToleranciaCeroGetCodQueryHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<ToleranciaCeroGetDto> Handle(ToleranciaCeroGetCodQuery request, CancellationToken cancellationToken)
            {
                var _tolerancia = _context.ToleranciaCero.Include(p => p.ToleranciaPersonas).Include(c => c.ToleranciaAnalisisCausa).Include(r => r.ToleranciaReglas).Where(u => u.CodTolCero == request.CodTolCero)
                    .FirstOrDefault();

                var tolerancia = new ToleranciaCeroGetDto();

                tolerancia.FechaTolerancia = _tolerancia.FechaTolerancia;
                tolerancia.CodPosicionGer = _tolerancia.CodPosicionGer;
                tolerancia.CodPosicionSup = _tolerancia.CodPosicionSup;
                tolerancia.Proveedor = _tolerancia.Proveedor;
                tolerancia.AntTolerancia = _tolerancia.AntTolerancia;
                tolerancia.IncumpDesc = _tolerancia.IncumpDesc;
                tolerancia.ConsecReales = _tolerancia.ConsecReales;
                tolerancia.ConsecPot = _tolerancia.ConsecPot;
                tolerancia.ConclusionesTol = _tolerancia.ConclusionesTol;
                tolerancia.CodDetSancion = _tolerancia.CodDetSancion;


                foreach (var item in _tolerancia.ToleranciaAnalisisCausa)
                {
                    var causa = _mapper.Map<TToleranciaCeroAnalisisCausa, TToleranciaCeroAnalisisCausaDto> (item);
                    tolerancia.Causas.Add(causa);
                }

                List<string> ListPerson = _tolerancia.ToleranciaPersonas.Where(p => p.Estado == true).Select( t => t.CodPersona).ToList();

                foreach (var item in ListPerson)
                {
                    //var pers = await _generalService.GetPersonas(item);
                    var pers = await _mediator.Send(new GetPersonaQuery() { CodPersona = item });
                    
                    //var convert = JsonConvert.DeserializeObject<PersonaDto>(pers);
                    tolerancia.Personas.Add(pers);
                }

                tolerancia.Reglas = _tolerancia.ToleranciaReglas.Where(r => r.Estado == true).Select(o => o.CodRegla).ToList();

                return tolerancia;
            }
        }
    }
}
