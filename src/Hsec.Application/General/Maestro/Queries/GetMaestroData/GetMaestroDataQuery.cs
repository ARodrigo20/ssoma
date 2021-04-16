using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Interfaces;
using Hsec.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.Maestro.Queries.GetMaestroData
{
    public class GetMaestroDataQuery : IRequest<IList<MaestroDataVM>>
    {
        public class GetMaestroDataQueryHandler : IRequestHandler<GetMaestroDataQuery, IList<MaestroDataVM>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetMaestroDataQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;

               
            }

            public async Task<IList<MaestroDataVM>> Handle(GetMaestroDataQuery request, CancellationToken cancellationToken)
            {

                List<MaestroDataVM> mVM = new List<MaestroDataVM>();               

                var tablas= await _context.TMaestro.Select(t=>t.CodTabla).Distinct().ToListAsync(cancellationToken).ConfigureAwait(true);

                var maestros = await _context.TMaestro.OrderBy(t =>t.CodTabla).ThenByDescending(x => x.Descripcion).ToListAsync(cancellationToken).ConfigureAwait(true);

                foreach (var tb in tablas) {
                    MaestroDataVM tabla = new MaestroDataVM(tb);
                    var list = maestros.Where(om => om.Estado && om.CodTabla == tb).OrderBy(t => t.CodTipo).ToList();
                    tabla.Tipos = _mapper.Map<List<CampoDto>>(list);
                    mVM.Add(tabla);
                }

                MaestroDataVM tbAnalisisC = new MaestroDataVM("ACTO_SUB_ESTANDAR");
                tbAnalisisC.Tipos= await _context.TAnalisisCausa.Where(om => om.Estado && om.CodPadre == "co01")
                 .ProjectTo<CampoDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.Descripcion).ToListAsync(cancellationToken).ConfigureAwait(true);
                mVM.Add(tbAnalisisC);

                MaestroDataVM tbcondo = new MaestroDataVM("CONDICION_SUB_ESTANDAR");
                tbcondo.Tipos = await _context.TAnalisisCausa.Where(om => om.Estado && om.CodPadre == "co02")
                 .ProjectTo<CampoDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.Descripcion).ToListAsync(cancellationToken).ConfigureAwait(true);
                mVM.Add(tbcondo);

                MaestroDataVM enumTipoPersona = new MaestroDataVM("TipoPersona");
                enumTipoPersona.Tipos = Enum.GetValues(typeof(TipoPersona))
                       .Cast<TipoPersona>()
                       .Select(p => new CampoDto { CodRegistro = p.GetHashCode().ToString(), Descripcion = p.ToString() })
                       .ToList();
                mVM.Add(enumTipoPersona);

                MaestroDataVM enumTipoDocumento = new MaestroDataVM("TipoDocumento");
                enumTipoDocumento.Tipos = Enum.GetValues(typeof(TipoDocumento))
                       .Cast<TipoDocumento>()
                       .Select(p => new CampoDto { CodRegistro = p.GetHashCode().ToString(), Descripcion = p.ToString() })
                       .ToList();
                mVM.Add(enumTipoDocumento);

                //mDVM.CodTabla = "ACTIVIDADRELACIONADA";
                //mDVM.Tipos =  await _context.TMaestro
                //    .Where(om => om.Estado && om.CodTabla== "ACTIVIDADRELACIONADA")
                //    .ProjectTo<CampoDto>(_mapper.ConfigurationProvider)
                //    .OrderBy(t => t.Descripcion)
                //    .ToListAsync(cancellationToken).ConfigureAwait(true);

                //int ACTO_SUB_ESTANDAR = 1;
                //mVM.observacion.ActoSubEstandar = await _context.TAnalisisCausa
                //    .Where(om => om.Estado && om.CodCondicion.Equals(ACTO_SUB_ESTANDAR))
                //    .ProjectTo<CampoDto>(_mapper.ConfigurationProvider)
                //    .OrderBy(t => t.Descripcion)
                //    .ToListAsync(cancellationToken);

                //mVM.observacion.AreaHSEC = await _context.TMaestro
                //    .Where(om => om.Estado && om.CodTabla.Equals("AREAHSEC"))
                //    .ProjectTo<CampoDto>(_mapper.ConfigurationProvider)
                //    .OrderBy(t => t.Descripcion)
                //    .ToListAsync(cancellationToken);

                //int CONDICION_SUB_ESTANDAR = 2;
                //mVM.observacion.CondicionSubEstandar = await _context.TAnalisisCausa
                //    .Where(om => om.Estado && om.CodCondicion.Equals(CONDICION_SUB_ESTANDAR))
                //    .ProjectTo<CampoDto>(_mapper.ConfigurationProvider)
                //    .OrderBy(t => t.Descripcion)
                //    .ToListAsync(cancellationToken);

                //mVM.observacion.Correcion = await _context.TMaestro
                //    .Where(om => om.Estado && om.CodTabla.Equals("OBCO"))
                //    .ProjectTo<CampoDto>(_mapper.ConfigurationProvider)
                //    .OrderBy(t => t.Descripcion)
                //    .ToListAsync(cancellationToken);

                ////mVM.Empresa = await _context.TMaestro
                ////    .Where(om => om.Estado && om.CodTabla == VirtualNameTable.Empresa)
                ////    .ProjectTo<CampoDto>(_mapper.ConfigurationProvider)
                ////    .OrderBy(t => t.Descripcion)
                ////    .ToListAsync(cancellationToken);

                //mVM.observacion.Error = await _context.TMaestro
                //    .Where(om => om.Estado && om.CodTabla.Equals("ERROR"))
                //    .ProjectTo<CampoDto>(_mapper.ConfigurationProvider)
                //    .OrderBy(t => t.Descripcion)
                //    .ToListAsync(cancellationToken);

                //mVM.observacion.Estado = await _context.TMaestro
                //    .Where(om => om.Estado && om.CodTabla.Equals("ESTADO"))
                //    .ProjectTo<CampoDto>(_mapper.ConfigurationProvider)
                //    .OrderBy(t => t.Descripcion)
                //    .ToListAsync(cancellationToken);

                //mVM.observacion.HHARelacionada = await _context.TMaestro
                //    .Where(om => om.Estado && om.CodTabla.Equals("HHARELACIONADA"))
                //    .ProjectTo<CampoDto>(_mapper.ConfigurationProvider)
                //    .OrderBy(t => t.Descripcion)
                //    .ToListAsync(cancellationToken);

                //mVM.observacion.NivelRiesgo = await _context.TMaestro
                //    .Where(om => om.Estado && om.CodTabla.Equals("NIVELRIESGO"))
                //    .ProjectTo<CampoDto>(_mapper.ConfigurationProvider)
                //    .OrderBy(t => t.Descripcion)
                //    .ToListAsync(cancellationToken);

                //mVM.observacion.StopWork = await _context.TMaestro
                //    .Where(om => om.Estado && om.CodTabla.Equals("TOSW"))
                //    .ProjectTo<CampoDto>(_mapper.ConfigurationProvider)
                //    .OrderBy(t => t.Descripcion)
                //    .ToListAsync(cancellationToken);

                return mVM;
            }

        }
    }
}
