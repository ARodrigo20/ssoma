using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.General;
using Hsec.Domain.Enums;
namespace Hsec.Application.General.PlanAnualGeneral.Commands.UpdatePlanAnualGeneral
{
    public class UpdateDto : IMapFrom<TPlanAnualGeneral>
    {
        public string Anio { get; set; }
        public string CodMes { get; set; }
        public string CodPersona { get; set; }
        public string CodReferencia { get; set; }
        public string Valor { get; set; }

        public UpdateDto setAnio(string Anio){
            this.Anio = Anio;
            return this;
        }
        public UpdateDto setCodMes(string CodMes){
            this.CodMes = CodMes;
            return this;
        }
        public UpdateDto setCodPersona(string CodPersona){
            this.CodPersona = CodPersona;
            return this;
        }
        public UpdateDto setCodReferencia(string CodReferencia){
            this.CodReferencia = CodReferencia;
            return this;
        }
        public UpdateDto setValor(string Valor){
            this.Valor = Valor;
            return this;
        }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateDto, TPlanAnualGeneral>();
        }
    }
}

