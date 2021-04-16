using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Hsec.Application.Common.Interfaces;
using Hsec.Domain.Entities.Observaciones;

namespace Hsec.Application.Observacion.Commands.UpdateObservacion {
    internal class Covid19SubHandle : Strategia {
        public string _VCCCod { get; set; }
        private readonly string RESPUESTA_INCORRECTA = "R002";
        public Covid19SubHandle (IApplicationDbContext context, IMapper mapper) : base (context, mapper) { }

        public override void UpsertSubTipo(ObservacionDto data)
        {
            string CodObservacion = data.CodObservacion;
            var VCCRequest = data.Covid19;
            TObservacionVerControlCritico vcc = _context.TObservacionVerControlCritico.FirstOrDefault(o => o.CodObservacion.Equals(CodObservacion));
            if (vcc == null)
            {
                vcc = new TObservacionVerControlCritico();
                _mapper.Map<VerificacionControlCriticoDto, TObservacionVerControlCritico>(VCCRequest, vcc);
                vcc.CodObservacion = CodObservacion;
                _VCCCod = nextCodVCC();
                vcc.CodVcc = _VCCCod;
                //vcc.Estado = true;
                _context.TObservacionVerControlCritico.Add(vcc);
            }
            else
            {
                vcc = _mapper.Map<VerificacionControlCriticoDto, TObservacionVerControlCritico>(VCCRequest, vcc);
                vcc.Estado = true;
                _VCCCod = vcc.CodVcc;
                _context.TObservacionVerControlCritico.Update(vcc);
            }
        }
        public override void CreateDetalleSubTipo(ObservacionDto dataObs)
        {

            ICollection<string> herramientas = dataObs.Covid19.Herramientas;
            foreach (var item in herramientas)
            {
                var data = new TObsVCCHerramienta();
                data.CodDesHe = item;
                data.CodVcc = _VCCCod;
                _context.TObsVCCHerramienta.Add(data);
            }
            ICollection<CriterioDto> Criterios = dataObs.Covid19.Criterios;

            var ControlCritico = Criterios.GroupBy(t => t.CodCC).Select(t => t.Key).ToList();
            foreach (var item in ControlCritico)
            {
                var data = new TObsVCCVerCCEfectividad();
                data.CodCC = item;
                data.CodVcc = _VCCCod;
                data.CodCartilla = dataObs.Covid19.CodCartilla;
                data.Efectividad = efectividad(Criterios).ToString();
                _context.TObsVCCVerCCEfectividad.Add(data);
            }

            // foreach (var item in Criterios) {
            //     var data = _mapper.Map<CriterioDto, TObsVCCVerCCEfectividad> (item);
            //     data.CodVcc = _VCCCod;
            //     data.CodCartilla = dataObs.Covid19.CodCartilla;
            //     _context.TObsVCCVerCCEfectividad.Add (data);
            // }
            foreach (var item in Criterios)
            {
                var data = _mapper.Map<CriterioDto, TObsVCCRespuesta>(item);
                data.CodVcc = _VCCCod;
                _context.TObsVCCRespuesta.Add(data);
            }
            ICollection<CierreInteraccionDto> CierreInteraccion = dataObs.Covid19.CierreInteracion;
            foreach (var item in CierreInteraccion)
            {
                var data = _mapper.Map<CierreInteraccionDto, TObsVCCCierreIteraccion>(item);
                data.CodVcc = _VCCCod;
                _context.TObsVCCCierreIteraccion.Add(data);
            }

        }
        public override void DeleteSubtipo(string COD_OBSERVACION)
        {
            TObservacionVerControlCritico obs_vcc = _context.TObservacionVerControlCritico.Where(t => t.CodObservacion.Equals(COD_OBSERVACION)).FirstOrDefault();
            if (obs_vcc != null)
            {
                obs_vcc.Estado = false;
                _context.TObservacionVerControlCritico.Update(obs_vcc);
            }
        }
        public override void DeleteDetalleSubtipo(string COD_OBSERVACION)
        {
            TObservacionVerControlCritico obs_vcc = _context.TObservacionVerControlCritico.Where(t => t.CodObservacion.Equals(COD_OBSERVACION)).FirstOrDefault();
            if (obs_vcc != null)
            {
                string CODIGO_VCC = obs_vcc.CodVcc;
                var list_Herramienta = _context.TObsVCCHerramienta.Where(t => t.CodVcc.Equals(CODIGO_VCC)).ToList();
                _context.TObsVCCHerramienta.RemoveRange(list_Herramienta);
                var list_VerCCEfectividad = _context.TObsVCCVerCCEfectividad.Where(t => t.CodVcc.Equals(CODIGO_VCC)).ToList();
                _context.TObsVCCVerCCEfectividad.RemoveRange(list_VerCCEfectividad);
                var list_Respuesta = _context.TObsVCCRespuesta.Where(t => t.CodVcc.Equals(CODIGO_VCC)).ToList();
                _context.TObsVCCRespuesta.RemoveRange(list_Respuesta);
                var list_CierreIteraccion = _context.TObsVCCCierreIteraccion.Where(t => t.CodVcc.Equals(CODIGO_VCC)).ToList();
                _context.TObsVCCCierreIteraccion.RemoveRange(list_CierreIteraccion);
            }
        }


        public string nextCodVCC()
        {
            /**
                la funcion se repite en 
                src\Hsec.Application\Observacion\Commands\CreateObservacion\CreateObservacionCommand.cs
                src\Hsec.Application\Observacion\Commands\UpdateObservacion\commands\Covid19SubHandle.cs
                src\Hsec.Application\Observacion\Commands\UpdateObservacion\commands\VerificacionControlCriticoSubHandle.cs
            */

            var COD_INCIDENTE_MAX = _context.TObservacionVerControlCritico.Max(t => t.CodVcc);
            if (COD_INCIDENTE_MAX == null) COD_INCIDENTE_MAX = "OBV00000001";
            //  OBV00000001
            // COD_INCIDENTE_MAX = String.Format("OBV{0,8:00000000}", max);
            else
            {
                string numberStr = COD_INCIDENTE_MAX.Substring(3);
                int max = Int32.Parse(numberStr) + 1;
                COD_INCIDENTE_MAX = String.Format("OBV{0,10:00000000}", max);
            }
            return COD_INCIDENTE_MAX;
        }

        private int efectividad(ICollection<CriterioDto> criterios)
        {
            foreach (var item in criterios)
            {
                if (item.Respuesta.Equals(RESPUESTA_INCORRECTA)) return 0;
            }
            return 1;
        }
    }

}