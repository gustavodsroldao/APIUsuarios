using APIUsuarios.Application.DTOs;

namespace APIUsuarios.Application.Interfaces;

public interface IEmpresaService
{
    Task<IEnumerable<EmpresaReadDto>> ListarAsync(CancellationToken ct);
    Task<EmpresaReadDto?> ObterAsync(int id, CancellationToken ct);
    Task<EmpresaReadDto> CriarAsync(EmpresaCreateDto dto, CancellationToken ct);
    Task<EmpresaReadDto?> AtualizarAsync(int id, EmpresaUpdateDto dto, CancellationToken ct);
    Task<bool> RemoverAsync(int id, CancellationToken ct);
}
