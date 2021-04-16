using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.General;

namespace Hsec.Application.General.AnalisisCausas.Queries.GetAnalisisCausaPartner
{
  
    public class AnalisisCausaVM : IMapFrom<TAnalisisCausa>
    {              
        public string CodAnalisis { get; set; }
        public string Descripcion { get; set; }       

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TAnalisisCausa, AnalisisCausaVM>();               
        }
    }
}
