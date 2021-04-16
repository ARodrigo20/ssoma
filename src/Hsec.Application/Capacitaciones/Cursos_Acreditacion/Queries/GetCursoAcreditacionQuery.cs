using AutoMapper;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Capacitaciones.Cursos_Acreditacion.Queries.DTOs;
using Hsec.Application.Capacitaciones.Cursos_Acreditacion.Queries.VMs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Capacitaciones.Cursos_Acreditacion.Queries
{
    public class GetCursoAcreditacionQuery : IRequest<GetCursoAcreditacionRespVM>
    {
        public GetCursoAcreditacionReqVM VM { get; set; }

        public class GetCursoAcreditacionQueryHandler : IRequestHandler<GetCursoAcreditacionQuery, GetCursoAcreditacionRespVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetCursoAcreditacionQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<GetCursoAcreditacionRespVM> Handle(GetCursoAcreditacionQuery request, CancellationToken cancellationToken)
            {
                try {
                    GetCursoAcreditacionRespVM respVM = new GetCursoAcreditacionRespVM();
                    var VM = request.VM;
                    DateTime fechIniVM = (DateTime)VM.fechaInicio;
                    DateTime fechFinVM = (DateTime)VM.fechaFin;

                    var registrosAll = (from curso in _context.TCurso.Where(I => I.Estado)
                                        join part in _context.TParticipantes.Where(I => I.Estado) on curso.CodCurso equals part.CodCurso
                                        join temCap in _context.TTemaCapacitacion.Where(I => I.Estado) on curso.CodTemaCapacita equals temCap.CodTemaCapacita into lista
                                        from list in lista.DefaultIfEmpty()
                                        where (
                                         curso.Estado && part.Estado && list.Estado && part.Nota!= null && part.Nota != 0 &&
                                         ((String.IsNullOrEmpty(VM.codPersona) || (part.CodPersona.EndsWith(VM.codPersona)))) &&
                                         ((String.IsNullOrEmpty(VM.temaCap) || (list.CodTemaCapacita.EndsWith(VM.temaCap)))) &&
                                        ((fechIniVM.Date <= curso.FechaInicio.Date && curso.FechaInicio.Date <= fechFinVM.Date) || (fechIniVM.Date <= curso.FechaFin.Date && curso.FechaFin.Date <= fechFinVM.Date))
                                        )
                                        select new GetCursoAcreditacionRespDTO()
                                        {
                                            descripcion = list.Descripcion,
                                            codTemaCapacita = list.CodTemaCapacita,
                                            fechaProgramada = curso.FechaInicio,
                                            nota = (!string.IsNullOrEmpty(part.Nota.ToString()) ? part.Nota.ToString() : ""),
                                            codCurso = curso.CodCurso,
                                            codPersona = (!string.IsNullOrEmpty(part.CodPersona) ? part.CodPersona : ""),
                                        }).ToList();
                    var distinctItems = registrosAll.OrderByDescending(i => i.fechaProgramada).GroupBy(x => x.codTemaCapacita).Select(y => y.First()).ToList();
                    foreach (var item in distinctItems)
                    {
                        var verifAcred = _context.TAcreditacionCurso.FirstOrDefault(I => I.Estado && I.CodPersona == item.codPersona && I.CodCurso == item.codCurso);
                        if (verifAcred != null)
                        {

                            item.codStiker = (!string.IsNullOrEmpty(verifAcred.CodStiker) ? verifAcred.CodStiker : "");
                            item.candado = (!string.IsNullOrEmpty(verifAcred.Candado) ? verifAcred.Candado : "");
                            item.fechaStiker = verifAcred.FechaStiker;
                            item.fechaTarjeta = verifAcred.FechaTarjeta;
                        }
                        respVM.data.Add(item);
                    }

                    var acredCursosPag = respVM.data.Where(i => !String.IsNullOrEmpty(i.codPersona)).OrderByDescending(i => i.codCurso).Skip(VM.pagina * VM.paginaTamanio - VM.paginaTamanio).Take(VM.paginaTamanio).ToList();

                    respVM.data = acredCursosPag;
                    respVM.count = distinctItems.Count();
                    return respVM;
                }
                catch (Exception e) {
                    throw new ExceptionGeneral("Error interno de Servidor");
                }
                
            }
        }
    }
}