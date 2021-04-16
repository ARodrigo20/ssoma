using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Common.Mappings;
using Hsec.Application.Verificaciones.Models;
using Hsec.Domain.Common;
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
using Hsec.Application.Files.Commands.CreateFiles;

namespace Hsec.Application.Verificaciones.Commands.CreateVerifiacion
{

    public class CreateVerificacionCommand : IRequest<string>
    {
        public VerificacionDto data { get; set; }
        public List<PlanVM> planAccion { get; set; }
        public IFormFileCollection Files { get; set; }

        public class CreateVerificacionCommandHandler : IRequestHandler<CreateVerificacionCommand, string>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public CreateVerificacionCommandHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<string> Handle(CreateVerificacionCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var nuevoVerificaciones = _mapper.Map<VerificacionDto,TVerificaciones>(request.data);

                    nuevoVerificaciones.CodVerificacion = nextCod();

                    _context.TVerificaciones.Add(nuevoVerificaciones);
                    if(nuevoVerificaciones.CodTipoVerificacion.Equals(TipoVerificacion.IPERC_Continuo)){

                        var nuevoIPERC = _mapper.Map<VerificacionIPERCDto,TVerificacionIPERC>(request.data.VerificacionIPERC);
                        if(nuevoIPERC!=null){
                            nuevoIPERC.CodVerificacion = nuevoVerificaciones.CodVerificacion;
                            _context.TVerificacionIPERC.Add(nuevoIPERC);
                        }
                    }
                    else if(nuevoVerificaciones.CodTipoVerificacion.Equals(TipoVerificacion.PTAR)){
                        var nuevoPTAR = _mapper.Map<VerificacionPTARDto,TVerificacionPTAR>(request.data.VerificacionPTAR);
                        if(nuevoPTAR!=null){
                            nuevoPTAR.CodVerificacion = nuevoVerificaciones.CodVerificacion;
                            _context.TVerificacionPTAR.Add(nuevoPTAR);
                        }
                    }

                    var r1 = await _context.SaveChangesAsync(cancellationToken);

                    //var r2 = await _imagenes.Upload(request.Files, nuevoVerificaciones.CodVerificacion,"TVer");
                    var r2 = await _mediator.Send(new CreateListFilesCommand { File = request.Files, NroDocReferencia = nuevoVerificaciones.CodVerificacion, NroSubDocReferencia = nuevoVerificaciones.CodVerificacion, CodTablaRef = "TVer" });


                    request.planAccion.ForEach(t => { t.docReferencia = nuevoVerificaciones.CodVerificacion; t.docSubReferencia = nuevoVerificaciones.CodVerificacion; });
                    //var r3 = await _planAccion.Create(request.planAccion, nuevoVerificaciones.CodVerificacion);
                    var r3 = await _mediator.Send(new CreatePlanAccionCommand() { planes = request.planAccion });

                    return nuevoVerificaciones.CodVerificacion;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }


            public string nextCod()
            {
                var COD_Verificacion_MAX = _context.TVerificaciones.Max(t => t.CodVerificacion);

                if (COD_Verificacion_MAX == null) COD_Verificacion_MAX = "VER00000001";
                else
                {
                    string numberStr = COD_Verificacion_MAX.Substring(3);
                    int max = Int32.Parse(numberStr) + 1;
                    COD_Verificacion_MAX = String.Format("VER{0,8:00000000}", max);
                }
                return COD_Verificacion_MAX;
            }
            
        }

    }

}