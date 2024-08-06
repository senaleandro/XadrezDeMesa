namespace xadrez
{
    class Posicao
    {
        public int coluna { get; set; }
        public int linha { get; set; }

        public Posicao(int coluna, int linha)
        {
            this.coluna = coluna;
            this.linha = linha;
        }
        public void definirValores(int linha, int coluna)
        {
            this.linha = linha;
            this.coluna = coluna;
        }

        public override string ToString()
        {
            return linha
            + ", "
             + coluna;
        }
    }
}