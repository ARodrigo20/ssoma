using AutoMapper;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.General.Personas.Queries.GetPersona;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Globalization;

namespace Hsec.Application.General.Personas.Queries.GetAprobadorInmediato
{
    public class GetAprobadorInmediatoQuery : IRequest<PersonaVM>
    {
        public string codPersona { get; set; }
        public class GetAprobadorInmediatoHandler : IRequestHandler<GetAprobadorInmediatoQuery, PersonaVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetAprobadorInmediatoHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<PersonaVM> Handle(GetAprobadorInmediatoQuery request, CancellationToken cancellationToken)
            {
                List<PersonaVM> personas = new List<PersonaVM>();

                var codPosicion = _context.TJerarquiaPersona.Where(jp => jp.CodPersona == request.codPersona && jp.Estado).Select(jp => jp.CodPosicion).FirstOrDefault();

                if (codPosicion != 0)
                {
                    var esGerencia = false;
                    var jerarquia = _context.TJerarquia.Where(j => j.CodPosicion == codPosicion && j.Estado).FirstOrDefault();
                    //var posicion = _context.TJerarquia.Where(jp => jp.CodPosicion == codPosicion).FirstOrDefault();
                    while (esGerencia == false)
                    {
                        
                        if (jerarquia != null)
                        {
                            var posicionPadre = _context.TJerarquia.Where(j => j.CodPosicion == jerarquia.CodPosicionPadre && j.Estado).FirstOrDefault();
                            if (posicionPadre != null)
                            {
                                jerarquia = posicionPadre;
                                //codPosicionPadre = (int)posicionPadre.CodPosicion;
                                if (jerarquia.Tipo == "G")
                                {
                                    esGerencia = true; 
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                    var codPosicionPadre = (jerarquia != null) ? jerarquia.CodPosicion : 0;


                    var respJerarquia = _context.TJerarquiaPersona.Where(jp => jp.CodPosicion == codPosicionPadre && jp.CodTipoPersona == 1 && jp.Estado && DateTime.Now > jp.FechaInicio && DateTime.Now < jp.FechaFin).FirstOrDefault();
                    if (respJerarquia != null)
                    {
                        var respPersona = _context.TPersona.Where(p => p.CodPersona == respJerarquia.CodPersona && p.Estado).FirstOrDefault();
                        if(respPersona != null)
                        {
                            return _mapper.Map<PersonaVM>(respPersona);
                        }
                        else
                        {
                            return new PersonaVM();
                        }
                    }
                    else
                    {
                        return new PersonaVM();
                    }


                }
                else
                {
                    return new PersonaVM();
                }
            }
        }
    }
}