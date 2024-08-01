using tabuleiro;
using xadrez_Console.Tabuleiro;

namespace xadrez
{
    class Torre : Peca
    {
        public Torre(Table tab, Cor cor) : base(tab, cor)
        {
        }

        public override string ToString()
        {
            return "T";
        }
    }
}