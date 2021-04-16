using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Capacitaciones.CursoProgramacionRule.Queries.DTOs;
using Hsec.Application.Capacitaciones.CursoProgramacionRule.Queries.VMs;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Capacitaciones.CursoProgramacionRule.Queries
{
    public class GetCursoProgramacionRulleFiltroQuery : IRequest<GetCursoProgramacionRuleResponseAllVM>
    {
        public GetCursoProgramacionRuleFiltroRequestVM VM { get; set; }

        public class GetCursoProgramacionRulleFiltroQueryHandler : IRequestHandler<GetCursoProgramacionRulleFiltroQuery, GetCursoProgramacionRuleResponseAllVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetCursoProgramacionRulleFiltroQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<GetCursoProgramacionRuleResponseAllVM> Handle(GetCursoProgramacionRulleFiltroQuery request, CancellationToken cancellationToken)
            {
                var cursoRule = _context.TCursoRules;
                var ViewModel = request.VM;

                var count = cursoRule.Where(i => i.Estado &&
                    (String.IsNullOrEmpty(ViewModel.recurrenceId) || (i.RecurrenceID.EndsWith(ViewModel.recurrenceId))) &&
                    (String.IsNullOrEmpty(ViewModel.recurrenceRule) || (i.RecurrenceRule.Contains(ViewModel.recurrenceRule))) &&
                    (String.IsNullOrEmpty(ViewModel.recurrenceException) || (i.RecurrenceException.Contains(ViewModel.recurrenceException))) &&
                    (ViewModel.tipoRecurrenceRule == i.TipoRecurrenceRule) &&
                   ((i.FechaInicio.Date >= ViewModel.start.Date && ViewModel.end.Date >= i.FechaFin.Date))
                    ).Count();

                var temaCap = cursoRule.Where(i => i.Estado &&
                    (String.IsNullOrEmpty(ViewModel.recurrenceId) || (i.RecurrenceID.EndsWith(ViewModel.recurrenceId))) &&
                    (String.IsNullOrEmpty(ViewModel.recurrenceRule) || (i.RecurrenceRule.Contains(ViewModel.recurrenceRule))) &&
                    (String.IsNullOrEmpty(ViewModel.recurrenceException) || (i.RecurrenceException.Contains(ViewModel.recurrenceException))) &&
                    (ViewModel.tipoRecurrenceRule == i.TipoRecurrenceRule) &&
                   ((i.FechaInicio.Date >= ViewModel.start.Date && ViewModel.end.Date >= i.FechaFin.Date))
                    ).OrderByDescending(i => i.RecurrenceID);

                var objeto = temaCap
                .ProjectTo<GetCursoProgramacionRuleResponseAllDto>(_mapper.ConfigurationProvider).Where(i => i.tipoRecurrenceRule && i.estado)
                .ToList();

                GetCursoProgramacionRuleResponseAllVM ViewModelResp = new GetCursoProgramacionRuleResponseAllVM();
                ViewModelResp.data = objeto;
                ViewModelResp.count = objeto.Count;

                return ViewModelResp;
            }
        }
    }
}
