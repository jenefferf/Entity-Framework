using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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

            using (var contexto = new LojaContext())
            {
                var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(SqlLoggerProvider.Create());


                var cliente = contexto
                 .Clientes
                 .Include(c => c.EnderecoEntrega)
                 .FirstOrDefault();

                Console.WriteLine($"Endereço de entrega: {cliente.EnderecoEntrega.Logradouro}");
            

                var produto = contexto
                .Produtos
                .Include(p => p.Compras)
                .Where(p => p.Id == 1)
                .FirstOrDefault();

                Console.WriteLine($"Mostrando as compras do produto {produto.Nome}");
                foreach (var item in produto.Compras)
                {
                    Console.WriteLine(item);
                }
        }


        }

        private static void ExibeProdutosNaPromocao()
        {
            using (var contexto2 = new LojaContext())
            {
                var promocao = contexto2.Promocoes
                .Include(p => p.Produtos)
                .ThenInclude(pp => pp.Produto)
                .FirstOrDefault();
                Console.WriteLine("\nMotrando os produtos da promoção...");
                foreach (var item in promocao.Produtos)
                {
                    Console.WriteLine(item.Produto);
                }

            }
        }

        public static void IncluirPromocao() {
            using (var contexto = new LojaContext())
            {
                var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(SqlLoggerProvider.Create());

                var promocao = new Promocao();
                promocao.Descricao = "Queima total janeiro 2020";
                promocao.DataInicio = new DateTime(2020, 1, 1);
                promocao.DataTermino = new DateTime(2020, 1, 31);

                var produtos = contexto.Produtos.Where(p => p.Categoria == "Bebidas").ToList();

                foreach (var item in produtos)
                {
                    promocao.IncluiProduto(item);
                }

                contexto.Promocoes.Add(promocao);
                ExibeEntries(contexto.ChangeTracker.Entries());
                contexto.SaveChanges();
            }


        }

        private static void UmPraUm() {
            var fulano = new Cliente();
            fulano.Nome = "Fulano de tal";
            fulano.EnderecoEntrega = new Endereco()
            {
                Numero = 12,
                Logradouro = "Rua das gaviotas",
                Complemento = "Casinha de sapê",
                Bairro = "Centro",
                Cidade = "Cidade"
            };

            using (var contexto = new LojaContext())
            {
                var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(SqlLoggerProvider.Create());

                contexto.Clientes.Add(fulano);
                contexto.SaveChanges();
            }


        }
        private static void MuitosParaMuitos() {
            /*
            var PaoFrances = new Produto();
            PaoFrances.Nome = "Pao Frances";
            PaoFrances.PrecoUnitario = 0.4;
            PaoFrances.Unidade = "Unidade";
            PaoFrances.Categoria = "Padaria";

            var compra = new Compra();
            compra.Quantidade = 6;
            compra.Produto = PaoFrances;
            compra.Preco = PaoFrances.PrecoUnitario * compra.Quantidade;
             */

            var p1 = new Produto() { Nome = "Suco de Laranja", Categoria = "Bebidas", PrecoUnitario = 8.79, Unidade = "Litros" };
            var p2 = new Produto() { Nome = "Café", Categoria = "Bebidas", PrecoUnitario = 12.69, Unidade = "Gramas" };
            var p3 = new Produto() { Nome = "Macarraão", Categoria = "Alimentos", PrecoUnitario = 4.20, Unidade = "Kg" };

            var promocaoDePascoa = new Promocao();
            promocaoDePascoa.Descricao = "Pascoa feliz";
            promocaoDePascoa.DataInicio = DateTime.Now;
            promocaoDePascoa.DataTermino = DateTime.Now.AddMonths(3);

            promocaoDePascoa.IncluiProduto(p1);
            promocaoDePascoa.IncluiProduto(p2);
            promocaoDePascoa.IncluiProduto(p3);

            using (var contexto = new LojaContext())
            {

                var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(SqlLoggerProvider.Create());

                // contexto.Promocoes.Add(promocaoDePascoa);

                var promocao = contexto.Promocoes.Find(1);
                contexto.Promocoes.Remove(promocao);
                ExibeEntries(contexto.ChangeTracker.Entries());

                // contexto.SaveChanges();

            }
        }

        private static void ExibeEntries(IEnumerable<EntityEntry> entries)
        {
            foreach (var e in entries)
            {
                Console.WriteLine(e.Entity.ToString() + " - " + e.State);
            }
        }

       

        /*
        * using (var contexto = new LojaContext())
       {
           var produtos = contexto.Produtos.ToList();
           foreach (var p in produtos)
           {
               Console.WriteLine(p);
           }


           Console.WriteLine("=================");


           foreach (var e in contexto.ChangeTracker.Entries())
           {
               Console.WriteLine(e.State);
           }

           var p1 = produtos.Last();
           p1.Nome = "Teste2";

           Console.WriteLine("=================");
           foreach (var e in contexto.ChangeTracker.Entries())
           {
               Console.WriteLine(e.State);
           }




        */

        /*

        Console.WriteLine("=================");
        foreach (var e in contexto.ChangeTracker.Entries())
        {
            Console.WriteLine(e);
        }



        Console.WriteLine("=================");
        foreach (var e in contexto.ChangeTracker.Entries())
        {
            Console.WriteLine(e);


        } */
    }
}
