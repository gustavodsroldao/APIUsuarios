using APIUsuarios.Application.DTOs;
using FluentValidation;

namespace APIUsuarios.Application.Validators;

public class EmpresaCreateDtoValidator : AbstractValidator<EmpresaCreateDto>
{
    public EmpresaCreateDtoValidator()
    {
        RuleFor(e => e.RazaoSocial)
            .NotEmpty()
            .Length(3, 150)
            .WithMessage("A razão social é obrigatória e deve ter entre 3 e 150 caracteres.");

        RuleFor(e => e.CNPJ)
            .NotEmpty()
            .Matches(@"^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$")
            .WithMessage("O CNPJ deve estar no formato XX.XXX.XXX/XXXX-XX.");

        RuleFor(e => e.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Um email válido é obrigatório.");

        RuleFor(e => e.Telefone)
            .Matches(@"^\(\d{2}\) \d{5}-\d{4}$")
            .When(e => !string.IsNullOrEmpty(e.Telefone))
            .WithMessage("O telefone deve estar no formato (XX) XXXXX-XXXX.");
    }
}
