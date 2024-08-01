using xadrez_Console.Tabuleiro;

namespace tabuleiro
{
    class Peca
    {
        public Posicao posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int qtdMovimentos { get; protected set; }
        public Table Tab { get; protected set; }

        public Peca(Posicao posicao,Table tab, Cor cor)
        {
            this.posicao = posicao;
            this.Tab = tab;
            this.Cor = cor;
            this.qtdMovimentos = 0;
        }
    }
}