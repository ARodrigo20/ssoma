using AutoMapper;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Capacitaciones.PerfilCapacitacionPosicion.Queries.DTOs;
using Hsec.Application.Capacitaciones.PerfilCapacitacionPosicion.Queries.VMs;
using Hsec.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.General.JerarquiasPersona.Queries.GetJerarquiasPersona;

namespace Hsec.Application.Capacitaciones.PerfilCapacitacionPosicion.Queries
{
    public class GetCursoYCursoPosicionQuery : IRequest<GetCursoYCursoPosicionVM>
    {
        public string codPersona { get; set; } // codReferencia
        public class GetCursoYCursoPosicionQueryHandler : IRequestHandler<GetCursoYCursoPosicionQuery, GetCursoYCursoPosicionVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public GetCursoYCursoPosicionQueryHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<GetCursoYCursoPosicionVM> Handle(GetCursoYCursoPosicionQuery request, CancellationToken cancellationToken)
            {
                GetCursoYCursoPosicionVM modelVM = new GetCursoYCursoPosicionVM();
                string codPersona = request.codPersona;
                var cursosPersona = (from plan in _context.TPlanTema.Where(i => i.Estado)
                                    join temaCap in _context.TTemaCapacitacion.Where(i => i.Estado) on plan.CodTemaCapacita equals temaCap.CodTemaCapacita
                                    into lista
                                    from list in lista.DefaultIfEmpty()
                                    where (plan.Estado && list.Estado && codPersona == plan.CodReferencia && !plan.Tipo)
                                    select new CursoDto
                                    {                                     
                                        bd = true,
                                        codTemaCapacita = list.CodTemaCapacita,
                                        codTipo = list.CodTipoTema,
                                        competencia = list.CompetenciaHs,
                                        curso = list.Descripcion                                                                             

                                    }).OrderByDescending(i => i.codTemaCapacita).ToList();

                modelVM.curso = cursosPersona;
                //var val = await _jerPos.JerarquiaPersonaPosicion(codPersona);
                var val = await _mediator.Send(new JerarquiaPersonaGSQuery() { codPersona = codPersona });

                if (val.data.Count > 0) {
                    var codPosicion = val.data[0].data;

                    var cursosPosicion = (from plan in _context.TPlanTema.Where(i => i.Estado)
                                          join temaCap in _context.TTemaCapacitacion.Where(i => i.Estado) on plan.CodTemaCapacita equals temaCap.CodTemaCapacita
                                          into lista
                                          from list in lista.DefaultIfEmpty()
                                          where (plan.Estado && list.Estado && codPosicion.ToString() == plan.CodReferencia && plan.Tipo)
                                          select new CursoPosicionDto
                                          {
                                              codTemaCapacita = list.CodTemaCapacita,
                                              codTipo = list.CodTipoTema,
                                              posicion= val.data[0].label,
                                              competencia = list.CompetenciaHs,
                                              curso = list.Descripcion
                                          }).ToList();
                    modelVM.cursoPosicion = cursosPosicion;
                }      
                return modelVM;
            }
        }
    }
}