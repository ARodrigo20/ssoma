using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Capacitaciones.PerfilCapacitacionPosicion.Command.ExtraerPopUp.VMs;
using Hsec.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace Hsec.Application.Capacitaciones.PerfilCapacitacionPosicion.Command.ExtraerPopUp
{
    public class ExtraerPopUpCursoPosicionCommand : IRequest<IList<ExtraerPopUpCursoPosicionResponseVM>>
    {
        public ExtraerPopUpCursoPosicionRequestVM VM { get; set; }
        public class ExtraerPopUpCursoPosicionCommandHandler : IRequestHandler<ExtraerPopUpCursoPosicionCommand, IList<ExtraerPopUpCursoPosicionResponseVM>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            public ExtraerPopUpCursoPosicionCommandHandler(IApplicationDbContext context, IMapper mapper)
            {
                this._context = context;
                _mapper = mapper;
            }

            public async Task<IList<ExtraerPopUpCursoPosicionResponseVM>> Handle(ExtraerPopUpCursoPosicionCommand request, CancellationToken cancellationToken)
            {
                var modelVM = request.VM;

                var count = _context.TTemaCapacitacion.Where(i => i.Estado &&
                                (String.IsNullOrEmpty(modelVM.codTipo) || i.CodTipoTema.Contains(modelVM.codTipo)) &&
                                (String.IsNullOrEmpty(modelVM.curso) || i.Descripcion.Contains(modelVM.curso))).Count();

                var valoresTemas = _context.TTemaCapacitacion.Where(i => i.Estado &&
                               (String.IsNullOrEmpty(modelVM.codTipo) || i.CodTipoTema.Contains(modelVM.codTipo)) &&
                               (String.IsNullOrEmpty(modelVM.curso) || i.Descripcion.Contains(modelVM.curso)));

                IList<ExtraerPopUpCursoPosicionResponseVM> temaVMs = new List<ExtraerPopUpCursoPosicionResponseVM>();

                var objeto = valoresTemas
               .ProjectTo<ExtraerPopUpCursoPosicionResponseVM>(_mapper.ConfigurationProvider).OrderByDescending(i => i.codTemaCapacita).ToList();
                return objeto;
            }
        }
    }
}