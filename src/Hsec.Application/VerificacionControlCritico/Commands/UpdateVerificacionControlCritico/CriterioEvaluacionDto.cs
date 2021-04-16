using Hsec.Application.Common.Models;
using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.VerficacionesCc;
using Hsec.Domain.Enums;

namespace Hsec.Application.VerificacionControlCritico.Commands.UpdateVerificacionControlCritico
{
  public class CriterioEvaluacionDto : IMapFrom<TVerificacionControlCriticoCartilla> 
  {
    public string CodigoVCC { get; set; }
    public string CodCartilla { get; set; }
    public string CodCC { get; set; }
    public string CC { get; set; }
    public string Cumplimiento { get; set; }
    public string Efectividad { get; set; }
    public string Justificacion { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CriterioEvaluacionDto, TVerificacionControlCriticoCartilla>();
    }

  }
}

