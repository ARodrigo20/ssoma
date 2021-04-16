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

namespace Hsec.Application.Auditoria.Queries.GetBuscarHallazgosAuditoria
{
    public class GetBuscarHallazgosAuditoriaQuery : IRequest<BuscarHallazgosAuditoriaVM>
    {
        public int Pagina { get; set; }
        public int PaginaTamanio { get; set; }
        public string CodAuditoria { get; set; }
        public class GetBuscarHallazgosAuditoriaQueryHandler : IRequestHandler<GetBuscarHallazgosAuditoriaQuery, BuscarHallazgosAuditoriaVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetBuscarHallazgosAuditoriaQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<BuscarHallazgosAuditoriaVM> Handle(GetBuscarHallazgosAuditoriaQuery request, CancellationToken cancellationToken)
            {
                var VM = new BuscarHallazgosAuditoriaVM();

                var filerListQuery = _context.THallazgos
                    .Where(t => t.Estado == true && t.CodAuditoria.Equals(request.CodAuditoria));
                
                VM.Count = filerListQuery.Count();

                VM.Pagina = request.Pagina;

                var ListQuery = filerListQuery
                    .OrderByDescending(i => i.CodAuditoria)
                    .Skip(request.Pagina * request.PaginaTamanio - request.PaginaTamanio)
                    .Take(request.PaginaTamanio)
                    .ToHashSet();

                foreach(var itemT in ListQuery){
                    var itemDto = new HallazgoDto();
                    itemDto.TipoHallazgo = itemT.CodTipoHallazgo;
                    itemDto.CodHallazgo = itemT.CodHallazgo;

                    switch(itemT.CodTipoHallazgo){
                        case TipoHallazgo.NoConformidad:
                            var itemHallazgoNC = _context.TAnalisisHallazgo
                                .Where(t => t.Estado == true && t.CodHallazgo.Equals(itemDto.CodHallazgo))
                                .FirstOrDefault();
                            itemDto.TipoNoconformidad = itemHallazgoNC.CodTipoNoConfor;
                            itemDto.CierreNoConformidad = itemHallazgoNC.CodRespCierNoConfor;
                        break;
                        case TipoHallazgo.Observacion:
                            var itemHallazgoObs = _context.TDatosHallazgo
                                .Where(t => t.Estado == true && t.CodHallazgo.Equals(itemDto.CodHallazgo))
                                .FirstOrDefault();
                            itemDto.ObsDocAuditoria = itemHallazgoObs.CodTipoHallazgo;
                            itemDto.ObsDescripcion = itemHallazgoObs.Descripcion;
                        break;
                        case TipoHallazgo.OportunidadMejora:
                            var itemHallazgoOpor = _context.TDatosHallazgo
                                    .Where(t => t.Estado == true && t.CodHallazgo.Equals(itemDto.CodHallazgo))
                                    .FirstOrDefault();
                            itemDto.OpoDocAuditoria = itemHallazgoOpor.CodTipoHallazgo;
                            itemDto.OpoDescripcion = itemHallazgoOpor.Descripcion;
                        break;
                        case TipoHallazgo.RequiereCorreccion:
                            var itemHallazgoReq = _context.TDatosHallazgo
                                    .Where(t => t.Estado == true && t.CodHallazgo.Equals(itemDto.CodHallazgo))
                                    .FirstOrDefault();
                            itemDto.ReqDocAuditoria = itemHallazgoReq.CodTipoHallazgo;
                            itemDto.ReqDescripcion = itemHallazgoReq.Descripcion;
                        break;
                    }
                    VM.list.Add(itemDto);
                }
                return VM;
            }
        }
    }
}