using AutoMapper;
using Hsec.Application.Common.Interfaces;

namespace Hsec.Application.Observacion.Commands.UpdateObservacion {
    internal class Strategia {
        protected readonly IApplicationDbContext _context;
        protected readonly IMapper _mapper;
        public Strategia (IApplicationDbContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }
        public virtual void UpsertSubTipo (ObservacionDto data) { }
        public virtual void CreateDetalleSubTipo (ObservacionDto data) { }
        public virtual void DeleteSubtipo (string COD_OBSERVACION) { }
        public virtual void DeleteDetalleSubtipo (string COD_OBSERVACION) { }
    }
}