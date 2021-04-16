using AutoMapper;
using Hsec.Application.Common.Models;
using Hsec.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.Accesos.Queries.GetAccesos
{
    public class GetAccesosQuerybyRol : IRequest<GeneralCollection<AccesoVM>>
    {
        public int CodRol{ get; set; }
        public class GetAccesosQueryHandler : IRequestHandler<GetAccesosQuerybyRol, GeneralCollection<AccesoVM>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetAccesosQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<GeneralCollection<AccesoVM>> Handle(GetAccesosQuerybyRol request, CancellationToken cancellationToken)
            {           
                var accesos = _context.TAcceso.Include(i => i.RolAccesos).AsQueryable();
                var accesos_model = new List<AccesoVM>();
                foreach (var item in accesos)
                {
                    if (item.RolAccesos.Any(t => t.CodRol == request.CodRol))
                    {
                        var accesoPadre = new AccesoVM();
                        accesoPadre.CodAcceso = item.CodAcceso;
                        accesoPadre.Descripcion = item.Descripcion;
                        accesos_model.Add(accesoPadre);
                    }
                }
                return new GeneralCollection<AccesoVM>(accesos_model);
            }           
        }
    }
}
