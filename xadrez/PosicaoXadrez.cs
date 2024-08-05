using xadrez_Console;

namespace xadrez
{
    class PosicaoXadrez
    {
        public char coluna { get; set; }
        public int linha { get; set; }

        public PosicaoXadrez(char coluna, int linha)
        {
            if (coluna < 'a' || coluna > 'h')
                throw new ArgumentException("coluna deve estar entre 'a' e 'h'");
            if (linha < 1 || linha > 8)
                throw new ArgumentException("linha deve estar entre 1 e 8");

            this.coluna = coluna;
            this.linha = linha;
        }

        public Posicao ToPosicao()
        {
            return new Posicao((char)(coluna - 'a'), 8 - linha);
        }

        public override string ToString()
        {
            return $"{coluna}{linha}";
        }
    }
}
