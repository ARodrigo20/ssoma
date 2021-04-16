using AutoMapper;
using Hsec.Application.Common.Interfaces;
using Hsec.Domain.Entities.General;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.Aprobaciones.Commands.CreateProceso
{
    public class CreateProcesoCommand : IRequest<Unit>
    {
        public CreateProcesoVM Data { get; set; }
        public class CreateProcesoCommandHandler : IRequestHandler<CreateProcesoCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public CreateProcesoCommandHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(CreateProcesoCommand request, CancellationToken cancellationToken)
            {
                var data = request.Data;
                var obj = _mapper.Map<TProceso>(data);
                obj.CadenaAprobacion = String.Join<string>('.',data.Lista.Select(t => t.CodCadenaAprobacion));

                obj.CodProceso = nextCodigo();

                _context.TProceso.Add(obj);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }

            private string nextCodigo(){
                var obj = _context.TProceso.OrderByDescending(t => t.CodProceso).FirstOrDefault();
                var PRE = "PA";
                var TAM = 6;
                
                if(obj==null) return formatCorrelativo(PRE,TAM,1);
                else{
                    var codigo_int = getNumero(PRE,obj.CodProceso);
                    return formatCorrelativo(PRE,TAM,codigo_int+1);
                }
            }
            private String formatCorrelativo(String pre,int tam,int codigo){
                var pad_zero = new String('0',tam-pre.Length);
                return String.Format(pre.ToUpper()+"{0:"+pad_zero+"}", codigo);
            }
            private int getNumero(String pre,String codigo){
                var numero_str = codigo.Substring(pre.Length,codigo.Length-pre.Length);
                return Int32.Parse(numero_str);
            }
        }
    }
}