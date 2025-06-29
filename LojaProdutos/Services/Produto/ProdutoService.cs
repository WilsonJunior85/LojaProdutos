﻿using LojaProdutos.Data;
using LojaProdutos.Dto.Produto;
using LojaProdutos.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaProdutos.Services.Produto
{
    public class ProdutoService : IProdutoInterface
    {
        
        private readonly DataContext _context;
        private readonly string _sistema;

        public ProdutoService(DataContext context, IWebHostEnvironment sistema)
        {
            
            _context = context;
            _sistema = sistema.WebRootPath;
        }




        public async Task<List<ProdutoModel>> BuscarProdutos()
        {


            try
            {
                return await _context.Produtos.Include(c => c.Categoria).ToListAsync();
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
     
               
        
        }



        public async Task<ProdutoModel> Cadastrar(CriarProdutoDto criarProdutoDto, IFormFile foto)
        {
            try
            {
                //Salvamento da foto
                var nomeCaminhoImagem = GeraCaminhoArquivo(foto);

                var produto = new ProdutoModel()
                {
                    Nome = criarProdutoDto.Nome,
                    Marca = criarProdutoDto.Marca,
                    Valor = criarProdutoDto.Valor,
                    CategoriaModelId = criarProdutoDto.CategoriaModelId,
                    Foto = nomeCaminhoImagem,
                    QuantidadeEstoque = criarProdutoDto.QuantidadeEstoque

                };

                //adicionando dentro do banco de dados
                _context.Produtos.Add(produto);

                await _context.SaveChangesAsync();
                return produto;



            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        private string GeraCaminhoArquivo(IFormFile foto) 
        { 
            var codigoUnico = Guid.NewGuid().ToString(); // vai gera um identificador único          
            var nomeCaminhoImagem = foto.FileName.Replace(" ", "").ToLower() + codigoUnico + ".png";         // gera o nome do caminho da imagem
           
            var caminhoParaSalvarImagens = _sistema + "\\imagem\\";      //nome do caminho para salvar as imagens

            //identifica a pasta caso não exista, ela cria.
            if (!Directory.Exists(caminhoParaSalvarImagens))
            {
                Directory.CreateDirectory(caminhoParaSalvarImagens);
            }

            //salvar o arquivo foto dentro do caminho dentro da pasta
            using (var stream = File.Create(caminhoParaSalvarImagens + nomeCaminhoImagem))
            {
                foto.CopyToAsync(stream).Wait(); // criando uma copie do arquivo dentro do strem
            }
            return nomeCaminhoImagem;
        }







    }

}
