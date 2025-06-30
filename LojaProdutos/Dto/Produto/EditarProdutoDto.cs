using System.ComponentModel.DataAnnotations;

namespace LojaProdutos.Dto.Produto
{
    public class EditarProdutoDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite o Nome")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Digite a Marca")]
        public string Marca { get; set; }
        //[Required(ErrorMessage = "Insira a Foto")]
        public string? Foto { get; set; }
        [Required(ErrorMessage = "Digite a Valor")]
        public double Valor { get; set; }
        [Required(ErrorMessage = "Insira a Quantidade")]
        public int QuantidadeEstoque { get; set; }



        [Required(ErrorMessage = "Selecione a Categoria")]
        public int CategoriaModelId { get; set; }
    }
}
