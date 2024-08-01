using xadrez_Console.Tabuleiro;

namespace tabuleiro
{
    class Peca
    {
        public Posicao posicao { get; set; }
        public Cor cor { get; protected set; }
        public int qtdMovimentos { get; protected set; }
        public Table Tab { get; protected set; }

        public Peca(Table tab, Cor cor)
        {
            this.posicao = null;
            this.Tab = tab;
            this.cor = cor;
            this.qtdMovimentos = 0;
        }
    }
}