using APIUsuarios.Application.DTOs;
using APIUsuarios.Domain.Entities;

namespace APIUsuarios.Application.Services;

public static class MappingExtensions
{
    public static UsuarioReadDto? ToReadDto(this Usuario u)
    {
        if (u == null) return null;
        
            return new UsuarioReadDto(
                Id: u.Id,  
                Nome: u.Nome,
                Email: u.Email,
                DataNascimento: u.DataNascimento,
                Telefone: u.Telefone,
                Ativo: u.Ativo,
                DataCriacao: u.DataCriacao
                
            );
    }

    public static EmpresaReadDto? ToReadDto(this Empresa e)
    {
        if (e == null) return null;

        return new EmpresaReadDto(
            Id: e.Id,
            RazaoSocial: e.RazaoSocial,
            NomeFantasia: e.NomeFantasia,
            CNPJ: e.CNPJ,
            Email: e.Email,
            Telefone: e.Telefone,
            Ativo: e.Ativo,
            DataCriacao: e.DataCriacao
        );
    }
}
