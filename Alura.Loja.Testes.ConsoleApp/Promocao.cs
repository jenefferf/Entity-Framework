using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Loja.Testes.ConsoleApp
{
    public class Promocao
    {
        public int ID { get; internal set; }
        public string Descricao { get; internal set; }
        public DateTime DataInicio { get; internal set; }
        public DateTime DataTermino { get; internal set; }
        public IList<Object> Produtos { get; internal set; }


        public Promocao()
        {
            this.Produtos = new List<PromocaoProduto>();
        }

        public void IncluiProduto(Produto produto)
        {
            this.Produtos.Add(new PromocaoProduto() { Produto = produto });
        }
    }
}
