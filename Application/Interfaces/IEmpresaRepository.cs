using APIUsuarios.Domain.Entities;

namespace APIUsuarios.Application.Interfaces;

public interface IEmpresaRepository
{
    Task<IEnumerable<Empresa>> GetAllAsync(CancellationToken ct);

    Task<Empresa?> GetByIdAsync(int id, CancellationToken ct);

    Task<Empresa?> GetByCNPJAsync(string cnpj, CancellationToken ct);

    Task AddAsync(Empresa empresa, CancellationToken ct);

    Task UpdateAsync(Empresa empresa, CancellationToken ct);

    Task RemoveAsync(Empresa empresa, CancellationToken ct);

    Task<bool> CNPJExistsAsync(string cnpj, CancellationToken ct);

    Task<bool> EmailExistsAsync(string email, CancellationToken ct);

    Task SaveChangesAsync(CancellationToken ct);
}
