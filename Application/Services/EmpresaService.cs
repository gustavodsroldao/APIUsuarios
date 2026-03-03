using APIUsuarios.Application.DTOs;
using APIUsuarios.Domain.Entities;
using APIUsuarios.Application.Interfaces;

namespace APIUsuarios.Application.Services;

public class EmpresaService : IEmpresaService
{
    private readonly IEmpresaRepository _repo;

    public EmpresaService(IEmpresaRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<EmpresaReadDto>> ListarAsync(CancellationToken ct)
    {
        var empresas = await _repo.GetAllAsync(ct);
        return empresas.Select(e => e.ToReadDto());
    }

    public async Task<EmpresaReadDto?> ObterAsync(int id, CancellationToken ct)
    {
        var empresa = await _repo.GetByIdAsync(id, ct);
        return empresa?.ToReadDto();
    }

    public async Task<EmpresaReadDto> CriarAsync(EmpresaCreateDto dto, CancellationToken ct)
    {
        if (await _repo.CNPJExistsAsync(dto.CNPJ, ct))
            throw new InvalidOperationException("CNPJ já cadastrado.");

        if (await _repo.EmailExistsAsync(dto.Email, ct))
            throw new InvalidOperationException("Email já cadastrado.");

        var empresa = new Empresa
        {
            RazaoSocial = dto.RazaoSocial,
            NomeFantasia = dto.NomeFantasia,
            CNPJ = dto.CNPJ,
            Email = dto.Email.ToLower(),
            Telefone = dto.Telefone,
            Ativo = true,
            DataCriacao = DateTime.Now
        };

        await _repo.AddAsync(empresa, ct);
        await _repo.SaveChangesAsync(ct);

        return empresa.ToReadDto();
    }

    public async Task<EmpresaReadDto?> AtualizarAsync(int id, EmpresaUpdateDto dto, CancellationToken ct)
    {
        var empresa = await _repo.GetByIdAsync(id, ct);
        if (empresa == null) return null;

        if (empresa.CNPJ != dto.CNPJ && await _repo.CNPJExistsAsync(dto.CNPJ, ct))
            throw new InvalidOperationException("CNPJ já cadastrado.");

        if (empresa.Email.ToLower() != dto.Email.ToLower() && await _repo.EmailExistsAsync(dto.Email, ct))
            throw new InvalidOperationException("Email já cadastrado.");

        empresa.RazaoSocial = dto.RazaoSocial;
        empresa.NomeFantasia = dto.NomeFantasia;
        empresa.CNPJ = dto.CNPJ;
        empresa.Email = dto.Email.ToLower();
        empresa.Telefone = dto.Telefone;
        empresa.Ativo = dto.Ativo;
        empresa.DataAtualizacao = DateTime.Now;

        await _repo.UpdateAsync(empresa, ct);
        await _repo.SaveChangesAsync(ct);

        return empresa.ToReadDto();
    }

    public async Task<bool> RemoverAsync(int id, CancellationToken ct)
    {
        var empresa = await _repo.GetByIdAsync(id, ct);
        if (empresa == null) return false;

        empresa.Ativo = false; // Soft delete
        empresa.DataAtualizacao = DateTime.Now;

        await _repo.UpdateAsync(empresa, ct);
        await _repo.SaveChangesAsync(ct);
        return true;
    }
}
