using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.General;
using System;

namespace Hsec.Application.General.Aprobaciones.Queries.GetHistorialAprobacion
{
  public class HAprobacionDto : IMapFrom<THistorialAprobacion>
  {
    public string DocReferencia { get; set; }
    public int CodAprobacion { get; set; }
    public string CodCadena { get; set; }
    public int Version { get; set; }
    public string CodAprobador { get; set; }
    public string Aprobador { get; set; }
    public string Email { get; set; }
    public string Cargo { get; set; }
    public string Comentario { get; set; }
    public string EstadoAprobacion { get; set; }
    public DateTime FechaCreacion { get; set; }
    public bool Editable { get; set; }
        
    public void Mapping(Profile profile)
    {
        profile.CreateMap<THistorialAprobacion,HAprobacionDto>()
        .ForMember( t => t.FechaCreacion , o => o.MapFrom( t => t.Creado ));
    }
  }
}