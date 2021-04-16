using AutoMapper;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.General.ControlCritico.Models;
using Hsec.Domain.Entities.General;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.ControlCritico.Commands.CreateControlCritico
{

    public class CreateControlCriticoCommand : IRequest<Unit>
    {
        public ControlCriticoDto data { get; set; }
        public class CreateControlCriticoCommandHandler : IRequestHandler<CreateControlCriticoCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public CreateControlCriticoCommandHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(CreateControlCriticoCommand request, CancellationToken cancellationToken)
            {

                string CodCC = request.data.CodCC;
                TControlCritico controlCritico = _context.TControlCritico.Find(CodCC);

                if (controlCritico == null)
                {
                    controlCritico = _mapper.Map<ControlCriticoDto, TControlCritico>(request.data);
                    _context.TControlCritico.Add(controlCritico);
                }
                else if(controlCritico.Estado == false)
                {
                    controlCritico.Criterios = _context.TCriterio.Where(t => t.Estado == true && t.CodCC.Equals(CodCC)).ToHashSet();
                    controlCritico = _mapper.Map<ControlCriticoDto, TControlCritico>(request.data, controlCritico);
                    _context.TControlCritico.Update(controlCritico);
                }
                else
                {
                    throw new Exception(String.Format("Clave en (1) en uso", CodCC));
                }

                await _context.SaveChangesAsync(cancellationToken);
                
                return Unit.Value;
            }
        }

      
    }
}