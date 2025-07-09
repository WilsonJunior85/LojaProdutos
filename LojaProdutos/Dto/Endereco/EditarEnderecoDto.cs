namespace LojaProdutos.Dto.Endereco
{
    public class EditarEnderecoDto
    {
        public int Id { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Numero { get; set; }
        public string CEP { get; set; }
        public string Estado { get; set; }
        public string? Complemento { get; set; }



        // Dentro de endereço podemos ter um usuário, vamos relaçionar a tabelas
        public int UsuarioModelId { get; set; }
    }
}
