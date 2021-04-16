using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Capacitaciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.PerfilCapacitacionPosicion.Command.ExtraerPopUp.VMs
{
    public class ExtraerPopUpCursoPosicionResponseVM : IMapFrom<TTemaCapacitacion>
    {
        public string competencia { get; set; }
        public string codTemaCapacita { get; set; }
        public string curso { get; set; }
        public string codTipo { get; set; }
        public string codHha { get; set; } //codHH

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TTemaCapacitacion, ExtraerPopUpCursoPosicionResponseVM>()
            .ForMember(i => i.competencia, opt => opt.MapFrom(t => t.CompetenciaHs))
            .ForMember(i => i.codTemaCapacita, opt => opt.MapFrom(t => t.CodTemaCapacita))
            .ForMember(i => i.curso, opt => opt.MapFrom(t => t.Descripcion))
            .ForMember(i => i.codTipo, opt => opt.MapFrom(t => t.CodTipoTema))
            .ForMember(i => i.codHha, opt => opt.MapFrom(t => t.CodHha))
            ;
        }
    }
}
