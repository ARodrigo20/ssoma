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
using Hsec.Application.Files.Commands.CreateFiles;

namespace Hsec.Application.VerificacionControlCritico.Commands.CreateVerificacionControlCritico
{
    public class CreateVerificacionControlCriticoCommand : IRequest<string>
    {
        public VerificacionControlCriticoDto verConCrit {get;set;}
        public IFormFileCollection Files { get; set; }
        // public List<PlanAccionVM> planAccion { get; set; }
        public class CreateVerificacionControlCriticoCommandHandler : IRequestHandler<CreateVerificacionControlCriticoCommand, string>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;            

            public CreateVerificacionControlCriticoCommandHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<string> Handle(CreateVerificacionControlCriticoCommand request, CancellationToken cancellationToken)
            {
                try{
                    if(request.verConCrit==null) throw new Exception("falta Verificacion Control Cricito");
                    var nuevo = _mapper.Map<VerificacionControlCriticoDto,TVerificacionControlCritico>(request.verConCrit);
                    nuevo.CodigoVCC = nextCod();
                    _context.TVerificacionControlCritico.Add(nuevo);

                    if(request.verConCrit.Criterios != null){
                        var nuevo_Detalle_List = _mapper.Map<IList<CriterioEvaluacionDto>,IList<TVerificacionControlCriticoCartilla>>(request.verConCrit.Criterios);
                        foreach(var item in nuevo_Detalle_List){
                            item.CodigoVCC = nuevo.CodigoVCC;
                            item.CodCartilla = nuevo.Cartilla;
                        }
                        _context.TVerificacionControlCriticoCartilla.AddRange(nuevo_Detalle_List);
                    }

                    var r1 = await _context.SaveChangesAsync(cancellationToken);

                    //var r2 = await _imagenes.Upload(request.Files, nuevo.CodigoVCC,"TVerCC");
                    var r2 = await _mediator.Send(new CreateListFilesCommand { File = request.Files, NroDocReferencia = nuevo.CodigoVCC, NroSubDocReferencia = nuevo.CodigoVCC, CodTablaRef = "TVerCC" });

                    //var r3 = await _planAccion.Create(request.verConCrit.PlanAccion.ToList(), nuevo.CodigoVCC);
                    var createList = request.verConCrit.PlanAccion.ToList();
                    createList.ForEach(t => { t.docReferencia = nuevo.CodigoVCC; t.docSubReferencia = nuevo.CodigoVCC; });
                    var r3 = await _mediator.Send(new CreatePlanAccionCommand() { planes = createList });

                    return nuevo.CodigoVCC;
                }
                catch(Exception ex){
                    throw ex;
                }
            }

            public string nextCod()
            {
                var COD_Verificacion_MAX = _context.TVerificacionControlCritico.Max(t => t.CodigoVCC);

                if (COD_Verificacion_MAX == null) COD_Verificacion_MAX = "VCC00000001";
                else
                {
                    string numberStr = COD_Verificacion_MAX.Substring(3);
                    int max = Int32.Parse(numberStr) + 1;
                    COD_Verificacion_MAX = String.Format("VCC{0,8:00000000}", max);
                }
                return COD_Verificacion_MAX;
            }

            
        }
    }
}