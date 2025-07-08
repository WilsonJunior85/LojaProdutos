using LojaProdutos.Enums;
using LojaProdutos.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace LojaProdutos.Dto.Usuario
{
    public class CriarUsuarioDto
    {
        [Required(ErrorMessage = "Digite o nome!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite o Email!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite o Cargo!")]
        public CargoEnum Cargo { get; set; }

        [Required(ErrorMessage = "Digite o Logradouro!")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "Digite o Bairro!")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Digite o número!")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "Digite o CEP!")]
        public string CEP { get; set; }

        [Required(ErrorMessage = "Digite o Estado!")]
        public string Estado { get; set; }

        //O complemento não é Required
        public string? Complemento { get; set; }

        [Required(ErrorMessage = "Digite a Senha!")]
        public string Senha { get; set; }


        [Required(ErrorMessage = "Confirme a Senha!"),Compare("Senha", ErrorMessage = "As Senhas não coincidem!")]
        public string ConfirmarSenha { get; set; }

    }
}
