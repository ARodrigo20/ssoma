using Hsec.Application.Inspeccion.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Inspeccion.Queries.GetInspeccion
{
    public class GetInspeccionVM
    {
        public InspeccionDto Inspeccion { get; set; }
        public ICollection<DetalleInspeccionDto> DetalleInspeccion { get; set; }
        public ICollection<EquipoDto> EquipoInspeccion { get; set; }
        public ICollection<AtendidosDto> PersonasAtendidas { get; set; }
        public ICollection<InspeccionAnalisisCausaDto> AnalisisCausa { get; set; }
    }
}