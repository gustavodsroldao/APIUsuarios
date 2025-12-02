using APIUsuarios.Application.DTOs;
using APIUsuarios.Infrastructure.Persistence;
using APIUsuarios.Interfaces;
using APIUsuarios.Repositories;
using APIUsuarios.Services;
using APIUsuarios.Validators;
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

// POST → já estava correto
app.MapPost("/usuarios", async (
    UsuarioCreateDto dto,
    IUsuarioService service,
    CancellationToken ct,
    [FromServices] IValidator<UsuarioCreateDto> validator) =>
{
    var validationResult = await validator.ValidateAsync(dto, ct);

    if (!validationResult.IsValid)
        return Results.ValidationProblem(validationResult.ToDictionary());

    var usuario = await service.CriarAsync(dto, ct);
    return Results.Created($"/usuarios/{usuario.Id}", usuario);
});

app.MapPut("/usuarios/{id}", async (
    int id,
    UsuarioUpdateDto dto,
    IUsuarioService service,
    CancellationToken ct,
    [FromServices] IValidator<UsuarioUpdateDto> validator) => // ← AQUI ERA O PROBLEMA
{
    var validationResult = await validator.ValidateAsync(dto, ct);

    if (!validationResult.IsValid)
        return Results.ValidationProblem(validationResult.ToDictionary());

    var usuarioAtualizado = await service.AtualizarAsync(id, dto, ct);
    return usuarioAtualizado is not null ? Results.Ok(usuarioAtualizado) : Results.NotFound();
});

// PATCH → MESMA COISA, adicionado [FromServices]
app.MapPatch("/usuarios/{id}", async (
    int id,
    UsuarioUpdateDto dto,
    IUsuarioService service,
    CancellationToken ct,
    [FromServices] IValidator<UsuarioUpdateDto> validator) => // ← AQUI TAMBÉM
{
    var validationResult = await validator.ValidateAsync(dto, ct);

    if (!validationResult.IsValid)
        return Results.ValidationProblem(validationResult.ToDictionary());

    var usuarioAtualizado = await service.AtualizarAsync(id, dto, ct);
    return usuarioAtualizado is not null ? Results.Ok(usuarioAtualizado) : Results.NotFound();
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