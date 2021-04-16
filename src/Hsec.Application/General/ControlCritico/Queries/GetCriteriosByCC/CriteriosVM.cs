using Hsec.Application.General.ControlCritico.Models;
using System.Collections.Generic;

namespace Hsec.Application.General.ControlCritico.Queries.GetCriteriosByCC
{
    public class CriteriosVM
    {
        public ICollection<CriterioDto> list { get; set; }
        public int size { get; set; }
    }
}