using System.Collections.Generic;

namespace Hsec.Application.VerificacionControlCritico.Queries.AvancePorCartilla
{
    public class AvancePorCartillaVM
    {
        public ICollection<AvenceDto> list { get; set; }

        public AvancePorCartillaVM()
        {
            list = new HashSet<AvenceDto>();
        }
    }
}