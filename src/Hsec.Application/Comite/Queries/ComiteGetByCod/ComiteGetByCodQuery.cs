using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Reunion.Models;
using Hsec.Domain.Entities.Otros;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.General.Personas.Queries.GetPersona;

namespace Hsec.Application.Comite.Queries.ComiteGetByCod
{
    public class ComiteGetByCodQuery : IRequest<ComiteGetDto>
    {
        public string CodComite { get; set; }

        public class ComiteGetByCodQueryHandler : IRequestHandler<ComiteGetByCodQuery, ComiteGetDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public ComiteGetByCodQueryHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<ComiteGetDto> Handle(ComiteGetByCodQuery request, CancellationToken cancellationToken)
            {
                var _comite = _context.TComite.Include(p => p.ListaParticipantes).Where(u => u.CodComite == request.CodComite)
                    .FirstOrDefault();

                var comite = new ComiteGetDto();

                comite.CodComite = request.CodComite;
                comite.Fecha = _comite.Fecha;
                comite.Hora = _comite.Hora;
                comite.CodTipoComite = _comite.CodTipoComite;
                comite.CodCategoria = _comite.CodCategoria;
                comite.CodPosicionGer = _comite.CodPosicionGer;
                comite.CodPosicionSup = _comite.CodPosicionSup;
                comite.Lugar = _comite.Lugar;
                comite.DetalleApertura = _comite.DetalleApertura;
                comite.CodSecretario = _comite.CodSecretario;
                comite.ResumenSalud = _comite.ResumenSalud;
                comite.ResumenSeguridad = _comite.ResumenSeguridad;
                comite.ResumenMedioAmbiente = _comite.ResumenMedioAmbiente;
                comite.ResumenComunidad = _comite.ResumenComunidad;
                comite.FechaCierre = _comite.FechaCierre;
                comite.HoraCierre = _comite.HoraCierre;

            List<string> ListParticipantes = _comite.ListaParticipantes.Where(p => p.Estado == true).Select(t => t.CodPersona).ToList();

                foreach (var item in ListParticipantes)
                {
                    //var pers = await _generalService.GetPersonas(item);
                    var pers = await _mediator.Send(new GetPersonaQuery() { CodPersona = item } );
                    //var convert = JsonConvert.DeserializeObject<PersonaVM>(pers);
                    comite.ListaParticipantes.Add(pers);
                }

                return comite;
            }
        }
    }
}
