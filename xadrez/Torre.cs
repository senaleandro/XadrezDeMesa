using xadrez_Console.Tabuleiro;

namespace xadrez
{
    class Torre : Peca
    {
        public Torre(Tabuleiro tab, Cor cor) : base(tab, cor)
        {
        }

        public override string ToString()
        {
            return "T";
        }

        private bool podeMover(Posicao pos)
        {
            Peca p = Tab.peca(pos);
            return p == null || p.cor != cor;
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.linhas, Tab.colunas];

            Posicao pos = new Posicao((char)0, 0);


            // Acima

            pos.definirValores(posicao.linha - 1, posicao.coluna);
            while (Tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (Tab.peca(pos) != null && Tab.peca(pos).cor != cor)
                {
                    break;
                }
                pos.linha -= 1;
            }
            return mat;


            // Abaixo

            pos.definirValores(posicao.linha + 1, posicao.coluna);
            while (Tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (Tab.peca(pos) != null && Tab.peca(pos).cor != cor)
                {
                    break;
                }
                pos.linha += 1;
            }
            return mat;


            // Direita

            pos.definirValores(posicao.linha, posicao.coluna + 'a');
            while (Tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (Tab.peca(pos) != null && Tab.peca(pos).cor != cor)
                {
                    break;
                }
                pos.coluna += 'a';
            }
            return mat;


            // Esquerda


            pos.definirValores(posicao.linha, posicao.coluna - 'a');
            while (Tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (Tab.peca(pos) != null && Tab.peca(pos).cor != cor)
                {
                    break;
                }
                pos.coluna  -= 'a';
            }
            return mat;


        }
    }
}