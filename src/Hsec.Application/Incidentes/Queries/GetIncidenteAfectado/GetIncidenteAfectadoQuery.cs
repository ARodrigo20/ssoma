using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Incidentes;
using Hsec.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Incidentes.Queries.GetIncidenteAfectado
{

    public class GetIncidenteAfectadoQuery : IRequest<AfectadosDescipcionesAccionesVM>
    {
        public string CodIncidente { get; set; }
        public class GetIncidenteAfectadoQueryHandler : IRequestHandler<GetIncidenteAfectadoQuery, AfectadosDescipcionesAccionesVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetIncidenteAfectadoQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<AfectadosDescipcionesAccionesVM> Handle(GetIncidenteAfectadoQuery request, CancellationToken cancellationToken)
            {
                var VM = new AfectadosDescipcionesAccionesVM();
                var CODIGO_INCIDENTE = request.CodIncidente;

                TIncidente objTIncidente = _context.TIncidente.Find(request.CodIncidente);

                if (objTIncidente == null || objTIncidente.Estado == false) throw new NotFoundException("Incidente", request.CodIncidente);


                VM = _context.TDetalleAfectado
                    .Where(t => t.Estado == true && t.CodIncidente.Equals(CODIGO_INCIDENTE))
                    .ProjectTo<AfectadosDescipcionesAccionesVM>(_mapper.ConfigurationProvider)
                    .FirstOrDefault();

                var listAC = _context.TAfectadoComunidad.Where(t => t.Estado == true && t.CodIncidente.Equals(CODIGO_INCIDENTE));
                foreach (var item in listAC)
                {
                    var obj = _mapper.Map<DetalleAfectadoDto>(item);
                    VM.Afectados.Add(obj);
                }

                var listMA = _context.TAfectadoMedioAmbiente.Where(t => t.Estado == true && t.CodIncidente.Equals(CODIGO_INCIDENTE));
                foreach (var item in listMA)
                {
                    var obj = _mapper.Map<DetalleAfectadoDto>(item);
                    VM.Afectados.Add(obj);
                }

                var listProp = _context.TAfectadoPropiedad.Where(t => t.Estado == true && t.CodIncidente.Equals(CODIGO_INCIDENTE));
                foreach (var item in listProp)
                {
                    var obj = _mapper.Map<DetalleAfectadoDto>(item);
                    VM.Afectados.Add(obj);
                }

                var listPersona = _context.TInvestigaAfectado.Where(t => t.Estado == true && t.CodIncidente.Equals(CODIGO_INCIDENTE));
                foreach (var item in listPersona)
                {
                    var obj = _mapper.Map<DetalleAfectadoDto>(item);
                    VM.Afectados.Add(obj);
                }

                return VM;
            }
        }

    }
}