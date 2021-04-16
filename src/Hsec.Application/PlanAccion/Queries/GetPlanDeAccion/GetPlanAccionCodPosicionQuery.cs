using Hsec.Application.Common.Interfaces;
using Hsec.Application.PlanAccion.Queries.GetPlanDeAccion;
using Hsec.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.General.JerarquiasPersona.Queries.GetJerarquiasPersonaAll;
using Hsec.Application.General.Jerarquias.Queries.GetJerarquiasPersonaAll;
using Hsec.Application.Common.Models;
using Hsec.Application.General.Jerarquias.Queries.GetJerarquia;

namespace Hsec.Application.PlanAccion.Queries.GetPlanDeAccion
{
    public class GetPlanAccionCodPosicionQuery : IRequest<JerarquiaVM>
    {
        public GetPlanAccionFiltradoDto plan { get; set; }
        public class GetPlanAccionCodPosicionQueryHandler : IRequestHandler<GetPlanAccionCodPosicionQuery, JerarquiaVM>
        {
            private readonly IApplicationDbContext _context;
            //private readonly IPersonasService _persons;
            //private readonly IJerarquiaCodPosicion _jerarquiaCodPosicion;
            //private readonly IJerarquiaPersona _jerarquiaPersona;
            //private readonly IPersonasJerarquiaAccion _perPorPosicion;
            private readonly IMediator _mediator;

            private IList<JerarquiaPersonaCodPosicionDto> lista;
            public IList<int?> codPosiciones { get; set; }
            public GetPlanAccionCodPosicionQueryHandler(
                IApplicationDbContext context,
                IMediator mediator
                )
            {
                _context = context;
                //_persons = persons;
                //_jerarquiaCodPosicion = jerarquiaCodPosicion;
                //_perPorPosicion = perPorPosicion;
                //_jerarquiaPersona = jerarquiaPersona;
                _mediator = mediator;

                codPosiciones = new List<int?>();
            }

            public async Task<JerarquiaVM> Handle(GetPlanAccionCodPosicionQuery request, CancellationToken cancellationToken)
            {
                //JerarquiaPersonaCodPosicionVM perPorPosicion = await _perPorPosicion.requestPerJerAccion(request.plan.codPosicion);
                GeneralCollection<PersonaPosVM> perPorPosicion = await _mediator.Send(new GetJerarquiasPersonaAllQuery() { CodPosicion = request.plan.codPosicion.Value });
                List<string> personas = new List<string>();
                foreach (var it in perPorPosicion.Data)
                {
                    personas.Add(it.CodPersona);
                }
                var item = request.plan;
                JerarquiaVM hijosPos = new JerarquiaVM();

                var Person = (from accion in _context.TAccion
                              join responsable in _context.TResponsable on accion.CodAccion equals responsable.CodAccion into lista
                              from list in lista.DefaultIfEmpty()
                              where personas.Contains(list.CodPersona) && accion.Estado && ((item.codAccion == 0) || (accion.CodAccion.ToString().EndsWith(item.codAccion.ToString()))) &&
                               (String.IsNullOrEmpty(item.docReferencia) || accion.DocReferencia.Contains(item.docReferencia)) &&
                               (String.IsNullOrEmpty(item.codTablaRef) || accion.CodTablaRef.Contains(item.codTablaRef)) &&
                               (String.IsNullOrEmpty(item.codEstadoAccion) || accion.CodEstadoAccion.Contains(item.codEstadoAccion)) &&
                               ((item.fechaInicial.Date <= accion.FechaSolicitud.Date && accion.FechaSolicitud.Date <= item.fechaFinal.Date))
                              select new Tuple<int, string>(accion.CodAccion, list.CodPersona)
                                 ).ToList();

                if (!item.check) // get List Person
                {
                    
                    foreach (var per in perPorPosicion.Data)
                    {
                        JerarquiaNodeVM person = new JerarquiaNodeVM();
                        person.label = per.ApellidoPaterno + ' ' + per.ApellidoMaterno + ", " + per.Nombre;
                        person.data = per.CodPersona;
                        person.count = Person.Count(t => t.Item2 == per.CodPersona);
                        person.children = null;
                        person.tipo = "per";
                        if(person.count>0) hijosPos.data.Add(person);
                    }
                }
                else //get list Jerarquias
                {                   
                    //var jerPos = await _jerarquiaCodPosicion.RequestJerarquiaCodPosicion(request.plan.codPosicion);
                    var jerPos = await _mediator.Send(new GetJerarquiaIdQuery() { CodPosicion = request.plan.codPosicion.Value });
                    foreach (var jer in jerPos.children) {
                        var PersonJera = perPorPosicion.Data.Where(p=>p.Path.Contains((char)jer.data.Value)).Select(p=>p.CodPersona).ToList();
                        JerarquiaNodeVM jerar = new JerarquiaNodeVM();
                        jerar.label = jer.label;
                        jerar.data = jer.data.Value + "";
                        jerar.tipo = "pos";
                        jerar.count = Person.Where(t => PersonJera.Contains(t.Item2)).Select(s=>s.Item1).ToList().GroupBy(i => i).Select(o => o.First()).Count();
                        if (jerar.count > 0) hijosPos.data.Add(jerar);
                    }
                }
                return hijosPos;
                //// old Code
                //DateTime date = DateTime.Today;

            }

        }
    }
}