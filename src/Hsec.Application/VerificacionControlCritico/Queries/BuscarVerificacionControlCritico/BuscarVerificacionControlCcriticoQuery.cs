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
using Hsec.Application.General.Maestro.Queries.GetMaestroData;

namespace Hsec.Application.VerificacionControlCritico.Queries.BuscarVerificacionControlCritico
{
    public class BuscarVerificacionControlCcriticoQuery : IRequest<BuscarVerificacionCCVM>
    {
        public string Codigo { get; set; }
        public string CodGerencia { get; set; }
        public string CodSuperIntendencia { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        //public string CodPersona { get; set; }

        public int Pagina { get; set; }
        public int PaginaTamanio { get; set; }
        public class BuscarVerificacionControlCcriticoQueryHandler : IRequestHandler<BuscarVerificacionControlCcriticoQuery, BuscarVerificacionCCVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public BuscarVerificacionControlCcriticoQueryHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<BuscarVerificacionCCVM> Handle(BuscarVerificacionControlCcriticoQuery request, CancellationToken cancellationToken)
            {
                try{
                    var vm = new BuscarVerificacionCCVM();
                    vm.List = new List<ItemBVCCDto>();
                    
                    var filerListQuery = _context.TVerificacionControlCritico
                        .Where(v =>
                                (v.Estado == true)
                            && (String.IsNullOrEmpty(request.Codigo) || v.CodigoVCC.Equals(request.Codigo))
                            && (String.IsNullOrEmpty(request.CodGerencia) || v.Gerencia.Equals(request.CodGerencia))
                            && (String.IsNullOrEmpty(request.CodSuperIntendencia) || v.SuperIndendecnia.Equals(request.CodSuperIntendencia))
                            && (String.IsNullOrEmpty(request.FechaInicio) || v.Fecha >= Convert.ToDateTime(request.FechaInicio))
                            && (String.IsNullOrEmpty(request.FechaFin) || v.Fecha <= Convert.ToDateTime(request.FechaFin))
                            );

                    vm.Count = filerListQuery.Count();

                    var ListQuery = filerListQuery
                        .OrderByDescending(i => i.CodigoVCC)
                        .Skip(request.Pagina * request.PaginaTamanio - request.PaginaTamanio)
                        .Take(request.PaginaTamanio)
                        .ProjectTo<ItemBVCCDto>(_mapper.ConfigurationProvider)
                        .ToList();

                    foreach(var item in ListQuery){
                        var listCriterios = _context.TVerificacionControlCriticoCartilla
                            .Where(t => t.Estado == true && t.CodigoVCC.Equals(item.CodigoVCC))
                            .ToList()
                            .Select(t => String.IsNullOrEmpty(t.Cumplimiento)?0.0f:float.Parse(t.Cumplimiento));
                        var total = listCriterios.Count();
                        if(total>0){
                            var sumaTotal = listCriterios.Sum();
                            item.Avance = (sumaTotal/total);
                        }
                        else{
                            item.Avance = 0.0f;
                        }
                        item.Mes = item.FechaCreacion.Month.ToString();
                        item.Anio = item.FechaCreacion.Year.ToString();
                    }
                    vm.List = ListQuery;

                    return vm;
                }
                catch(Exception e){
                    throw e;
                }
                // var VM = new BuscarVerificacionCCVM();
                // VM.list = new List<ItemBVCCDto>();

                // ItemBVCCDto temp = new ItemBVCCDto();
                // temp.Anio = "2020";
                // temp.Avance = "10";
                // temp.Cartilla = "MDW3242";
                // temp.CodGerencia = "3425";
                // temp.CodResponsable = "S4352342";
                // temp.FechaCreacion = "2020-03-03";
                // temp.Mes = "03";
                // temp.NroVerificacion = "VCC0000001";
                
                // VM.list.Add(temp);
                // temp.NroVerificacion = "VCC0000002";
                // VM.list.Add(temp);
                
            }
            private string buscarPorCodigo(ICollection<MaestroDataVM> texto, string table, string codigo)
            {

                try
                {
                    if (texto == null) return codigo;
                    var list = texto.Where(t => t.CodTabla.Equals(table)).Select(t => t.Tipos).FirstOrDefault();
                    var a = list.Where(t => t.CodRegistro.Equals(codigo)).Select(t => t.Descripcion).First();
                    return a;
                }
                catch (Exception e)
                {
                    return codigo;
                }

            }            
        }
    }
}