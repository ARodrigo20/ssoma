using FluentValidation;
using Hsec.Application.General.PlanAnualGeneral.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.General.PlanAnualGeneral.Commands.UpdatePlanAnualGeneral
{
    public class UpdatePlanAnualGeneralVMValidator : AbstractValidator<UpdatePlanAnualGeneralVM>
    {
        public UpdatePlanAnualGeneralVMValidator()
        {
            RuleFor(v => v.Anio)
                .Matches("^(19[0-9]{2}|2[0-9]{3})$");
            RuleFor(v => v.Mes)
                .Matches("^([1-9]|[1][0-2]?)$");
            
            RuleForEach(v => v.ListPersonas).SetValidator(new PersonaDtoValidator());
        }
    }

    public class PersonaDtoValidator : AbstractValidator<PersonaDto>
    {
        public PersonaDtoValidator(){
            RuleFor(v => v.CodPersona)
                .NotNull().NotEmpty();
            RuleForEach(v => v.ListCodigos)
                .SetValidator(new PlanReferenciaDtoValidator());
        }
    }

    public class PlanReferenciaDtoValidator : AbstractValidator<PlanReferenciaDto>
    {
        public PlanReferenciaDtoValidator(){
            RuleFor(v => v.CodReferencia)
                .NotNull().NotEmpty();
            RuleFor(v => v.Valor)
                .Matches("^\\d+$");
        }
    }
}
