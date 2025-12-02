using APIUsuarios.Application.DTOs;
using FluentValidation;

namespace APIUsuarios.Application.Validators;

public class UsuarioCreateDtoValidator : AbstractValidator<UsuarioCreateDto>
{
    public UsuarioCreateDtoValidator()
    {
        RuleFor(u => u.Nome)
            .NotEmpty()
            .Length(3, 100)
            .WithMessage("O nome é obrigatório e deve ter entre 3 e 100 caracteres.");

        RuleFor(u => u.DataNascimento)
            .NotEmpty()
            .Must(d => d <= DateTime.Now.AddYears(-18))
            .WithMessage("O usuário deve ter pelo menos 18 anos.");

        RuleFor(u => u.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Um email válido é obrigatório.");

        RuleFor(u => u.Senha)
            .NotEmpty()
            .MinimumLength(6)
            .WithMessage("A senha deve ter no mínimo 6 caracteres.");
            
        RuleFor(u => u.Telefone)
            .Matches(@"^\(\d{2}\) \d{5}-\d{4}$")
            .When(u => !string.IsNullOrEmpty(u.Telefone))
            .WithMessage("O telefone deve estar no formato (XX) XXXXX-XXXX.");
    }
}