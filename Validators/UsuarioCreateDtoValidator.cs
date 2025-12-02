using APIUsuarios.Application.DTOs;
using FluentValidation;

namespace APIUsuarios.Validators;

public class UsuarioCreateDtoValidator : AbstractValidator<UsuarioCreateDto>
{
    public UsuarioCreateDtoValidator()
    {
        RuleFor(u => u.Nome)
            .NotEmpty()
            .WithMessage("O nome é obrigatório.");
        RuleFor(u => u.DataNascimento)
            .NotEmpty()
            .WithMessage("O usuário deve ter pelo menos 18 anos.");
        RuleFor(u => u.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Um email válido é obrigatório.");
        RuleFor(u => u.Senha)
            .NotEmpty()
            .MinimumLength(6)
            .WithMessage("A senha deve ter no mínimo 6 caracteres.");
        
    }
}