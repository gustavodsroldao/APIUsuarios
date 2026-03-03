using APIUsuarios.Domain.Entities;
using APIUsuarios.Infrastructure.Persistence;
using APIUsuarios.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APIUsuarios.Infrastructure.Repositories;

public class EmpresaRepository : IEmpresaRepository
{
    private readonly AppDbContext _context;

    public EmpresaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Empresa>> GetAllAsync(CancellationToken ct)
    {
        return await _context.Empresas.AsNoTracking().ToListAsync(ct);
    }

    public async Task<Empresa?> GetByIdAsync(int id, CancellationToken ct)
    {
        return await _context.Empresas.FindAsync(new object[] { id }, ct);
    }

    public async Task<Empresa?> GetByCNPJAsync(string cnpj, CancellationToken ct)
    {
        return await _context.Empresas.FirstOrDefaultAsync(e => e.CNPJ == cnpj, ct);
    }

    public async Task AddAsync(Empresa empresa, CancellationToken ct)
    {
        await _context.Empresas.AddAsync(empresa, ct);
    }

    public Task UpdateAsync(Empresa empresa, CancellationToken ct)
    {
        _context.Empresas.Update(empresa);
        return Task.CompletedTask;
    }

    public Task RemoveAsync(Empresa empresa, CancellationToken ct)
    {
        _context.Empresas.Remove(empresa);
        return Task.CompletedTask;
    }

    public Task<bool> CNPJExistsAsync(string cnpj, CancellationToken ct)
    {
        return _context.Empresas.AnyAsync(e => e.CNPJ == cnpj, ct);
    }

    public Task<bool> EmailExistsAsync(string email, CancellationToken ct)
    {
        return _context.Empresas.AnyAsync(e => e.Email == email, ct);
    }

    public async Task SaveChangesAsync(CancellationToken ct)
    {
        await _context.SaveChangesAsync(ct);
    }
}
