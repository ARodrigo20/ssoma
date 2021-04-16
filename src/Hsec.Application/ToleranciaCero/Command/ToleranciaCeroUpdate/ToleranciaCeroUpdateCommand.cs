using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.ToleranciaCero.Command.ToleranciaCeroInsert;
using Hsec.Application.ToleranciaCero.Models;
using Hsec.Domain.Entities.Otros;
using Hsec.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.ToleranciaCero.Command.ToleranciaCeroUpdate
{
    public class ToleranciaCeroUpdateCommand : IRequest<Unit>
    {
        public string CodTolCero { get; set; }

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


        public class ToleranciaCeroUpdateCommandHandler : IRequestHandler<ToleranciaCeroUpdateCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public ToleranciaCeroUpdateCommandHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(ToleranciaCeroUpdateCommand request, CancellationToken cancellationToken)
            {
                var tolerancia = _context.ToleranciaCero.Include(p => p.ToleranciaPersonas).Include(c => c.ToleranciaAnalisisCausa).Include(r => r.ToleranciaReglas).First(t => t.CodTolCero.Equals(request.CodTolCero));

                tolerancia.FechaTolerancia = request.FechaTolerancia;
                tolerancia.CodPosicionGer = request.CodPosicionGer;
                tolerancia.CodPosicionSup = request.CodPosicionSup;
                tolerancia.Proveedor = request.Proveedor;
                tolerancia.AntTolerancia = request.AntTolerancia;
                tolerancia.IncumpDesc = request.IncumpDesc;
                tolerancia.ConsecReales = request.ConsecReales;
                tolerancia.ConsecPot = request.ConsecPot;
                tolerancia.ConclusionesTol = request.ConclusionesTol;
                tolerancia.CodDetSancion = request.CodDetSancion;

                //PERSONAS
                tolerancia.ToleranciaPersonas = tolerancia.ToleranciaPersonas.Where(p => p.Estado == true).ToList();

                var interPersonas = tolerancia.ToleranciaPersonas.Select(x => x.CodPersona).Intersect(request.Personas).ToList(); //update
                var leftPersonas = request.Personas.Except(tolerancia.ToleranciaPersonas.Select(x => x.CodPersona)).ToList(); //new
                var rightPersonas = tolerancia.ToleranciaPersonas.Select(x => x.CodPersona).Except(request.Personas).ToList(); //delete

                foreach (var CodPersona in interPersonas)
                {
                    var pm = tolerancia.ToleranciaPersonas.First(t => t.CodPersona.Equals(CodPersona));
                    pm.Estado = true;
                }

                foreach (var CodPersona in leftPersonas)
                {
                    TPersonaToleranciaDto toleranciapersona = new TPersonaToleranciaDto();
                    toleranciapersona.CodPersona = CodPersona;
                    var _toleranciapersona = _mapper.Map<TPersonaToleranciaDto, TPersonaTolerancia>(toleranciapersona);
                    tolerancia.ToleranciaPersonas.Add(_toleranciapersona);
                }

                foreach (var CodPersona in rightPersonas)
                {
                    var pm = tolerancia.ToleranciaPersonas.First(t => t.CodPersona.Equals(CodPersona));
                    pm.Estado = false;
                }


                //CAUSAS
                tolerancia.ToleranciaAnalisisCausa = tolerancia.ToleranciaAnalisisCausa.Where(a => a.Estado == true).ToList();

                var interCausas = tolerancia.ToleranciaAnalisisCausa.Select(x => x.CodAnalisis).Intersect(request.Causas.Select(c => c.CodAnalisis)).ToList(); //update
                var leftCausas = request.Causas.Select(c => c.CodAnalisis).Except(tolerancia.ToleranciaAnalisisCausa.Select(x => x.CodAnalisis)).ToList(); //new
                var rightCausas = tolerancia.ToleranciaAnalisisCausa.Select(x => x.CodAnalisis).Except(request.Causas.Select(c => c.CodAnalisis)).ToList(); //delete

                foreach (var CodAnalisis in interCausas)
                {
                    var causa = request.Causas.First(c => c.CodAnalisis.Equals(CodAnalisis));
                    var cm = tolerancia.ToleranciaAnalisisCausa.First(t => t.CodAnalisis.Equals(CodAnalisis));
                    cm.Comentario = causa.Comentario;
                    cm.Estado = true;
                }

                foreach (var CodAnalisis in leftCausas)
                {
                    var causa = request.Causas.First(c => c.CodAnalisis.Equals(CodAnalisis));
                    TToleranciaCeroAnalisisCausaDto toleranciaanalisis = new TToleranciaCeroAnalisisCausaDto();
                    toleranciaanalisis.CodAnalisis = CodAnalisis;
                    toleranciaanalisis.Comentario = causa.Comentario;
                    var _toleranciaanalisis = _mapper.Map<TToleranciaCeroAnalisisCausaDto, TToleranciaCeroAnalisisCausa>(toleranciaanalisis);
                    tolerancia.ToleranciaAnalisisCausa.Add(_toleranciaanalisis);
                }

                foreach (var CodAnalisis in rightCausas)
                {
                    var cm = tolerancia.ToleranciaAnalisisCausa.First(t => t.CodAnalisis.Equals(CodAnalisis));
                    cm.Comentario = "";
                    cm.Estado = false;
                }

                //REGLAS
                tolerancia.ToleranciaReglas = tolerancia.ToleranciaReglas.Where(r => r.Estado == true).ToList();

                var interReglas = tolerancia.ToleranciaReglas.Select(x => x.CodRegla).Intersect(request.Reglas).ToList(); //update
                var leftReglas = request.Reglas.Except(tolerancia.ToleranciaReglas.Select(x => x.CodRegla)).ToList(); //new
                var rightReglas = tolerancia.ToleranciaReglas.Select(x => x.CodRegla).Except(request.Reglas).ToList(); //delete

                foreach (var CodRegla in interReglas)
                {
                    var rm = tolerancia.ToleranciaReglas.First(t => t.CodRegla.Equals(CodRegla));
                    rm.Estado = true;
                }

                foreach (var CodRegla in leftReglas)
                {
                    TRegTolDetalleDto toleranciaregla = new TRegTolDetalleDto();
                    toleranciaregla.CodRegla = CodRegla;
                    var _toleranciaregla = _mapper.Map<TRegTolDetalleDto, TRegTolDetalle>(toleranciaregla);
                    tolerancia.ToleranciaReglas.Add(_toleranciaregla);
                }

                foreach (var CodRegla in rightReglas)
                {
                    var rm = tolerancia.ToleranciaReglas.First(t => t.CodRegla.Equals(CodRegla));
                    rm.Estado = false;
                }

                _context.ToleranciaCero.Update(tolerancia);
                //_context.TUsuario.Remove(user);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
