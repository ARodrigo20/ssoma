using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Claims;
using Newtonsoft.Json;
using Hsec.Application.Reunion.Queries.ReunionGetByCod;
using System.Collections.Generic;
using Hsec.Application.General.Personas.Queries.GetPersona;

namespace Hsec.Application.Reunion.Queries.ReunionSearch
{
    public class ReunionSearchQuery : IRequest<ReunionVM>
    {
        public string Reunion { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

        public string Lugar { get; set; }

        public string CodPerFacilitador { get; set; }

        public int Pagina { get; set; }
        public int PaginaTamanio { get; set; }

        public class ReunionSearchHandler : IRequestHandler<ReunionSearchQuery, ReunionVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;
            private readonly IHttpContextAccessor _httpContext;

            public ReunionSearchHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator, IHttpContextAccessor HttpContext)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
                _httpContext = HttpContext;
            }

            public async Task<ReunionVM> Handle(ReunionSearchQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var vm = new ReunionVM();

                    var userToken = _httpContext.HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                    var rolToken = _httpContext.HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

                    var filerListQuery = _context.TReunion
                        .Where(t => (t.Reunion == request.Reunion || string.IsNullOrEmpty(request.Reunion))
                            && (t.Fecha >= request.FechaInicio || request.FechaInicio == DateTime.MinValue || request.FechaInicio == null) && (t.Fecha <= request.FechaFin || request.FechaFin == DateTime.MinValue || request.FechaFin == null) 
                            && (t.Lugar == request.Lugar || string.IsNullOrEmpty(request.Lugar))
                            && (t.CodPerFacilitador == request.CodPerFacilitador || string.IsNullOrEmpty(request.CodPerFacilitador))
                            && t.Estado == true && (t.CreadoPor.Contains(userToken) || rolToken != "6"));

                    vm.count = filerListQuery.Count();

                    var ListQuery = filerListQuery
                            .OrderByDescending(t => t.CodReunion)
                            .Skip(request.Pagina * request.PaginaTamanio - request.PaginaTamanio)
                            .Take(request.PaginaTamanio)
                            .ToHashSet();

                    var ListReunion = new HashSet<ReunionSearchDto>();
                    foreach (var item in ListQuery)
                    {
                        ReunionSearchDto reunion = new ReunionSearchDto();
                        reunion.CodReunion = item.CodReunion;
                        reunion.Reunion = item.Reunion;
                        reunion.Lugar = item.Lugar;
                        reunion.Fecha = item.Fecha;
                        reunion.Acuerdos = item.Acuerdos;
                        reunion.Comentarios = item.Comentarios;
                        reunion.Otros = item.Otros;
                        reunion.UsuCreacion = item.CreadoPor;

                        if(item.CodPerFacilitador!= null)
                        {
                            //var pers = await _generalService.GetPersonas(item.CodPerFacilitador);
                            var pers = await _mediator.Send(new GetPersonaQuery() { CodPersona = item.CodPerFacilitador } );

                            //reunion.CodPerFacilitador = JsonConvert.DeserializeObject<PersonaDto>(pers);
                            reunion.CodPerFacilitador = pers;
                        }

                        ListReunion.Add(reunion);
                    }

                    vm.Lists = ListReunion;

                    foreach (var row in vm.Lists)
                    {
                        row.Editable = row.UsuCreacion == userToken || (new string[] { "1", "4" }).Contains(rolToken);
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