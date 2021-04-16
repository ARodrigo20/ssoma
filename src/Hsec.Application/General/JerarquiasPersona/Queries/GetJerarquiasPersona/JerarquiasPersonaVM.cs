using AutoMapper;
using Hsec.Domain.Entities;
using System.Collections.Generic;

namespace Hsec.Application.General.JerarquiasPersona.Queries.GetJerarquiaPersona
{
    public class JerarquiasPersonaVM
    {
        public IList<JerarquiasPersonaNodeVM> Data { get; set; }
        public int count { get; set; }
    }
}
