using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.General;
using Hsec.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.PlanAnualVerifiacionCC.Commands.UpdatePlanAnual
{
    public class UpdatePlanAnualGeneralCommand : IRequest<Unit>
    {
        public UpdatePlanAnualGeneralVM data { get; set; }
        public class CreatePlanAnualGeneralCommandHandler : IRequestHandler<UpdatePlanAnualGeneralCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public CreatePlanAnualGeneralCommandHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(UpdatePlanAnualGeneralCommand request, CancellationToken cancellationToken)
            {
                var VM = request.data;
                foreach (var persona in VM.ListPersonas)
                {
                    foreach (var codigo in persona.ListCodigos)
                    {
                        int iterMes = Int32.Parse(VM.Mes);
                        do
                        {
                            var upsert = _context.TPlanAnualVerConCri.Find(VM.Anio,iterMes.ToString(),persona.CodPersona,codigo.CodReferencia);
                            if(upsert == null){
                                upsert = new TPlanAnualVerConCri(){
                                    Anio = VM.Anio,
                                    CodMes = iterMes.ToString(),
                                    CodPersona = persona.CodPersona,
                                    CodReferencia = codigo.CodReferencia,
                                    Valor = codigo.Valor
                                };
                                _context.TPlanAnualVerConCri.Add(upsert);
                            }
                            else{
                                if(Int32.Parse(codigo.Valor)>0){
                                    upsert.Valor = codigo.Valor;
                                    _context.TPlanAnualVerConCri.Update(upsert);
                                }
                                else{
                                    _context.TPlanAnualVerConCri.Remove(upsert);
                                }
                            }
                            iterMes = iterMes + 1;
                        } while (VM.Replicar && iterMes<=12);
                    }
                }
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}