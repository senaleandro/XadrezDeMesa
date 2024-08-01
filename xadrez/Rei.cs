using tabuleiro;
using xadrez_Console.Tabuleiro;

namespace xadrez
{
    class Rei : Peca
    {
        public Rei(Table tab, Cor cor) : base(tab, cor)
        {
        }

        public override string ToString()
        {
            return "R";
        }
    }
}