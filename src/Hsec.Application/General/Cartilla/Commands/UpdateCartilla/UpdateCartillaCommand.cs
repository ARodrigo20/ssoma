using AutoMapper;
using Hsec.Application.General.Cartilla.Models;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Domain.Entities.General;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.Cartilla.Commands.UpdateCartilla
{
    public class UpdateCartillaCommand : IRequest<Unit>
    {
        public CartillaDto data { get; set; }
        public string CodCartilla { get; set; }
        public class UpdateCartillaCommandHandler : IRequestHandler<UpdateCartillaCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public UpdateCartillaCommandHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(UpdateCartillaCommand request, CancellationToken cancellationToken)
            {
                TCartilla obj = _context.TCartilla.Find(request.CodCartilla);

                if (obj != null && obj.Estado == false) throw new NotFoundException("Cartilla", request.CodCartilla);
                else
                {
                    obj.Detalle = _context.TCartillaDetalle.Where(t => t.CodCartilla.Equals(request.CodCartilla)).ToList();
                    obj = _mapper.Map<CartillaDto, TCartilla>(request.data,obj);

                    var max = "";

                    foreach(var item in obj.Detalle)
                    {
                        if (string.IsNullOrEmpty(item.CodCartillaDet))
                        {
                            item.CodCartillaDet = nextCod(max);
                            max = nextCod(max);
                        }
                    }

                    _context.TCartilla.Update(obj);
                    await _context.SaveChangesAsync(cancellationToken);
                }
                return Unit.Value;
            }
            public string nextCod(string COD_MAX = "")
            {
                if (string.IsNullOrEmpty(COD_MAX))
                    COD_MAX = _context.TCartillaDetalle.Max(t => t.CodCartillaDet);
                if (COD_MAX == null) COD_MAX = "CDET0000001";
                else
                {
                    string numberStr = COD_MAX.Substring(4);
                    int max = Int32.Parse(numberStr) + 1;
                    COD_MAX = String.Format("CDET{0,7:0000000}", max);
                }
                return COD_MAX;
            }
        }

    }
}