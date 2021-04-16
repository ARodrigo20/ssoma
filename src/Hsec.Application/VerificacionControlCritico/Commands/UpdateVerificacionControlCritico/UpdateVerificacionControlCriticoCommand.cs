using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Common.Mappings;
using Hsec.Application.Common.Models;
using Hsec.Domain.Entities.VerficacionesCc;
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
using Hsec.Application.Files.Queries.GetFilesUpload;
using Hsec.Application.Files.Commands.DeleteFiles;
using Hsec.Application.Files.Commands.UpdateFiles;
using Hsec.Application.PlanAccion.Commands.DeletePlanDeAccion;
using Hsec.Application.PlanAccion.Commands.UpdatePlanDeAccion;
using Hsec.Application.Files.Commands.CreateFiles;
using Hsec.Application.Files.Queries.GetFilesUpload;

namespace Hsec.Application.VerificacionControlCritico.Commands.UpdateVerificacionControlCritico
{
    public class UpdateVerificacionControlCriticoCommand : IRequest<Unit>
    {
        public VerificacionControlCriticoDto verConCrit {get;set;}
        public IFormFileCollection Files { get; set; }
        public List<PlanVM> planAccion { get; set; }
        public List<FilesUploadOneVM> UpdateFiles { get; set; }
        public class UpdateVerificacionControlCriticoCommandHandler : IRequestHandler<UpdateVerificacionControlCriticoCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;          

            public UpdateVerificacionControlCriticoCommandHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<Unit> Handle(UpdateVerificacionControlCriticoCommand request, CancellationToken cancellationToken)
            {
                try{
                    if(request.verConCrit==null) throw new Exception("falta Verificacion Control Cricito");
                    var CODIGO = request.verConCrit.CodigoVCC;
                    var data = _context.TVerificacionControlCritico.Where(t => t.Estado == true && t.CodigoVCC.Equals(CODIGO)).FirstOrDefault();
                    if(data==null) throw new Exception("error no existe el dato "+CODIGO);
                    data = _mapper.Map<VerificacionControlCriticoDto,TVerificacionControlCritico>(request.verConCrit,data);
                    _context.TVerificacionControlCritico.Update(data);

                    if(request.verConCrit.Criterios != null){
                        var list_data = request.verConCrit.Criterios;
                        foreach(var item in list_data){
                            var updateItem = _context.TVerificacionControlCriticoCartilla
                                .Where(t => t.CodigoVCC.Equals(item.CodigoVCC) 
                                    && t.CodCartilla.Equals(item.CodCartilla) 
                                    && t.CodCC.Equals(item.CodCC) ).FirstOrDefault();
                            if(updateItem == null){
                                updateItem = _mapper.Map<CriterioEvaluacionDto,TVerificacionControlCriticoCartilla>(item);
                                _context.TVerificacionControlCriticoCartilla.Add(updateItem);
                            }
                            else{
                                updateItem = _mapper.Map<CriterioEvaluacionDto,TVerificacionControlCriticoCartilla>(item,updateItem);
                                updateItem.Estado = true;
                                _context.TVerificacionControlCriticoCartilla.Update(updateItem);
                            }
                        }
                    }
                    await _context.SaveChangesAsync(cancellationToken);
                    
                    
                    foreach (var item in request.UpdateFiles)
                    {
                        if(item.Estado ==false)
                        {
                            //var r1 = await _imagenes.Delete(item.CorrelativoArchivos.ToString());
                            var r1 = await _mediator.Send(new DeleteFileCommand() { CorrelativoArchivos = item.CorrelativoArchivos });
                        }
                        else if(item.Estado == true && item.CorrelativoArchivos != 0)
                        {
                            //var r2 = await _imagenes.Update(item);
                            var r2 = await _mediator.Send(new UpdateFileCommand() { CorrelativoArchivos = item.CorrelativoArchivos, Descripcion = item.Descripcion });
                        }
                    }

                    var updateList = new List<PlanVM>();
                    var createList = new List<PlanVM>();
                    foreach(var item in request.verConCrit.planAccion)
                    {
                        if(item.estado == false && item.codAccion > 0)
                        {
                            //var r3 = await _planAccion.Delete(item.codAccion.ToString());
                            var r3 = await _mediator.Send(new DeletePlanAccionCommand() { CodAccion = item.codAccion });
                        }
                        else if(item.estado == true && item.codAccion > 0)
                        {
                            updateList.Add(item);
                        }
                        else if(item.codAccion < 0)
                        {
                            createList.Add(item);
                        }
                    }

                    //var r4 = await _planAccion.Create(createList,CODIGO);
                    createList.ForEach(t => { t.docReferencia = CODIGO; t.docSubReferencia = CODIGO; });
                    var r4 = await _mediator.Send(new CreatePlanAccionCommand() { planes = createList });

                    //var r5 = await _planAccion.Update(updateList);
                    var r5 = await _mediator.Send(new UpdatePlanAccionCommand() { planes = updateList });

                    //var r6 = await _imagenes.Upload(request.Files, CODIGO,"TVerCC");
                    var r6 = await _mediator.Send(new CreateListFilesCommand { File = request.Files, NroDocReferencia = CODIGO, NroSubDocReferencia = CODIGO, CodTablaRef = "TVerCC" });

                    foreach (var item in request.UpdateFiles)
                    {
                        //await _imagenes.Update(item);
                        var num2 = await _mediator.Send(new UpdateFileCommand() { CorrelativoArchivos = item.CorrelativoArchivos, Descripcion = item.Descripcion });
                    }

                    return Unit.Value;
                }
                catch(Exception ex){
                    throw ex;
                }
            }
            
            
        }
    }
}