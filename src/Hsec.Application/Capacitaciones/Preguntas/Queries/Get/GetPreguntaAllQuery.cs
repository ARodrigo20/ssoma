using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Capacitaciones.Preguntas.Queries.Get.DTOs;
using Hsec.Application.Capacitaciones.Preguntas.Queries.Get.VMs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Capacitaciones.Preguntas.Queries.Get
{
    public class GetPreguntaAllQuery : IRequest<GetPreguntaVM>
    {
        public string codCurso { get; set; }
        public class GetPreguntaAllQueryHandler : IRequestHandler<GetPreguntaAllQuery, GetPreguntaVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetPreguntaAllQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<GetPreguntaVM> Handle(GetPreguntaAllQuery request, CancellationToken cancellationToken)
            {
                string codCurso = request.codCurso;
                var preguntasEntity = _context.TPreguntas.AsQueryable().Where(i => i.Estado && i.CodCurso == codCurso);
                GetPreguntaVM modelVM = new GetPreguntaVM();

                IList<GetPreguntaDTO> objeto = preguntasEntity
                .ProjectTo<GetPreguntaDTO>(_mapper.ConfigurationProvider)
                .ToList();

                foreach (var it in objeto) {
                    it.countRespuestas = string.IsNullOrEmpty(it.respuesta) ? 0 : it.respuesta.Split(";").ToList().Count();
                    it.alternativas = it.alternativas.Where(a => a.estado).ToList();
                }

                modelVM.data = objeto;
                modelVM.count = objeto.Count();

                return modelVM;
            }
        }
    }
}
