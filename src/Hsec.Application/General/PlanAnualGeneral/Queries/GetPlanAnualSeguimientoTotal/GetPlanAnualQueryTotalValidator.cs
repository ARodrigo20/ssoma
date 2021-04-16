using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.General.PlanAnualGeneral.Queries.GetPlanAnualSeguimientoTotal
{
    public class GetPlanAnualSeguimientoTotalQueryValidator : AbstractValidator<GetPlanAnualSeguimientoTotalQuery>
    {
        public GetPlanAnualSeguimientoTotalQueryValidator()
        {
            RuleFor(v => v.filtros.Anio)
                .Matches("^(19[0-9]{2}|2[0-9]{3})$");
            RuleFor(v => v.filtros.Mes)
                .Matches("^([1-9]|[1][0-2]?)$");
            //RuleFor(v => v.Comportamiento.CodStopWork).Custom((t,a)=>t.is)
        }
    }
}
