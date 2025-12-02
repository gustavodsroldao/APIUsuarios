using APIUsuarios.Application.DTOs;
using APIUsuarios.Domain.Entities;
using APIUsuarios.Application.Interfaces;

namespace APIUsuarios.Application.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _repo;
    
    public UsuarioService(IUsuarioRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<UsuarioReadDto>> ListarAsync(CancellationToken ct)
    {
        var usuarios = await _repo.GetAllAsync(ct);
        return usuarios.Select(u => u.ToReadDto());
    }

    public async Task<UsuarioReadDto?> ObterAsync(int id, CancellationToken ct = default)
    {
        var usuario = await _repo.GetByIdAsync(id, ct);
        return usuario?.ToReadDto();
    }

    public async Task<UsuarioReadDto> CriarAsync(UsuarioCreateDto dto, CancellationToken ct)
    {
        if (await _repo.EmailExistsAsync(dto.Email, ct))
        {
            throw new InvalidOperationException("Email já cadastrado.");
        }

        var usuario = new Usuario
        {
            Nome = dto.Nome,
            Email = dto.Email.ToLower(),
            Senha = dto.Senha, // In a real app, hash this!
            DataNascimento = dto.DataNascimento,
            Telefone = dto.Telefone,
            Ativo = true,
            DataCriacao = DateTime.Now
        };

        await _repo.AddAsync(usuario, ct);
        await _repo.SaveChangesAsync(ct);

        return usuario.ToReadDto();
    }

    public async Task<UsuarioReadDto> AtualizarAsync(int id, UsuarioUpdateDto dto, CancellationToken ct)
    {
        var usuario = await _repo.GetByIdAsync(id, ct);
        if (usuario == null) return null;

        if (usuario.Email.ToLower() != dto.Email.ToLower())
        {
            if (await _repo.EmailExistsAsync(dto.Email, ct))
            {
                 throw new InvalidOperationException("Email já cadastrado.");
            }
        }

        usuario.Nome = dto.Nome;
        usuario.Email = dto.Email.ToLower();
        usuario.DataNascimento = dto.DataNascimento;
        usuario.Telefone = dto.Telefone;
        usuario.Ativo = dto.Ativo;
        usuario.DataAtualizacao = DateTime.Now;

        await _repo.UpdateAsync(usuario, ct);
        await _repo.SaveChangesAsync(ct);

        return usuario.ToReadDto();
    }

    public async Task<bool> RemoverAsync(int id, CancellationToken ct)
    {
        var usuario = await _repo.GetByIdAsync(id, ct);
        if (usuario == null) return false;

        usuario.Ativo = false; // Soft delete
        usuario.DataAtualizacao = DateTime.Now;
        
        await _repo.UpdateAsync(usuario, ct);
        await _repo.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> EmailJaCadastradoAsync(string email, CancellationToken ct)
    {
        return await _repo.EmailExistsAsync(email, ct);
    }
}