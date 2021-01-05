using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Loja.Testes.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // GravarUsandoAdoNet();
            //GravarUsandoEntity();
           // RecuperarProdutos();
           // ExcluirProdutos();
            AtualizarProduto();
        }

        private static void AtualizarProduto()
        {
            GravarUsandoEntity();
            RecuperarProdutos();
          
            using (var repo = new ProdutoDAOEntity())
            {
                Produto primeiro = repo.Produtos().First();
                primeiro.Nome = "Cassino Royale - Editado";
                repo.Atualizar(primeiro);
            }
        }

        private static void ExcluirProdutos()
        {
            using (var repo = new ProdutoDAOEntity())
            {
                IList<Produto> produtos = repo.Produtos().ToList();
                foreach(Produto item in produtos)
                {
                    repo.Remover(item);
                }
            }
        }

        private static void GravarUsandoEntity()
        {
            Produto p = new Produto();
            p.Nome = "Harry Potter e a Ordem da Fênix";
            p.Categoria = "Livros";
            p.Preco = 19.89;

            using (var contexto = new ProdutoDAOEntity())
            {
                contexto.Adicionar(p);
            }
        }

        private static void RecuperarProdutos()
        {
            using(var repo = new ProdutoDAOEntity())
            {
                IList<Produto> produtos = repo.Produtos().ToList();
                Console.WriteLine("Teste ", produtos.Count());
                foreach(var item in produtos)
                {
                    Console.WriteLine(item.Nome);
                }

            }
         }
            private static void GravarUsandoAdoNet()
            {
                Produto p = new Produto();
                p.Nome = "Harry Potter e a Ordem da Fênix";
                p.Categoria = "Livros";
                p.Preco = 19.89;

                using (var repo = new ProdutoDAO())
                {
                    repo.Adicionar(p);
                }
            }

    }
}