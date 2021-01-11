namespace Alura.Loja.Testes.ConsoleApp
{
    public class Cliente
    {
        public Cliente()
        {
        }

        public int Id { get; internal set; }
        public string Nome { get; internal set; }
        public Endereco EnderecoEntrega { get; set; }


    }
}