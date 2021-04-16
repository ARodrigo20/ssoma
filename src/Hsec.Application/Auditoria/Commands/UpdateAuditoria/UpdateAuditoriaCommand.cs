using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Common.Mappings;
using Hsec.Application.Auditoria.Models;
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
using Hsec.Application.Files.Queries.GetFilesUpload;
using Hsec.Application.PlanAccion.Commands.CreatePlanDeAccion;
using Hsec.Application.PlanAccion.Commands.DeletePlanDeAccion;
using Hsec.Application.PlanAccion.Commands.UpdatePlanDeAccion;

namespace Hsec.Application.Auditoria.Commands.UpdateAuditoria
{

    public class UpdateAuditoriaCommand : IRequest<Unit>
    {
        public AuditoriaDto data { get; set; }
        // public List<PlanAccionVM> planAccion { get; set; }
        public IFormFileCollection newFiles { get; set; }
        public List<FilesUploadOneVM> updateFiles { get; set; }
        public class UpdateAuditoriaCommandHandler : IRequestHandler<UpdateAuditoriaCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public UpdateAuditoriaCommandHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<Unit> Handle(UpdateAuditoriaCommand request, CancellationToken cancellationToken)
            {
                try{
                    var COD_AUDITORIA = request.data.CodAuditoria;
                    var forViewData = request.data;

                    List<PlanVM> list_plan_accion = new List<PlanVM>();
                    List<PlanVM> list_plan_accion_update = new List<PlanVM>();

                    var data = _context.TAuditoria
                        // .Include(t => t.TEquipoAuditor)
                        .Where(t => t.Estado == true && t.CodAuditoria.Equals(COD_AUDITORIA))
                        .Include(t => t.TEquipoAuditor)
                        .FirstOrDefault();


                    if(data == null) throw new NotFoundException("Auditoria",COD_AUDITORIA);

                    //update general
                    data = _mapper.Map<AuditoriaDto,TAuditoria>(forViewData,data);
                    
                    //update grupo personas
                    
                    data.TEquipoAuditor = _mapper.Map<ICollection<EquipoAuditorDto>,ICollection<TEquipoAuditor>>(forViewData.Equipo);
                    
                    string CodigoHallazgoNew = null;

                    List<string> list_delete_hallazgos = _context.THallazgos.Where(t => t.CodAuditoria.Equals(COD_AUDITORIA)).Select(t => t.CodHallazgo).ToList();

                    foreach(var hallasgoDto in forViewData.Hallazgos){
                        if(string.IsNullOrEmpty(hallasgoDto.CodHallazgo)){
                            var hallasgoT = _mapper.Map<HallazgoDto,THallazgos>(hallasgoDto);
                            
                            CodigoHallazgoNew = nextCodHallazgo(CodigoHallazgoNew);
                            hallasgoT.CodHallazgo = CodigoHallazgoNew;
                            data.THallazgos.Add(hallasgoT);

                            var nextCorrelativo = nextCorrelativoHallazgo();
                            if(hallasgoDto.NoConformidad!=null){
                                hallasgoT.CodTipoHallazgo = TipoHallazgo.NoConformidad;
                                var AnalisisHallasgo = _mapper.Map<NoConformidadDto,TAnalisisHallazgo>(hallasgoDto.NoConformidad);
                                AnalisisHallasgo.CodHallazgo = CodigoHallazgoNew;
                                AnalisisHallasgo.CodTipoHallazgo = TipoHallazgo.NoConformidad;
                                _context.TAnalisisHallazgo.Add(AnalisisHallasgo);
                                if(hallasgoDto.NoConformidad.AnalisisCausalidad!=null)
                                    foreach(var analsisCausalidadDto in hallasgoDto.NoConformidad.AnalisisCausalidad){
                                        var TAnalisisCausalidad = _mapper.Map<AnalisisCausalidadDto,TAuditoriaAnalisisCausalidad>(analsisCausalidadDto);
                                        TAnalisisCausalidad.CodHallazgo = CodigoHallazgoNew;
                                        _context.TAuditoriaAnalisisCausalidad.Add(TAnalisisCausalidad);
                                    }
                                
                            }
                            if(hallasgoDto.Observacion!=null){
                                hallasgoT.CodTipoHallazgo = TipoHallazgo.Observacion;
                                var ObservacionT = _mapper.Map<DatosHallazgoDto,TDatosHallazgo>(hallasgoDto.Observacion);
                                ObservacionT.CodHallazgo = CodigoHallazgoNew;
                                ObservacionT.CodTipoHallazgo = TipoHallazgo.Observacion;
                                ObservacionT.Correlativo = nextCorrelativo; 
                                nextCorrelativo = nextCorrelativoHallazgo(nextCorrelativo);
                                _context.TDatosHallazgo.Add(ObservacionT);
                            }
                            if(hallasgoDto.OportunidadMejora!=null){
                                hallasgoT.CodTipoHallazgo = TipoHallazgo.OportunidadMejora;
                                var OportunidadMejoraT = _mapper.Map<DatosHallazgoDto,TDatosHallazgo>(hallasgoDto.OportunidadMejora);
                                OportunidadMejoraT.CodHallazgo = CodigoHallazgoNew;
                                OportunidadMejoraT.CodTipoHallazgo = TipoHallazgo.OportunidadMejora;
                                OportunidadMejoraT.Correlativo = nextCorrelativo;
                                nextCorrelativo = nextCorrelativoHallazgo(nextCorrelativo);
                                _context.TDatosHallazgo.Add(OportunidadMejoraT);
                            }
                            if(hallasgoDto.RequiereCorreccion!=null){
                                hallasgoT.CodTipoHallazgo = TipoHallazgo.RequiereCorreccion;
                                var RequiereCorreccionT = _mapper.Map<DatosHallazgoDto,TDatosHallazgo>(hallasgoDto.RequiereCorreccion);
                                RequiereCorreccionT.CodHallazgo = CodigoHallazgoNew;
                                RequiereCorreccionT.CodTipoHallazgo = TipoHallazgo.RequiereCorreccion;
                                RequiereCorreccionT.Correlativo = nextCorrelativo;
                                nextCorrelativo = nextCorrelativoHallazgo(nextCorrelativo);
                                _context.TDatosHallazgo.Add(RequiereCorreccionT);
                            }
                            if(hallasgoDto.planAccion!=null)
                                foreach(var plan in hallasgoDto.planAccion){
                                    plan.docSubReferencia = plan.docReferencia = CodigoHallazgoNew;
                                    list_plan_accion.Add(plan);
                                }

                            // await _context.SaveChangesAsync(cancellationToken);
                        }
                        else{

                            list_delete_hallazgos.Remove(hallasgoDto.CodHallazgo);

                            var hallazgo = _context.THallazgos
                                .Where(t => t.Estado == true && t.CodHallazgo.Equals(hallasgoDto.CodHallazgo))
                                .FirstOrDefault();
                            hallazgo = _mapper.Map<HallazgoDto,THallazgos>(hallasgoDto,hallazgo);
                            _context.THallazgos.Update(hallazgo);

                            var CodigoHallazgo = hallasgoDto.CodHallazgo;
                            var nextCorrelativo = nextCorrelativoHallazgo();
                            var Correlativo = 0;
                            if(hallasgoDto.NoConformidad!=null){
                                
                                var updateHallazgo = _context.TAnalisisHallazgo.Where(t => t.CodHallazgo.Equals(CodigoHallazgo)).FirstOrDefault();

                                updateHallazgo = _mapper.Map<NoConformidadDto,TAnalisisHallazgo>(hallasgoDto.NoConformidad,updateHallazgo);
                                
                                _context.TAnalisisHallazgo.Update(updateHallazgo);
                                
                                if(hallasgoDto.NoConformidad.AnalisisCausalidad!=null)
                                    foreach(var analsisCausalidadDto in hallasgoDto.NoConformidad.AnalisisCausalidad){
                                        TAuditoriaAnalisisCausalidad TAnalisisCausalidad = _context.TAuditoriaAnalisisCausalidad
                                            .Where(t => 
                                                t.CodAnalisis.Equals(analsisCausalidadDto.CodAnalisisCausa) && 
                                                t.CodHallazgo.Equals(analsisCausalidadDto.CodHallazgo) &&
                                                t.CodCausa.Equals(analsisCausalidadDto.CodCausa) &&
                                                t.CodCondicion.Equals(analsisCausalidadDto.CodCondicion))
                                            .FirstOrDefault();
                                        
                                        if(TAnalisisCausalidad == null){
                                            TAnalisisCausalidad = _mapper.Map<AnalisisCausalidadDto,TAuditoriaAnalisisCausalidad>(analsisCausalidadDto);
                                            TAnalisisCausalidad.CodHallazgo = CodigoHallazgo;
                                            _context.TAuditoriaAnalisisCausalidad.Add(TAnalisisCausalidad);
                                        }
                                        else{
                                            TAnalisisCausalidad = _mapper.Map<AnalisisCausalidadDto,TAuditoriaAnalisisCausalidad>(analsisCausalidadDto,TAnalisisCausalidad);
                                            _context.TAuditoriaAnalisisCausalidad.Update(TAnalisisCausalidad);
                                        }
                                    }
                            }
                            if(hallasgoDto.Observacion!=null){
                                var updateHallazgo = _context.TDatosHallazgo.Where(t => t.CodHallazgo.Equals(CodigoHallazgo)).FirstOrDefault();
                                updateHallazgo = _mapper.Map<DatosHallazgoDto,TDatosHallazgo>(hallasgoDto.Observacion,updateHallazgo);
                                _context.TDatosHallazgo.Update(updateHallazgo);
                            }
                            if(hallasgoDto.OportunidadMejora!=null){
                                var updateHallazgo = _context.TDatosHallazgo.Where(t => t.CodHallazgo.Equals(CodigoHallazgo)).FirstOrDefault();
                                updateHallazgo = _mapper.Map<DatosHallazgoDto,TDatosHallazgo>(hallasgoDto.OportunidadMejora,updateHallazgo);
                                _context.TDatosHallazgo.Update(updateHallazgo);
                            }
                            if(hallasgoDto.RequiereCorreccion!=null){
                                var updateHallazgo = _context.TDatosHallazgo.Where(t => t.CodHallazgo.Equals(CodigoHallazgo)).FirstOrDefault();
                                updateHallazgo = _mapper.Map<DatosHallazgoDto,TDatosHallazgo>(hallasgoDto.RequiereCorreccion,updateHallazgo);
                                _context.TDatosHallazgo.Update(updateHallazgo);
                            }                            
                            if(hallasgoDto.planAccion!=null){
                                foreach(var plan in hallasgoDto.planAccion){

                                    if (plan.estado == false && plan.codAccion > 0) {
                                        //var r3 = await _planAccion.Delete (plan.codAccion.ToString ());
                                        var r3 = await _mediator.Send(new DeletePlanAccionCommand() { CodAccion = plan.codAccion });
                                    } else if (plan.estado == true && plan.codAccion > 0) {
                                        list_plan_accion_update.Add (plan);
                                    } else if (plan.codAccion < 0) {
                                        plan.docReferencia = COD_AUDITORIA;
                                        plan.docSubReferencia = CodigoHallazgo;
                                        list_plan_accion.Add (plan);
                                    }

                                }
                            }
                        }
                    }


                    foreach(var codItem in list_delete_hallazgos){
                        var itemHallazgo = _context.THallazgos.Where(t => t.CodHallazgo.Equals(codItem)).FirstOrDefault();
                        if(itemHallazgo != null){
                            itemHallazgo.Estado = false;
                            _context.THallazgos.Update(itemHallazgo);

                            if(itemHallazgo.CodTipoHallazgo == TipoHallazgo.NoConformidad){
                                var item = _context.TAnalisisHallazgo.Where(t => t.Estado == true && t.CodTipoHallazgo.Equals(itemHallazgo.CodTipoHallazgo)).FirstOrDefault();
                                item.Estado = false;
                                var listAnaCausalidad = _context.TAuditoriaAnalisisCausalidad
                                    .Where(t => t.Estado == true && t.CodHallazgo.Equals(item.CodHallazgo))
                                    .ToHashSet();
                                foreach(var itemAnaCausalidad in listAnaCausalidad){
                                    itemAnaCausalidad.Estado = false;
                                    _context.TAuditoriaAnalisisCausalidad.Update(itemAnaCausalidad);
                                }
                                _context.TAnalisisHallazgo.Update(item);
                            }
                            if(itemHallazgo.CodTipoHallazgo == TipoHallazgo.Observacion){
                                var item = _context.TDatosHallazgo.Where(t => t.Estado == true && t.CodTipoHallazgo.Equals(itemHallazgo.CodTipoHallazgo)).FirstOrDefault();
                                item.Estado = false;
                                _context.TDatosHallazgo.Update(item);
                            }
                            if(itemHallazgo.CodTipoHallazgo == TipoHallazgo.OportunidadMejora){
                                var item = _context.TDatosHallazgo.Where(t => t.Estado == true && t.CodTipoHallazgo.Equals(itemHallazgo.CodTipoHallazgo)).FirstOrDefault();
                                item.Estado = false;
                                _context.TDatosHallazgo.Update(item);
                            }
                            if(itemHallazgo.CodTipoHallazgo == TipoHallazgo.RequiereCorreccion){
                                var item = _context.TDatosHallazgo.Where(t => t.Estado == true && t.CodTipoHallazgo.Equals(itemHallazgo.CodTipoHallazgo)).FirstOrDefault();
                                item.Estado = false;
                                _context.TDatosHallazgo.Update(item);
                            }       
                            //await _planAccion.Remove(list_plan_accion_update)
                        }
                    }

                    _context.TAuditoria.Update(data);
                    await _context.SaveChangesAsync(cancellationToken);

                    //await _planAccion.Create(list_plan_accion,"");
                    var rp = await _mediator.Send(new CreatePlanAccionCommand() { planes = list_plan_accion });

                    //await _planAccion.Update(list_plan_accion_update);
                    var ru = await _mediator.Send(new UpdatePlanAccionCommand() { planes = list_plan_accion_update });

                    return Unit.Value;
                }catch(Exception e){
                    var ee = e;
                    throw e;
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
                if(COD_MAX == -1) COD_MAX = _context.TDatosHallazgo.Max(t => t.Correlativo);
                return COD_MAX+1;
            }
        }
    }
}