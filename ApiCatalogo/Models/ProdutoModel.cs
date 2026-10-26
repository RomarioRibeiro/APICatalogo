using ApiCatalogo.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiCatalogo.Models;

public class ProdutoModel
{
    [Key]
    public int ProdutoId { get; set; }

    [Required(ErrorMessage = "Nome é  obrigatório!")]
    [StringLength(80, ErrorMessage = "O nome deve ter entre 5 a 20 caracteres", MinimumLength = 3)]
    [PrimeiraLetraMaiuscula]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "Descrição é obrigatório!")]
    [StringLength(300, ErrorMessage = "O descrição deve ter no máximo {1} caracteres!")]
    public string? Descricao { get; set; }

    [Required(ErrorMessage = "O preço é  obrigatório!")]
    [Column(TypeName = "decimal(10,2)")]
    [Range(1, 1000, ErrorMessage = "Preço deve esta entre {1} e {2}")]
    public decimal Preco { get; set; }

    [Required(ErrorMessage = "A imagem é  obrigatório!")]
    [StringLength(300, MinimumLength = 10)]
    public string? ImagemUrl { get; set; }

    public float Estoque { get; set; }

    public DateTime DataCadastro { get; set; } = DateTime.Now;

    public int CategoriaId { get; set; }

    [JsonIgnore]
    public CategoriaModel? Categoria { get; set; }
}
