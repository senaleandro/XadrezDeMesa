using xadrez_Console;

namespace xadrez
{
    class PosicaoXadrez
    {
        public char Coluna { get; set; }
        public int Linha { get; set; }

        public PosicaoXadrez(char coluna, int linha)
        {
            if (coluna < 'a' || coluna > 'h')
                throw new ArgumentException("Coluna deve estar entre 'a' e 'h'");
            if (linha < 1 || linha > 8)
                throw new ArgumentException("Linha deve estar entre 1 e 8");

            Coluna = coluna;
            Linha = linha;
        }

        public Posicao ToPosicao()
        {
            return new Posicao((char)(Coluna - 'a'), 8 - Linha);
        }

        public override string ToString()
        {
            return $"{Coluna}{Linha}";
        }
    }
}
