using APIUsuarios.Application.DTOs;
using APIUsuarios.Infrastructure.Persistence;
using APIUsuarios.Application.Interfaces;
using APIUsuarios.Infrastructure.Repositories;
using APIUsuarios.Application.Services;
using APIUsuarios.Application.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseSqlite("Data Source=app.db"));

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

// Registro automático de todos os validadores que estão no assembly
builder.Services.AddValidatorsFromAssemblyContaining<UsuarioCreateDtoValidator>();

var app = builder.Build();

// GET todos
app.MapGet("/usuarios", async (IUsuarioService service, CancellationToken ct) =>
{
    var usuarios = await service.ListarAsync(ct);
    return Results.Ok(usuarios);
});

// GET por id
app.MapGet("/usuarios/{id}", async (int id, IUsuarioService service, CancellationToken ct) =>
{
    var usuario = await service.ObterAsync(id, ct);
    return usuario is not null ? Results.Ok(usuario) : Results.NotFound();
});

// POST
app.MapPost("/usuarios", async (
    UsuarioCreateDto dto,
    IUsuarioService service,
    CancellationToken ct,
    [FromServices] IValidator<UsuarioCreateDto> validator) =>
{
    var validationResult = await validator.ValidateAsync(dto, ct);

    if (!validationResult.IsValid)
        return Results.ValidationProblem(validationResult.ToDictionary());

    try
    {
        var usuario = await service.CriarAsync(dto, ct);
        return Results.Created($"/usuarios/{usuario.Id}", usuario);
    }
    catch (InvalidOperationException ex)
    {
        return Results.Conflict(new { message = ex.Message });
    }
});

// PUT
app.MapPut("/usuarios/{id}", async (
    int id,
    UsuarioUpdateDto dto,
    IUsuarioService service,
    CancellationToken ct,
    [FromServices] IValidator<UsuarioUpdateDto> validator) =>
{
    var validationResult = await validator.ValidateAsync(dto, ct);

    if (!validationResult.IsValid)
        return Results.ValidationProblem(validationResult.ToDictionary());

    try
    {
        var usuarioAtualizado = await service.AtualizarAsync(id, dto, ct);
        return usuarioAtualizado is not null ? Results.Ok(usuarioAtualizado) : Results.NotFound();
    }
    catch (InvalidOperationException ex)
    {
        return Results.Conflict(new { message = ex.Message });
    }
});

// DELETE
app.MapDelete("/usuarios/{id}", async (int id, IUsuarioService service, CancellationToken ct) =>
{
    var removido = await service.RemoverAsync(id, ct);
    return removido ? Results.NoContent() : Results.NotFound();
});

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();