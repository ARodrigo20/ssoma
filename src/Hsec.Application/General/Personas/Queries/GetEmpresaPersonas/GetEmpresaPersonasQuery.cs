using AutoMapper;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Hsec.Application.General.Personas.Queries.GetEmpresaPersonas
{
    public class GetEmpresaPersonasQuery : IRequest<List<EmpresaVM>>
    {
        public ICollection<string> dnisPersona { get; set; }
        public class GetEmpresaPersonasQueryHandler : IRequestHandler<GetEmpresaPersonasQuery, List<EmpresaVM>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetEmpresaPersonasQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<EmpresaVM>> Handle(GetEmpresaPersonasQuery request, CancellationToken cancellationToken)
            {
                List<EmpresaVM> empresas = new List<EmpresaVM>();

                //var result = (
                //                from p in _context.TPersona
                //                where (
                //                    p.Empresa.TrimEnd() != "" && p.Empresa.TrimEnd() != "-" && p.Empresa != null
                //                )
                //                select p);


                //var _result = _context.TPersona.Where(p => p.Empresa.TrimEnd() != "" && p.Empresa.TrimEnd() != "-" && p.Empresa != null);

                var _result = _context.TPersona.Select(p => p.Empresa).Distinct().ToList();

                //empresas = _mapper.Map<List<EmpresaVM>>(_result);

                int count = 0;
                EmpresaVM emp;

                foreach (var row in _result)
                {
                    if (row != null && row.TrimEnd() != "" && row.TrimEnd() != "-")
                    {
                        emp = new EmpresaVM();
                        emp.Correlativo = count;
                        emp.Empresa = row;
                        empresas.Add(emp);
                        count++;
                    }
                }
                return empresas;
            }
        }
    }
}