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
}
