using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Galoto.Models;

[Table("Categorias")]
public class Categoria
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "A descrição é obrigatória")]
    [StringLength(100, ErrorMessage = "A descrição deve ter no máximo 100 caracteres")]
    public string Descricao { get; set; } = string.Empty;
}