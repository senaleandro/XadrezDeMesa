using Tabuleiro;


namespace xadrez_Console.Tabuleiro
{
    class Peca
    {
        public Posicao posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int qtdMovimentos { get; protected set; }
        public Tabuleiro1 Tab { get; protected set; }

        public Peca(Posicao posicao,Tabuleiro1 tab, Cor cor)
        {
            this.posicao = posicao;
            this.Tab = tab;
            this.Cor = cor;
            this.qtdMovimentos = 0;
        }
    }
}