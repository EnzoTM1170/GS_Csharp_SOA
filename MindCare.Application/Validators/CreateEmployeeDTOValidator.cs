using FluentValidation;
using MindCare.Application.DTOs;
using System.Text.RegularExpressions;

namespace MindCare.Application.Validators;

public class CreateEmployeeDTOValidator : AbstractValidator<CreateEmployeeDTO>
{
    public CreateEmployeeDTOValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Nome é obrigatório")
            .MinimumLength(3).WithMessage("Nome deve ter no mínimo 3 caracteres")
            .MaximumLength(100).WithMessage("Nome deve ter no máximo 100 caracteres")
            .Matches(@"^[a-zA-ZÀ-ÿ\s]+$").WithMessage("Nome deve conter apenas letras");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email é obrigatório")
            .EmailAddress().WithMessage("Email inválido")
            .MaximumLength(255).WithMessage("Email deve ter no máximo 255 caracteres");

        RuleFor(x => x.Department)
            .NotEmpty().WithMessage("Departamento é obrigatório")
            .MaximumLength(100).WithMessage("Departamento deve ter no máximo 100 caracteres");

        RuleFor(x => x.Position)
            .NotEmpty().WithMessage("Cargo é obrigatório")
            .MaximumLength(100).WithMessage("Cargo deve ter no máximo 100 caracteres");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Telefone é obrigatório")
            .Matches(@"^[\d\s\(\)\-]+$").WithMessage("Telefone inválido")
            .MinimumLength(10).WithMessage("Telefone deve ter no mínimo 10 caracteres");
    }
}

