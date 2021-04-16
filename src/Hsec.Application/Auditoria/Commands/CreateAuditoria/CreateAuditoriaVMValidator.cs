using FluentValidation;


namespace Hsec.Application.Auditoria.Commands.CreateAuditoria
{
    public class CreateAuditoriaVMValidator : AbstractValidator<CreateAuditoriaVM>
    {
        public CreateAuditoriaVMValidator()
        {
            RuleFor(v => v.JSONAuditoria );
        }
    }
}

    // public class PersonaDtoValidator : AbstractValidator<PersonaDto>
    // {
    //     public PersonaDtoValidator(){
    //         RuleFor(v => v.CodPersona)
    //             .NotNull().NotEmpty();
    //         RuleForEach(v => v.ListCodigos)
    //             .SetValidator(new PlanReferenciaDtoValidator());
    //     }
    // }

    // public class PlanReferenciaDtoValidator : AbstractValidator<PlanReferenciaDto>
    // {
    //     public PlanReferenciaDtoValidator(){
    //         RuleFor(v => v.CodReferencia)
    //             .NotNull().NotEmpty();
    //         RuleFor(v => v.Valor)
    //             .Matches("^\\d+$");
    //     }
    // }
