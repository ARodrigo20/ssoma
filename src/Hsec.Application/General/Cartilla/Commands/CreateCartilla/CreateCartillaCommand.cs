using AutoMapper;
using Hsec.Application.General.Cartilla.Models;
using Hsec.Application.Common.Interfaces;
using Hsec.Domain.Entities.General;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.Cartilla.Commands.CreateCartilla
{
    public class CreateCartillaCommand : IRequest<Unit>
    {
        public CartillaDto data {get;set;}
        public class CreateCartillaCommandHandler : IRequestHandler<CreateCartillaCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public CreateCartillaCommandHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(CreateCartillaCommand request, CancellationToken cancellationToken)
            {
                try{
                    TCartilla obj = _context.TCartilla.Find(request.data.CodCartilla);
                    
                    if (obj == null)
                    {
                        obj = _mapper.Map<CartillaDto, TCartilla>(request.data);
                        string codigo = "";
                        foreach (var item in obj.Detalle)
                        {
                            codigo = nextCod(codigo);
                            item.CodCartillaDet = codigo;
                        }
                        _context.TCartilla.Add(obj);
                    }
                    else if(obj.Estado == false)
                    {
                        obj.Estado = true;
                        obj = _mapper.Map<CartillaDto, TCartilla>(request.data);
                        string codigo = "";
                        foreach (var item in obj.Detalle)
                        {
                            codigo = nextCod(codigo);
                            item.CodCartillaDet = codigo;
                        }
                        _context.TCartilla.Update(obj);
                    }
                    else
                    {
                        throw new Exception(String.Format("Codigo Cartilla '{0}' en uso", request.data.CodCartilla));
                    }
                    await _context.SaveChangesAsync(cancellationToken);
                }catch(Exception ex){
                    throw ex;
                }
                return Unit.Value;
            }

            public string nextCod(string COD_MAX="")
            {
                if(string.IsNullOrEmpty(COD_MAX)) 
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