using xadrez_Console;

namespace xadrez
{
    class PosicaoXadrez
    {
        public char Coluna { get; set; }
        public int Linha { get; set; }

        public PosicaoXadrez(char coluna, int linha)
        {
            Coluna = coluna;
            Linha = linha;
        }

        public Posicao ToPosicao()
        {
            return new Posicao(Coluna - 'a', 8 - Linha);
        }

          public override string ToString()
        {
            return "" + Coluna + Linha;
        }

    }
}
