using xadrez_Console.Tabuleiro;

namespace xadrez
{
    class Peca
    {
        public Posicao posicao { get; set; }
        public Cor cor { get; protected set; }
        public int qtdMovimentos { get; protected set; }
        public Tabuleiro Tab { get; protected set; }

        public Peca(Tabuleiro tab, Cor cor)
        {
            this.posicao = null;
            this.Tab = tab;
            this.cor = cor;
            this.qtdMovimentos = 0;
        }

        public void incrementarQtdMovimento()
        {
            qtdMovimentos++;
        }
    }
}