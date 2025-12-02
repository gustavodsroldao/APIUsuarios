using APIUsuarios.Application.DTOs;
using APIUsuarios.Interfaces;

namespace APIUsuarios.Services;

public class UsuarioService : IUsuarioService
{
    public Task<IEnumerable<UsuarioReadDto>> ListarAsync(CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<UsuarioReadDto?> ObterAsync(int id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<UsuarioReadDto> CriarAsync(UsuarioCreateDto dto, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<UsuarioReadDto> AtualizarAsync(int id, UsuarioUpdateDto dto, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoverAsync(int id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<bool> EmailJaCadastradoAsync(string email, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}