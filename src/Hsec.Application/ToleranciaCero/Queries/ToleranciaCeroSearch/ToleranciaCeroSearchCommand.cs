using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.ToleranciaCero.Models;
using Hsec.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.General.Jerarquias.Queries.Code2Name;
using Hsec.Application.General.Empresa.Queries.GetCode2Name;

namespace Hsec.Application.ToleranciaCero.Queries.ToleranciaCeroSearch
{
    public class ToleranciaCeroSearchCommand : IRequest<ToleranciaCeroModel>
    {
        public string CodTolCero { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

        public string CodPosicionGer { get; set; }

        public string CodPosicionSup { get; set; }

        public int Pagina { get; set; }
        public int PaginaTamanio { get; set; }

        public class ToleranciaCeroSearchHandler : IRequestHandler<ToleranciaCeroSearchCommand, ToleranciaCeroModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public ToleranciaCeroSearchHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<ToleranciaCeroModel> Handle(ToleranciaCeroSearchCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var vm = new ToleranciaCeroModel();

                    var filerListQuery = _context.ToleranciaCero
                        .Where(t => (t.CodTolCero == request.CodTolCero || request.CodTolCero == null || request.CodTolCero == "") && (t.CodPosicionGer == request.CodPosicionGer || request.CodPosicionGer == null || request.CodPosicionGer == "") && (t.CodPosicionSup == request.CodPosicionSup || request.CodPosicionSup == null || request.CodPosicionSup == "")
                            && (t.FechaTolerancia >= request.FechaInicio || request.FechaInicio == DateTime.MinValue || request.FechaInicio == null) && (t.FechaTolerancia <= request.FechaFin || request.FechaFin == DateTime.MinValue || request.FechaFin == null) && t.Estado == true);

                    vm.count = filerListQuery.Count();

                    var ListQuery = filerListQuery
                            .OrderByDescending(t => t.CodTolCero)
                            .Skip(request.Pagina * request.PaginaTamanio - request.PaginaTamanio)
                            .Take(request.PaginaTamanio)
                            .ProjectTo<ToleranciaCeroDto>(_mapper.ConfigurationProvider)
                            .ToHashSet();

                    vm.Lists = ListQuery;

                    foreach (var item in vm.Lists)
                    {
                        if (!string.IsNullOrEmpty(item.CodPosicionGer))
                        {
                            //item.CodPosicionGer = await _generalService.GetJeraquias(item.CodPosicionGer);
                            item.CodPosicionGer = await _mediator.Send(new Code2NameQuery() { codigo = item.CodPosicionGer });
                        }
                        if (!string.IsNullOrEmpty(item.CodPosicionSup))
                        {
                            //item.CodPosicionSup = await _generalService.GetJeraquias(item.CodPosicionSup);
                            item.CodPosicionSup = await _mediator.Send(new Code2NameQuery() { codigo = item.CodPosicionSup });
                        }
                        if (!string.IsNullOrEmpty(item.Proveedor))
                        {
                            //item.Proveedor = await _generalService.GetProveedor(item.Proveedor);
                            item.Proveedor = await _mediator.Send(new GetCode2NameQuery() { codigo = item.Proveedor });
                        }
                    }

                    return vm;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
    }
}
