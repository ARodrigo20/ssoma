using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Otros;
using System;
using System.Collections.Generic;
using Hsec.Application.General.Personas.Queries.GetPersona;

namespace Hsec.Application.Comite.Queries.ComiteGetByCod
{
    public class ComiteGetDto : IMapFrom<TComite>
    {
        public string CodComite { get; set; }

        public DateTime Fecha { get; set; }

        public string Hora { get; set; }

        public string CodTipoComite { get; set; }

        public string CodCategoria { get; set; }

        public string CodPosicionGer { get; set; }

        public string CodPosicionSup { get; set; }

        public string Lugar { get; set; }

        public string DetalleApertura { get; set; }

        public string CodSecretario { get; set; }

        public string ResumenSalud { get; set; }

        public string ResumenSeguridad { get; set; }

        public string ResumenMedioAmbiente { get; set; }

        public string ResumenComunidad { get; set; }

        public DateTime FechaCierre { get; set; }

        public string HoraCierre { get; set; }

        public ICollection<PersonaVM> ListaParticipantes { get; set; }

        public ComiteGetDto()
        {
            ListaParticipantes = new HashSet<PersonaVM>();
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ComiteGetDto, TComite>();
            profile.CreateMap<TComite, ComiteGetDto>();
        }
    }
}
