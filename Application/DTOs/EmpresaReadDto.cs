namespace APIUsuarios.Application.DTOs;

public record EmpresaReadDto(
    int Id,
    string RazaoSocial,
    string? NomeFantasia,
    string CNPJ,
    string Email,
    string? Telefone,
    bool Ativo,
    DateTime DataCriacao
);
