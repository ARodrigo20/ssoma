using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Auditoria;
using Hsec.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.Files.Commands.DeleteFileDocRef;
using Hsec.Application.PlanAccion.Commands.DeleteDocRefHsec;

namespace Hsec.Application.Incidentes.Commands.DeleteIncidente
{

    public class DeleteAuditoriaCommand : IRequest<int>
    {        
        public string Codigo { get; set; }
        public class DeleteAuditoriaCommandHandler : IRequestHandler<DeleteAuditoriaCommand, int>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public DeleteAuditoriaCommandHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<int> Handle(DeleteAuditoriaCommand request, CancellationToken cancellationToken)
            {
                string COD_AUDITORIA = request.Codigo;

                var TAuditoria = _context.TAuditoria.Where(t => t.Estado == true && t.CodAuditoria.Equals(request.Codigo)).FirstOrDefault();

                IList<string> list_codigo_hallazgo = new List<string>();

                if(TAuditoria == null) throw new NotFoundException("Auditoria",COD_AUDITORIA);
                
                TAuditoria.Estado = false;
                _context.TAuditoria.Update(TAuditoria);

                

                var listEquipo = _context.TEquipoAuditor.Where(t => t.Estado == true && t.CodAuditoria.Equals(COD_AUDITORIA));
                foreach(var itemEquipo in listEquipo){
                    itemEquipo.Estado = false;
                }
                _context.TEquipoAuditor.UpdateRange(listEquipo);
                
                var listHallasgo = _context.THallazgos
                        .Where(t => t.Estado == true && t.CodAuditoria.Equals(COD_AUDITORIA))
                        .ToHashSet();
                
                foreach(var itemHallazgo in listHallasgo){
                    itemHallazgo.Estado = false;
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

                    list_codigo_hallazgo.Add(itemHallazgo.CodHallazgo);

                    TAudCartilla AudCC = _context.TAudCartilla.Where(t => t.Estado == true && t.CodAuditoria.Equals(COD_AUDITORIA)).FirstOrDefault();
                    if(AudCC!=null){
                        AudCC.Estado = true;
                        _context.TAudCartilla.Update(AudCC);
                        var listCCCriterios = _context.TAudCCCriterio
                            .Where(t => t.Estado == true && t.CodAuditoria.Equals(COD_AUDITORIA) && t.CodCartilla.Equals(AudCC.CodCartilla))
                            .ToHashSet();
                        foreach(var itemCCCriterios in listCCCriterios){
                            itemCCCriterios.Estado = false;
                            _context.TAudCCCriterio.Update(itemCCCriterios);
                        }
                    }
                }

                //var ri = _imagen.Delete(COD_AUDITORIA);
                var ri = await _mediator.Send(new DeleteFileDocRefCommand() { NroDocReferencia = COD_AUDITORIA });
                //foreach (var itemCodigoHallazgo in  list_codigo_hallazgo){
                //    var te = _planAccion.Delete(itemCodigoHallazgo);
                //}
                var te = await _mediator.Send(new DeleteDocRefCommand() { NroDocReferencia = COD_AUDITORIA });

                return await _context.SaveChangesAsync(cancellationToken);
            }
        }

    }
}