using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Common.Mappings;
using Hsec.Application.Verificaciones.Models;
using Hsec.Domain.Entities.Verficaciones;
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

namespace Hsec.Application.Verificaciones.Commands.UpdateVerificacion
{

    public class UpdateVerificacionCommand : IRequest<int>
    {
        public VerificacionDto data { get; set; }
        public List<PlanVM> planAccion { get; set; }
        public IFormFileCollection newFiles { get; set; }
        public List<FilesUploadOneVM> updateFiles { get; set; }
        public class UpdateVerificacionCommandHandler : IRequestHandler<UpdateVerificacionCommand, int>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public UpdateVerificacionCommandHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            private void deleteLogicoIPERC(string CODIGO_VERIFICACION){
                var iperc = _context.TVerificacionIPERC.Where(t => t.CodVerificacion.Equals(CODIGO_VERIFICACION)).FirstOrDefault();
                if(iperc!=null){
                    iperc.Estado = false;
                    _context.TVerificacionIPERC.Update(iperc);
                }
            }
            private void deleteLogicoPTAR(string CODIGO_VERIFICACION){
                var ptar = _context.TVerificacionPTAR.Where(t => t.CodVerificacion.Equals(CODIGO_VERIFICACION)).FirstOrDefault();
                if(ptar!=null){
                    ptar.Estado = false;
                    _context.TVerificacionPTAR.Update(ptar);
                }
            }
            public async Task<int> Handle(UpdateVerificacionCommand request, CancellationToken cancellationToken)
            {
                
                var CODIGO_VERIFICACION = request.data.CodVerificacion;

                var oldVerificacion = _context.TVerificaciones
                .Where(t => t.Estado == true && t.CodVerificacion.Equals(CODIGO_VERIFICACION))
                .FirstOrDefault();

                oldVerificacion = _mapper.Map<VerificacionDto,TVerificaciones>(request.data,oldVerificacion);
                
                _context.TVerificaciones.Update(oldVerificacion);

                if(request.data.CodTipoVerificacion.Equals(TipoVerificacion.IPERC_Continuo)){
                    
                    var iperc = _context.TVerificacionIPERC.Where(t => t.CodVerificacion.Equals(CODIGO_VERIFICACION)).FirstOrDefault();
                    if(iperc==null){
                        iperc = _mapper.Map<VerificacionIPERCDto,TVerificacionIPERC>(request.data.VerificacionIPERC);
                        deleteLogicoPTAR(CODIGO_VERIFICACION);
                        _context.TVerificacionIPERC.Add(iperc);
                    }
                    else{
                        iperc = _mapper.Map<VerificacionIPERCDto,TVerificacionIPERC>(request.data.VerificacionIPERC,iperc);
                        iperc.Estado = true;
                        _context.TVerificacionIPERC.Update(iperc);
                    }
                }
                else if(request.data.CodTipoVerificacion.Equals(TipoVerificacion.PTAR)){
                    var ptar = _context.TVerificacionPTAR.Where(t => t.CodVerificacion.Equals(CODIGO_VERIFICACION)).FirstOrDefault();
                    if(ptar==null){
                        ptar = _mapper.Map<VerificacionPTARDto,TVerificacionPTAR>(request.data.VerificacionPTAR);
                        deleteLogicoIPERC(CODIGO_VERIFICACION);
                        _context.TVerificacionPTAR.Add(ptar);
                    }
                    else{
                        ptar = _mapper.Map<VerificacionPTARDto,TVerificacionPTAR>(request.data.VerificacionPTAR,ptar);
                        _context.TVerificacionPTAR.Update(ptar);
                    }
                }

                foreach (var item in request.updateFiles)
                {
                    if(item.Estado ==false)
                    {
                        //var r1 = await _imagen.Delete(item.CorrelativoArchivos.ToString());
                        var r1 = await _mediator.Send(new DeleteFileCommand() { CorrelativoArchivos = item.CorrelativoArchivos });
                    }
                    else if(item.Estado == true && item.CorrelativoArchivos != 0)
                    {
                        //var r2 = await _imagen.Update(item);
                        var r2 = await _mediator.Send(new UpdateFileCommand() { CorrelativoArchivos = item.CorrelativoArchivos, Descripcion = item.Descripcion });
                    }
                }

                var updateList = new List<PlanVM>();
                var createList = new List<PlanVM>();
                foreach(var item in request.planAccion)
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
                var r7 = await _context.SaveChangesAsync(cancellationToken);
                //var r4 = await _planAccion.Create(createList,request.data.CodVerificacion);
                createList.ForEach(t => { t.docReferencia = request.data.CodVerificacion; t.docSubReferencia = request.data.CodVerificacion; });
                var r4 = await _mediator.Send(new CreatePlanAccionCommand() { planes = createList });

                //var r5 = await _planAccion.Update(updateList);
                var r5 = await _mediator.Send(new UpdatePlanAccionCommand() { planes = updateList });

                //var r6 = await _imagen.Upload(request.newFiles, request.data.CodVerificacion,"TVer");
                var r6 = await _mediator.Send(new CreateListFilesCommand { File = request.newFiles, NroDocReferencia = request.data.CodVerificacion, NroSubDocReferencia = request.data.CodVerificacion, CodTablaRef = "TVer" });
                return 0;
            }
        }
    }
}