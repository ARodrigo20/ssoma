using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.General.Personas.Queries.GetBuscarPersonas
{
    class BuscarPersonasQueryValidator : AbstractValidator<GetBuscarPersonasQuery>
    {
        public BuscarPersonasQueryValidator()
        {
            RuleFor(v => v.Pagina).GreaterThan(0);
            RuleFor(v => v.PaginaTamanio).GreaterThan(0);
        }
    }
}
