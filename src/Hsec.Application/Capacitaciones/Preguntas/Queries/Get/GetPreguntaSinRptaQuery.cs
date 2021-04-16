using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Capacitaciones.Preguntas.Queries.Get.DTOs;
using Hsec.Application.Capacitaciones.Preguntas.Queries.Get.VMs;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Capacitaciones.Preguntas.Queries.Get
{
    public class GetPreguntaSinRptaQuery : IRequest<GetPreguntaVM>
    {
        public string codCurso { get; set; }
        public class GetPreguntaQueryHandler : IRequestHandler<GetPreguntaSinRptaQuery, GetPreguntaVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetPreguntaQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<GetPreguntaVM> Handle(GetPreguntaSinRptaQuery request, CancellationToken cancellationToken)
            {
                var codCurso = request.codCurso;
                var preguntasEntity = _context.TPreguntas.AsQueryable().Where(i => i.Estado && i.CodCurso == codCurso);
                GetPreguntaVM modelVM = new GetPreguntaVM();

                IList<GetPreguntaDTO> objeto = preguntasEntity
                 .ProjectTo<GetPreguntaDTO>(_mapper.ConfigurationProvider)
                 .ToList();

                modelVM.data = objeto;
                modelVM.count = objeto.Count;

                return modelVM;
            }
        }
    }
}
