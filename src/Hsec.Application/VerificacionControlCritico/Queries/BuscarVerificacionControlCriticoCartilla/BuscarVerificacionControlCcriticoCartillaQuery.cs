using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Common.Mappings;
using Hsec.Application.Common.Models;
using Hsec.Domain.Entities;
using Hsec.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.General.PlanAnualVerifiacionCC.Queries.GetCartillasPorPersonFiltro;

namespace Hsec.Application.VerificacionControlCritico.Queries.BuscarVerificacionControlCriticoCartilla
{
    public class BuscarVerificacionControlCcriticoCartillaQuery : IRequest<BuscarVerificacionCCCartillaVM>
    {
        public string Cartilla { get; set; }
        public int CodGerencia { get; set; }
        public string CodPersona { get; set; }
        public string CodSuperIntendencia { get; set; }
        public string Anio { get; set; }
        // public string FechaFin { get; set; }
        //public string CodPersona { get; set; }

        public int Pagina { get; set; }
        public int PaginaTamanio { get; set; }
        public class BuscarVerificacionControlCcriticoCartillaQueryHandler : IRequestHandler<BuscarVerificacionControlCcriticoCartillaQuery, BuscarVerificacionCCCartillaVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public BuscarVerificacionControlCcriticoCartillaQueryHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<BuscarVerificacionCCCartillaVM> Handle(BuscarVerificacionControlCcriticoCartillaQuery request, CancellationToken cancellationToken)
            {
                try{
                    var vm = new BuscarVerificacionCCCartillaVM();
                    //si se te oruccre una idea mejor usala
                    var mes = DateTime.Now.Month.ToString();

                    //var plan = await _generalService.GetPlanVerificacionFiltroCC(request.CodGerencia,request.CodPersona,request.Cartilla,request.Anio,mes,request.Pagina,request.PaginaTamanio);
                    var _filtro = new FiltroVerCCPorPersonasFiltro();
                    _filtro.Gerencia = request.CodGerencia;
                    _filtro.CodPersona = request.CodPersona;
                    _filtro.CodReferencia = request.Cartilla;
                    _filtro.Anio = request.Anio;
                    _filtro.CodMes = mes;
                    _filtro.Pagina = request.Pagina;
                    _filtro.PaginaTamanio = request.PaginaTamanio;

                    var plan = await _mediator.Send(new GetVerCCPorPersonasFiltroQuery() { filtro = _filtro }); 
                    
                    vm = _mapper.Map<BuscarVerificacionCCCartillaVM>(plan);


                    foreach(var iten in vm.List){
                        iten.Avance = ((await resueltos(iten))*100/iten.Avance);
                    }

                    return vm;
                }
                catch(Exception e){
                    throw e;
                }
                
            }

            private async Task<int> resueltos(ItemBVCCDto iten){
                int resueltos = _context.TVerificacionControlCritico
                        .Where(t => 
                            t.Cartilla.Equals(iten.Cartilla)
                            && t.CodResponsable.Equals(iten.CodResponsable)
                            // && t.Gerencia.Equals(iten.Gerencia)
                            && t.Fecha.Year.ToString().Equals(iten.Anio)
                            ).Count();
                return resueltos;
            }

        }
    }
}