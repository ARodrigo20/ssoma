using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.General;
using Hsec.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.PlanAnualVerifiacionCC.Queries.GetCartillasPorPersonFiltro
{
    public class GetVerCCPorPersonasFiltroQuery : IRequest<VerCCPorPersonaFiltroVM>
    {
        public FiltroVerCCPorPersonasFiltro filtro {get;set;}
        
        public class GetVerCCPorPersonasFiltroQueryHandler : IRequestHandler<GetVerCCPorPersonasFiltroQuery, VerCCPorPersonaFiltroVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetVerCCPorPersonasFiltroQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            
            public async Task<VerCCPorPersonaFiltroVM> Handle(GetVerCCPorPersonasFiltroQuery request, CancellationToken cancellationToken)
            {
                var vm = new VerCCPorPersonaFiltroVM();
                var filtro = request.filtro;

                var JerPosicion = await _context.TJerarquia.FindAsync(filtro.Gerencia);
                IList<string> JerarquiaPersonas = null;
                if(!string.IsNullOrEmpty(filtro.CodPersona)){
                    JerarquiaPersonas = new List<string>();
                    JerarquiaPersonas.Add(filtro.CodPersona);
                }
                else if(JerPosicion!=null){
                    JerarquiaPersonas = _context.TJerarquia.Join(_context.TJerarquiaPersona, jer => jer.CodPosicion, jper => jper.CodPosicion, (jer, jper) => new { jer = jer, jper = jper })
                    .Where(tuple => (tuple.jer.PathJerarquia.Substring(0, JerPosicion.PathJerarquia.Length) == JerPosicion.PathJerarquia && tuple.jper.CodTipoPersona == 1))
                    .Select(t => t.jper.CodPersona)
                    .ToList();   
                }
                var meses = Meses(filtro.CodMes);

                var query = _context.TPlanAnualVerConCri.Where(t => 
                    t.Anio.Equals(filtro.Anio)
                    && ( String.IsNullOrEmpty(filtro.CodPersona) ||  t.CodPersona.Equals(filtro.CodPersona) )
                    && ( String.IsNullOrEmpty(filtro.CodReferencia) ||  t.CodReferencia.Equals(filtro.CodReferencia) )
                    && ( JerarquiaPersonas == null || JerarquiaPersonas.Contains(t.CodPersona))
                    && ( meses == null || meses.Contains(t.CodMes) )
                    )
                    .GroupBy(t => new {t.CodPersona,t.CodReferencia,t.Anio})
                    .Select(data => new VerCCProDto(){
                        CodReferencia = data.Key.CodReferencia,
                        NumeroPlanes = data.Count(),
                        Anio = data.Key.Anio,
                        CodPersona = data.Key.CodPersona
                    });
                    // .ProjectTo<VerCCProDto>(_mapper.ConfigurationProvider)
                    // .ToList();

                vm.Count = query.Count();
                vm.Pagina = filtro.Pagina;

                if ((filtro.Pagina) > (vm.Count / filtro.PaginaTamanio) + 1) filtro.Pagina = 1;
                
                var ListQuery = query
                        .Skip(filtro.Pagina * filtro.PaginaTamanio - filtro.PaginaTamanio)
                        .Take(filtro.PaginaTamanio)
                        .ToHashSet();

                var index = 0;
                foreach(var iten in ListQuery){
                    iten.Orden = index++;
                    iten.Persona = await GetPersonaFull(iten.CodPersona);
                    var jer = await GetJeraquiaSG(iten.CodPersona);
                    if(jer != null && jer.Count()>0){
                        var super = jer.Where(t => t.Tipo.Equals("S")).FirstOrDefault();
                        if(super!=null){
                            iten.CodSuperintendencia = super.CodPosicion.ToString();
                            iten.Superintendencia = super.Descripcion;
                        }
                        var gerencia = jer.Where(t => t.Tipo.Equals("G")).FirstOrDefault();
                        if(gerencia!=null){
                            iten.CodGerencia = gerencia.CodPosicion.ToString();
                            iten.Gerencia = gerencia.Descripcion;
                        }
                    }
                }
                vm.list = ListQuery;

                return vm;
            }

            private List<string> Meses(string mes){
                
                if(String.IsNullOrEmpty(mes)) return null;
                var len = Int32.Parse(mes);
                var data = new List<string>();
                for (int i = 1; i <= len; i++)
                {
                    data.Add(i.ToString());
                }
                return data;
                
            }

            private async Task<string> GetPersonaFull(string codPersona){
                if(String.IsNullOrEmpty(codPersona)) return "";
                var persona = _context.TPersona.Where(t => t.CodPersona.Equals(codPersona)).FirstOrDefault();
                if(persona == null) return "";
                return persona.ApellidoPaterno + " " + persona.ApellidoMaterno + ", " + persona.Nombres;
            }

            private async Task<List<TJerarquia>> GetJeraquiaSG(string codPersona){
                var jerarquiaPersona = await _context.TJerarquiaPersona.FirstOrDefaultAsync(i => i.CodPersona == codPersona);

                bool estadoG = true, estadoS = true;
                if (jerarquiaPersona == null) return null;
                List<TJerarquia> list = new List<TJerarquia>();

                TJerarquia iter = _context.TJerarquia.Where(i => i.CodPosicion == jerarquiaPersona.CodPosicion).FirstOrDefault() ;

                while(iter!=null && (estadoG||estadoS) ){

                    if (iter.Tipo == "S" && estadoS)
                    {
                        estadoS = false;
                        list.Add(iter);
                    }    
                    if (iter.Tipo == "G" && estadoG)
                    {
                        estadoG = false;
                        list.Add(iter);
                    }         
                    iter = _context.TJerarquia.Where(i => i.CodPosicion == iter.CodPosicionPadre).FirstOrDefault();
                }

                return list;
            }

        }
    }
}