using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Common.Mappings;
using Hsec.Application.Auditoria.Models;
using Hsec.Domain.Common;
using Hsec.Domain.Entities.Auditoria;
using Hsec.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.PlanAccion.Commands.CreatePlanDeAccion;

namespace Hsec.Application.Auditoria.Commands.CreateAuditoria
{

    public class CreateAuditoriaCommand : IRequest<string>
    {
        public AuditoriaDto data { get; set; }
        public List<PlanVM> planAccion { get; set; }
        public IFormFileCollection Files { get; set; }

        public class CreateAuditoriaCommandHandler : IRequestHandler<CreateAuditoriaCommand, string>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public CreateAuditoriaCommandHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<string> Handle(CreateAuditoriaCommand request, CancellationToken cancellationToken)
            {
                // Monitor.Enter(this);
                try{
                    // lock (this){
                        List<PlanVM> list_plan_accion = new List<PlanVM>();

                        var nuevo = _mapper.Map<AuditoriaDto,TAuditoria>(request.data);
                        int addAuditorIndex = 0;
                        foreach(var auditorDto in request.data.Equipo){
                            var addAuditorT = _mapper.Map<EquipoAuditorDto,TEquipoAuditor>(auditorDto);
                            addAuditorT.Correlativo = addAuditorIndex++;
                            nuevo.TEquipoAuditor.Add(addAuditorT);
                        }

                        var nuevoCodigo = nextCod();
                        var nextCorrelativo = nextCorrelativoHallazgo();
                        var CodigoHallazgo = nextCodHallazgo();

                        foreach(var hallasgoDto in request.data.Hallazgos){
                            CodigoHallazgo = nextCodHallazgo(CodigoHallazgo);
                            var hallasgoT = _mapper.Map<HallazgoDto,THallazgos>(hallasgoDto);
                            hallasgoT.CodHallazgo = CodigoHallazgo;
                            nuevo.THallazgos.Add(hallasgoT);

                            if(hallasgoDto.NoConformidad!=null){
                                hallasgoT.CodTipoHallazgo = TipoHallazgo.NoConformidad;
                                var AnalisisHallasgo = _mapper.Map<NoConformidadDto,TAnalisisHallazgo>(hallasgoDto.NoConformidad);
                                AnalisisHallasgo.CodHallazgo = CodigoHallazgo;
                                AnalisisHallasgo.CodTipoHallazgo = TipoHallazgo.NoConformidad;
                                _context.TAnalisisHallazgo.Add(AnalisisHallasgo);
                                if(hallasgoDto.NoConformidad.AnalisisCausalidad!=null)
                                    foreach(var analsisCausalidadDto in hallasgoDto.NoConformidad.AnalisisCausalidad){
                                        var TAnalisisCausalidad = _mapper.Map<AnalisisCausalidadDto,TAuditoriaAnalisisCausalidad>(analsisCausalidadDto);
                                        TAnalisisCausalidad.CodHallazgo = CodigoHallazgo;
                                        _context.TAuditoriaAnalisisCausalidad.Add(TAnalisisCausalidad);
                                    }
                            }
                            if(hallasgoDto.Observacion!=null){
                                hallasgoT.CodTipoHallazgo = TipoHallazgo.Observacion;
                                var ObservacionT = _mapper.Map<DatosHallazgoDto,TDatosHallazgo>(hallasgoDto.Observacion);
                                ObservacionT.CodHallazgo = CodigoHallazgo;
                                ObservacionT.CodTipoHallazgo = TipoHallazgo.Observacion;
                                ObservacionT.Correlativo = nextCorrelativo; 
                                nextCorrelativo = nextCorrelativoHallazgo(nextCorrelativo);
                                _context.TDatosHallazgo.Add(ObservacionT);
                            }
                            if(hallasgoDto.OportunidadMejora!=null){
                                hallasgoT.CodTipoHallazgo = TipoHallazgo.OportunidadMejora;
                                var OportunidadMejoraT = _mapper.Map<DatosHallazgoDto,TDatosHallazgo>(hallasgoDto.OportunidadMejora);
                                OportunidadMejoraT.CodHallazgo = CodigoHallazgo;
                                OportunidadMejoraT.CodTipoHallazgo = TipoHallazgo.OportunidadMejora;
                                OportunidadMejoraT.Correlativo = nextCorrelativo;
                                nextCorrelativo = nextCorrelativoHallazgo(nextCorrelativo);
                                _context.TDatosHallazgo.Add(OportunidadMejoraT);
                            }
                            if(hallasgoDto.RequiereCorreccion!=null){
                                hallasgoT.CodTipoHallazgo = TipoHallazgo.RequiereCorreccion;
                                var RequiereCorreccionT = _mapper.Map<DatosHallazgoDto,TDatosHallazgo>(hallasgoDto.RequiereCorreccion);
                                RequiereCorreccionT.CodHallazgo = CodigoHallazgo;
                                RequiereCorreccionT.CodTipoHallazgo = TipoHallazgo.RequiereCorreccion;
                                RequiereCorreccionT.Correlativo = nextCorrelativo;
                                nextCorrelativo = nextCorrelativoHallazgo(nextCorrelativo);
                                _context.TDatosHallazgo.Add(RequiereCorreccionT);
                            }

                            if(hallasgoDto.planAccion != null)
                                foreach(var plan in hallasgoDto.planAccion){
                                    plan.docReferencia = nuevoCodigo;
                                    plan.docSubReferencia = CodigoHallazgo;
                                    list_plan_accion.Add(plan);
                                }
                        }
                        
                        //control critico
                        TAudCartilla AudCC = null;
                        if(request.data.ControlCritico!=null){
                            //malabares del modelado :V 
                            AudCC = _mapper.Map<ControlCriticoDto,TAudCartilla>(request.data.ControlCritico);
                            AudCC.CodAuditoria = nuevoCodigo;
                            AudCC.TAudCCCriterio = _mapper.Map<ICollection<CCCriteriosDto>,ICollection<TAudCCCriterio>>(request.data.ControlCritico.Criterios);
                            _context.TAudCartilla.Add(AudCC);
                        }
                        
                        nuevo.CodAuditoria = nuevoCodigo;
                        
                        _context.TAuditoria.Add(nuevo);
                        var ii = await _context.SaveChangesAsync(cancellationToken);

                    //var pa = await _planAccion.Create(list_plan_accion,"");
                    var pa = await _mediator.Send(new CreatePlanAccionCommand() { planes = list_plan_accion });

                    return nuevo.CodAuditoria;
                    // }
                }
                catch(Exception e){
                    var ee = e;
                    throw e;
                }
                finally {
                    // Monitor.Exit(this);
                }
            }

            public string nextCod()
            {
                var COD_MAX = _context.TAuditoria.Max(t => t.CodAuditoria);
                if (COD_MAX == null) COD_MAX = "AUD0000000001";
                else
                {
                    string numberStr = COD_MAX.Substring(3);
                    int max = Int32.Parse(numberStr) + 1;
                    COD_MAX = String.Format("AUD{0,10:0000000000}", max);
                }
                return COD_MAX;
            }
            public string nextCodHallazgo(string COD_MAX = null)
            {
                if(COD_MAX == null) COD_MAX = _context.THallazgos.Max(t => t.CodHallazgo);
                if (COD_MAX == null) COD_MAX = "HAL0000000001";
                else
                {
                    string numberStr = COD_MAX.Substring(3);
                    int max = Int32.Parse(numberStr) + 1;
                    COD_MAX = String.Format("HAL{0,10:0000000000}", max);
                }
                return COD_MAX;
            }
            public int nextCorrelativoHallazgo(int COD_MAX = -1)
            {
                try{
                    if(COD_MAX == -1) COD_MAX = _context.TDatosHallazgo.Max(t => t.Correlativo);
                    return COD_MAX+1;
                }
                catch(Exception e){
                    var size = _context.TDatosHallazgo.Count();
                    if(size == 0) return 0;
                    else throw e;
                }
                
            }
            
        }

    }

}