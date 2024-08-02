namespace xadrez
{
    class Posicao
    {
        public int linha { get; set; }
        public char coluna { get; set; }

        public Posicao(char coluna, int linha)
        {
            this.coluna = coluna;
            this.linha = linha;
        }

        public void definirValores( int linha, int coluna)
        {
        }

        public Posicao toPosicao()
        {
            return new Posicao((char)(coluna - 'a'), 8 - linha);
        }

        public override string ToString()
        {
            return linha 
            + ", "
             + coluna;
        }
    }
}