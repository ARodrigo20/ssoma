using AutoMapper;
using Hsec.Application.Common.Models;
using Hsec.Application.Common.Interfaces;
using Hsec.Domain.Entities.General;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.Accesos.Queries.GetAccesos
{
    public class GetAccesosQuery : IRequest<GeneralCollection<AccesoVM>>
    {        
        public class GetAccesosQueryHandler : IRequestHandler<GetAccesosQuery, GeneralCollection<AccesoVM>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetAccesosQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<GeneralCollection<AccesoVM>> Handle(GetAccesosQuery request, CancellationToken cancellationToken)
            {
                //AccesoVM padre = new AccesoVM();
                var accesos = _context.TAcceso.Include(i => i.Hijos).AsQueryable();/*Distinct().OrderBy(x => x.CodAcceso);*//*.Skip(0).Take(10);*/
                var accesos_model = new List<AccesoVM>();
                foreach (var item in accesos)
                {
                    if (item.CodPadre == null)
                    {
                        AccesoVM node = recursion(item);
                        accesos_model.Add(node);
                    }
                }
                return new GeneralCollection<AccesoVM>(accesos_model);
            }

            public AccesoVM recursion(TAcceso it)
            {
                var accesoPadre = new AccesoVM();
                accesoPadre.CodAcceso = it.CodAcceso;
                accesoPadre.Descripcion = it.Descripcion;
                if (it.Hijos.Count == 0)
                {
                    return accesoPadre;
                }
                foreach (var item in it.Hijos)
                {
                    accesoPadre.Hijos.Add(recursion(item));
                }
                return accesoPadre;
            }
        }
    }
}
