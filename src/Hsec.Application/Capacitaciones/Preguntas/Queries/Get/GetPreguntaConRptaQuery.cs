using Hsec.Application.Capacitaciones.Preguntas.Queries.Get.DTOs;
using MediatR;
using AutoMapper;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Capacitaciones.Preguntas.Queries.Get.VMs;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;

namespace Hsec.Application.Capacitaciones.Preguntas.Queries.Get
{
    public class GetPreguntaConRptaQuery : IRequest<GetPreguntaVM>
    {
        public string codCurso { get; set; }
        public class GetPreguntaQueryHandler : IRequestHandler<GetPreguntaConRptaQuery, GetPreguntaVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetPreguntaQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<GetPreguntaVM> Handle(GetPreguntaConRptaQuery request, CancellationToken cancellationToken)
            {
                var codCurso = request.codCurso;
                var preguntasEntity = _context.TPreguntas.AsQueryable().Where(i => i.Estado && i.CodCurso == codCurso );
                GetPreguntaVM modelVM = new GetPreguntaVM();

                IList<GetPreguntaDTO> objetos = preguntasEntity
                 .ProjectTo<GetPreguntaDTO>(_mapper.ConfigurationProvider)
                 .ToList();

                foreach (var item in objetos) {
                    item.countRespuestas = string.IsNullOrEmpty(item.respuesta) ? 0 : item.respuesta.Split(";").Count();
                    item.respuesta = null;
                }
                modelVM.data = objetos;
                modelVM.count = objetos.Count;
                return modelVM;
            }
        }
    }
}
