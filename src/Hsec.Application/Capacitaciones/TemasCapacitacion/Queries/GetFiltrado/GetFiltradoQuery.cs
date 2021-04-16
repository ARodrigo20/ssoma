using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Capacitaciones.TemasCapacitacion.Queries.GetFiltrado.DTOs;
using Hsec.Application.Capacitaciones.TemasCapacitacion.Queries.GetFiltrado.VMs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Capacitaciones.TemasCapacitacion.Queries.GetFiltrado
{
    public class GetFiltradoQuery : IRequest<GetFiltradoResponseCabVM>
    {
        public GetFiltradoRequestVM temaCapacitacionVM { get; set; }
        public class GetFiltradoQueryHandler : IRequestHandler<GetFiltradoQuery, GetFiltradoResponseCabVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetFiltradoQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<GetFiltradoResponseCabVM> Handle(GetFiltradoQuery request, CancellationToken cancellationToken)
            {
                GetFiltradoResponseCabVM modelListVMReq = new GetFiltradoResponseCabVM();
                GetFiltradoRequestVM modelVMReq = request.temaCapacitacionVM;

                //var count = _context.TTemaCapacitacion.Where(i => i.Estado &&
                // (String.IsNullOrEmpty(modelVMReq.codTemaCapacita) || i.CodTemaCapacita.EndsWith(modelVMReq.codTemaCapacita)) &&
                // (String.IsNullOrEmpty(modelVMReq.codTipoTema) || i.CodTipoTema.Contains(modelVMReq.codTipoTema)) &&
                // (String.IsNullOrEmpty(modelVMReq.codAreaCapacita) || i.CodAreaCapacita.Contains(modelVMReq.codAreaCapacita)) &&
                // (String.IsNullOrEmpty(modelVMReq.descripcion) || i.Descripcion.Contains(modelVMReq.descripcion)) &&
                // (String.IsNullOrEmpty(modelVMReq.competenciaHs) || i.CompetenciaHs.Contains(modelVMReq.competenciaHs)) &&
                // (String.IsNullOrEmpty(modelVMReq.codHha) || i.CodHha.Contains(modelVMReq.codHha))).Where(i => i.Estado).Count();

                var temaCap = _context.TTemaCapacitacion.Where(i => i.Estado &&
                (String.IsNullOrEmpty(modelVMReq.codTemaCapacita) || i.CodTemaCapacita.EndsWith(modelVMReq.codTemaCapacita)) &&
                (String.IsNullOrEmpty(modelVMReq.codTipoTema) || i.CodTipoTema.Contains(modelVMReq.codTipoTema)) &&
                (String.IsNullOrEmpty(modelVMReq.codAreaCapacita) || i.CodAreaCapacita.Contains(modelVMReq.codAreaCapacita)) &&
                (String.IsNullOrEmpty(modelVMReq.descripcion) || i.Descripcion.Contains(modelVMReq.descripcion)) &&
                (String.IsNullOrEmpty(modelVMReq.competenciaHs) || i.CompetenciaHs.Contains(modelVMReq.competenciaHs)) &&
                (String.IsNullOrEmpty(modelVMReq.codHha) || i.CodHha.Contains(modelVMReq.codHha))).Include(i => i.TemaCapEspecifico).OrderBy(o=>o.CodTipoTema).ThenByDescending(o => o.CodTemaCapacita);

                IList<GetFiltradoResponseVM> objeto = temaCap
                .ProjectTo<GetFiltradoResponseVM>(_mapper.ConfigurationProvider)
                .ToList();

                GetFiltradoResponseVM tem;
                if (modelVMReq.temaCapEspecifico.Count > 0) {
                    foreach (var obj in objeto)
                    {
                        tem = new GetFiltradoResponseVM();
                        tem = obj;
                        if (tem.temaCapEspecifico.Count > 0)
                        {
                            var valor = tem.temaCapEspecifico.Where(i => i.estado &&
                            (modelVMReq.temaCapEspecifico.Any(y => i.codPeligro.Contains(y.codPeligro) || string.IsNullOrEmpty(y.codPeligro))) &&
                            (modelVMReq.temaCapEspecifico.Any(y => i.codRiesgo.Contains(y.codRiesgo) || string.IsNullOrEmpty(y.codRiesgo))) &&
                            (modelVMReq.temaCapEspecifico.Any(y => i.codTemaCapacita.EndsWith(y.codTemaCapacita) || string.IsNullOrEmpty(y.codTemaCapacita))) &&
                            ((modelVMReq.temaCapEspecifico.Any(y => (i.correlativo == y.correlativo) || (y.correlativo == 0)))
                            )).ToList();

                            if (valor.Count > 0)
                            {
                                tem.temaCapEspecifico = valor;
                                modelListVMReq.data.Add(tem);
                            }
                        }                       
                    }
                }
                else
                {
                    foreach (var obj in objeto)
                    {
                        //if (obj.temaCapEspecifico.Count > 0) 
                        //{
                        //    var valoresTemCap = obj.temaCapEspecifico.Where(i => i.estado).ToList();
                        //    obj.temaCapEspecifico = valoresTemCap;
                        //}
                        modelListVMReq.data.Add(obj);
                    }
                }
                modelListVMReq.count = modelListVMReq.data.Count;
                return modelListVMReq;
            }
        }
    }
}