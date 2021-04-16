using System.Collections.Generic;
using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities;
using Hsec.Domain.Enums;
namespace Hsec.Application.VerificacionControlCritico.Queries.BuscarVerificacionControlCritico
{
    public class BuscarVerificacionCCVM 
    {
        
        public IList<ItemBVCCDto> List {get;set;}

        public int Pagina { get; set; }
        public int Count { get; set; }

    }
}