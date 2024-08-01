using tabuleiro;
using xadrez;
using xadrez_Console.Tabuleiro;
namespace xadrez_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                Table tab = new Table(8, 8);

                tab.colocarPeca(new Torre(tab, Cor.Preta), new Posicao(0, 0));
                tab.colocarPeca(new Torre(tab, Cor.Preta), new Posicao(1, 9));
                tab.colocarPeca(new Rei(tab, Cor.Preta), new Posicao(0, 0));

                Tela.imprimirTabuleiro(tab);

            }
            catch (TabuleiroException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

        }
    }
}