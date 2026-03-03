namespace APIUsuarios.Application.DTOs;

public record EmpresaCreateDto(
    string RazaoSocial,
    string? NomeFantasia,
    string CNPJ,
    string Email,
    string? Telefone
);
