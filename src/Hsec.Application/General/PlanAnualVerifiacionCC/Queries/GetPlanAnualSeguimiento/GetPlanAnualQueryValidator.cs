using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.General.PlanAnualVerifiacionCC.Queries.GetPlanAnualSeguimiento
{
    public class GetPlanAnualSeguimientoQueryValidator : AbstractValidator<GetPlanAnualSeguimientoQuery>
    {
        public GetPlanAnualSeguimientoQueryValidator()
        {
            RuleFor(v => v.filtros.Anio)
                .Matches("^(19[0-9]{2}|2[0-9]{3})$");
            RuleFor(v => v.filtros.Mes)
                .Matches("^([1-9]|[1][0-2]?)$");
            RuleFor(v => v.filtros.Pagina)
                .NotNull()
                .NotEmpty()
                .GreaterThanOrEqualTo(1);
            RuleFor(v => v.filtros.PaginaTamanio)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0);
            //RuleFor(v => v.Comportamiento.CodStopWork).Custom((t,a)=>t.is)
        }
    }
}
