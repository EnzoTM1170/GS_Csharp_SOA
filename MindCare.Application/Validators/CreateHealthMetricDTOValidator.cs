using FluentValidation;
using MindCare.Application.DTOs;
using MindCare.Domain.Enums;

namespace MindCare.Application.Validators;

public class CreateHealthMetricDTOValidator : AbstractValidator<CreateHealthMetricDTO>
{
    public CreateHealthMetricDTOValidator()
    {
        RuleFor(x => x.EmployeeId)
            .GreaterThan(0).WithMessage("ID do funcionário deve ser maior que zero");

        RuleFor(x => x.Type)
            .IsInEnum().WithMessage("Tipo de métrica inválido");

        RuleFor(x => x.Value)
            .GreaterThanOrEqualTo(0).WithMessage("Valor deve ser maior ou igual a zero")
            .LessThanOrEqualTo(1000).WithMessage("Valor deve ser menor ou igual a 1000");

        RuleFor(x => x.Unit)
            .NotEmpty().WithMessage("Unidade é obrigatória")
            .MaximumLength(20).WithMessage("Unidade deve ter no máximo 20 caracteres");

        RuleFor(x => x.Source)
            .IsInEnum().WithMessage("Fonte da métrica inválida");

        RuleFor(x => x.RecordedAt)
            .NotEmpty().WithMessage("Data de registro é obrigatória")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Data não pode ser no futuro");
    }
}

