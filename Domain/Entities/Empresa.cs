using System.ComponentModel.DataAnnotations;

namespace APIUsuarios.Domain.Entities;

public class Empresa
{
    [Key]
    public int Id { get; set; } // PK, Auto-increment

    [Required]
    [MaxLength(150)]
    [MinLength(3)]
    public string RazaoSocial { get; set; } // Obrigatório, 3-150 caracteres

    [MaxLength(150)]
    public string? NomeFantasia { get; set; } // Opcional

    [Required]
    public string CNPJ { get; set; } // Obrigatório, formato XX.XXX.XXX/XXXX-XX, único

    [Required]
    [EmailAddress]
    public string Email { get; set; } // Obrigatório, formato válido, único

    [Phone]
    public string? Telefone { get; set; } // Opcional, formato (XX) XXXXX-XXXX

    [Required]
    public bool Ativo { get; set; } = true; // Obrigatório, default true

    [Required]
    public DateTime DataCriacao { get; set; } = DateTime.Now; // Obrigatório, preenchido automaticamente

    public DateTime? DataAtualizacao { get; set; } // Opcional, atualizado automaticamente
}
