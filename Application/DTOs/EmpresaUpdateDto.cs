namespace APIUsuarios.Application.DTOs;

public record EmpresaUpdateDto(
    string RazaoSocial,
    string? NomeFantasia,
    string CNPJ,
    string Email,
    string? Telefone,
    bool Ativo
);
