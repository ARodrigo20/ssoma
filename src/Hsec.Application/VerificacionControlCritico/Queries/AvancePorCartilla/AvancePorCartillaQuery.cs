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
using Hsec.Application.General.PlanAnualVerifiacionCC.Queries.GetCartillasPorPerson;

namespace Hsec.Application.VerificacionControlCritico.Queries.AvancePorCartilla
{
    public class AvancePorCartillaQuery : IRequest<AvancePorCartillaVM>
    {
        public string CodPersona { get; set; }
        public string CodReferencia { get; set; }
        public string Anio { get; set; }
        public string CodMes { get; set; }
        public class AvancePorCartillaQueryHandler : IRequestHandler<AvancePorCartillaQuery, AvancePorCartillaVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public AvancePorCartillaQueryHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<AvancePorCartillaVM> Handle(AvancePorCartillaQuery request, CancellationToken cancellationToken)
            {
                var vm = new AvancePorCartillaVM();

                //var plan = await _general.GetPlanVerificacionCC(request.CodPersona,request.CodReferencia,request.Anio,request.CodMes);
                var plan = await _mediator.Send(new GetVerCCPorPersonasQuery() { CodPersona = request.CodPersona, CodReferencia = request.CodReferencia, Anio = request.Anio, CodMes = request.CodMes });

                foreach (var item in plan.list){
                    var list_VCC = _context.TVerificacionControlCritico.Where(t => 
                        t.Fecha.Month == Int32.Parse(item.CodMes) 
                        && t.Fecha.Year == Int32.Parse(item.Anio)
                        && t.CodResponsable.Equals(item.CodPersona)
                        && t.Cartilla.Equals(item.CodReferencia))
                        .ToList();
                    
                    //var faltantes = Int32.Parse(item.Valor) - list_VCC.Count;
                    
                    //foreach(var itemVCC in list_VCC){
                    //    var nuevo = new AvenceDto(itemVCC.CodigoVCC,itemVCC.Cartilla,item.Anio,item.CodMes,item.CodPersona);
                    //    vm.list.Add(nuevo);
                    //}


                    //for (int i = 0; i < faltantes; i++)
                    //{
                    //    var nuevo = new AvenceDto("",item.CodReferencia,item.Anio,item.CodMes,item.CodPersona);
                    //    vm.list.Add(nuevo);
                    //}
                    if(list_VCC.Count() > 0)
                    {
                        var nuevo = new AvenceDto(list_VCC.First().CodigoVCC, item.CodReferencia, item.Anio, item.CodMes, item.CodPersona);
                        vm.list.Add(nuevo);
                    }
                    else
                    {
                        var nuevo = new AvenceDto("", item.CodReferencia, item.Anio, item.CodMes, item.CodPersona);
                        vm.list.Add(nuevo);
                    }


                    vm.list = vm.list.OrderBy(t => Int32.Parse(t.CodMes)).ToList();
                }
                return vm;
            }
        }
    }
}