using AutoMapper;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using Hsec.Application.Common.Exceptions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.ToleranciaCero.Models;
using Hsec.Domain.Entities.Otros;

namespace Hsec.Application.ToleranciaCero.Command.ToleranciaCeroInsert
{
    public class ToleranciaCeroInsertCommand : IRequest<Unit>
    {

        public DateTime FechaTolerancia { get; set; }

        public string CodPosicionGer { get; set; }

        public string CodPosicionSup { get; set; }

        public string Proveedor { get; set; }

        public string AntTolerancia { get; set; }

        public string IncumpDesc { get; set; }

        public string ConsecReales { get; set; }

        public string ConsecPot { get; set; }

        public string ConclusionesTol { get; set; }

        public string CodDetSancion { get; set; }

        public ICollection<string> Personas { get; set; }

        public ICollection<TToleranciaCeroAnalisisCausaDto> Causas { get; set; }

        public ICollection<string> Reglas { get; set; }

        public class ToleranciaCeroInsertCommandHandler : IRequestHandler<ToleranciaCeroInsertCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public ToleranciaCeroInsertCommandHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(ToleranciaCeroInsertCommand request, CancellationToken cancellationToken)
            {
                try
                {

                    ToleranciaCeroDto nuevo = new ToleranciaCeroDto();
                    string maxCod = _context.ToleranciaCero.Max(t => t.CodTolCero);
                    if (maxCod == null) maxCod = "TOL00000001";
                    else
                    {
                        int id = int.Parse(maxCod.Substring(3, 8)) + 1;
                        maxCod = "TOL" + id.ToString("D8");
                    }

                    nuevo.CodTolCero = maxCod;
                    nuevo.FechaTolerancia = request.FechaTolerancia;
                    nuevo.CodPosicionGer = request.CodPosicionGer;
                    nuevo.CodPosicionSup = request.CodPosicionSup;
                    nuevo.Proveedor = request.Proveedor;
                    nuevo.AntTolerancia = request.AntTolerancia;
                    nuevo.IncumpDesc = request.IncumpDesc;
                    nuevo.ConsecReales = request.ConsecReales;
                    nuevo.ConsecPot = request.ConsecPot;
                    nuevo.ConclusionesTol = request.ConclusionesTol;
                    nuevo.CodDetSancion = request.CodDetSancion;

                    foreach (string persona in request.Personas)
                    {
                        TPersonaToleranciaDto toleranciapersona = new TPersonaToleranciaDto();
                        toleranciapersona.CodPersona = persona;
                        nuevo.ToleranciaPersonas.Add(toleranciapersona);

                    }

                    foreach (var causa in request.Causas)
                    {
                        TToleranciaCeroAnalisisCausaDto analisiscausa = new TToleranciaCeroAnalisisCausaDto();
                        analisiscausa.CodAnalisis = causa.CodAnalisis;
                        analisiscausa.Comentario = causa.Comentario;
                        analisiscausa.CodCondicion = causa.CodCondicion;
                        nuevo.ToleranciaAnalisisCausa.Add(analisiscausa);

                    }

                    foreach (string regla in request.Reglas)
                    {
                        TRegTolDetalleDto reglatolerancia = new TRegTolDetalleDto();
                        reglatolerancia.CodRegla = regla;
                        nuevo.ToleranciaReglas.Add(reglatolerancia);

                    }

                    var tolerancia = _mapper.Map<ToleranciaCeroDto, TToleranciaCero>(nuevo);
                    _context.ToleranciaCero.Add(tolerancia);

                    await _context.SaveChangesAsync(cancellationToken);

                    return Unit.Value;
                }catch(Exception e)
                {
                    throw e;
                }
            }
        }
    }
}
