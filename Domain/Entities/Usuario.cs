using System.ComponentModel.DataAnnotations;

namespace APIUsuarios.Domain.Entities;

public  class  Usuario

{
    [Key]
    public  int Id { get; set; } // PK, Auto-increment

    [Required]
    [MaxLength(100)]
    [MinLength(3)]
    public  string Nome { get; set; } // Obrigatório, 3-100 caracteres

    [Required]
    [EmailAddress]
    public  string Email { get; set; } // Obrigatório, formato válido, único

    [Required]
    [MinLength(6)]
    public  string Senha { get; set; } // Obrigatório, min 6 caracteres
    
    [Required]
    public DateTime DataNascimento { get; set; } // Obrigatório, idade >= 18 anos

    [Phone]
    public  string Telefone { get; set; } // Opcional, formato (XX) XXXXX-XXXX

    [Required] 
    public bool Ativo { get; set; } = true; // Obrigatório, default true

    [Required]
    public  DateTime DataCriacao { get; set; } = DateTime.Now; // Obrigatório, preenchido automaticamente
    
    public  DateTime? DataAtualizacao { get; set; }  // Opcional, atualizado automaticamente

}