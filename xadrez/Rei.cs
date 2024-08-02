using xadrez_Console.Tabuleiro;

namespace xadrez
{
    class Rei : Peca
    {
        public Rei(Tabuleiro tab, Cor cor) : base(tab, cor)
        {
        }

        public override string ToString()
        {
            return "R";
        }

        private bool podeMover(Posicao pos)
        {
            Peca p = Tab.peca(pos);
            return p == null || p.cor != cor; 
        }

        public override bool[,] movimentosPossiveis()
        {
           bool[,] mat = new bool [Tab.linhas, Tab.colunas];

           Posicao pos = new Posicao((char)0, 0);


           // Acima

           pos.definirValores(posicao.linha - 1, posicao.coluna);
           if (Tab.posicaoValida(pos) && podeMover(pos))
           {
            mat[pos.linha, pos.coluna] = true;

           }

           // NE

            pos.definirValores(posicao.linha - 1, posicao.coluna +1);
           if (Tab.posicaoValida(pos) && podeMover(pos))
           {
            mat[pos.linha, pos.coluna] = true;
           
           }

           // Direita

             pos.definirValores(posicao.linha, posicao.coluna + 1);
           if (Tab.posicaoValida(pos) && podeMover(pos))
           {
            mat[pos.linha, pos.coluna] = true;
           }

           
           // SE

            pos.definirValores(posicao.linha + 1, posicao.coluna + 1);
           if (Tab.posicaoValida(pos) && podeMover(pos))
           {
            mat[pos.linha, pos.coluna] = true;
           }


           // Baixo

            pos.definirValores(posicao.linha + 1, posicao.coluna);
           if (Tab.posicaoValida(pos) && podeMover(pos))
           {
            mat[pos.linha, pos.coluna] = true;
           }


           // SO

            pos.definirValores(posicao.linha + 1, posicao.coluna - 1);
           if (Tab.posicaoValida(pos) && podeMover(pos))
           {
            mat[pos.linha, pos.coluna] = true;
           }


           // Esquerda

            pos.definirValores(posicao.linha, posicao.coluna - 1);
           if (Tab.posicaoValida(pos) && podeMover(pos))
           {
            mat[pos.linha, pos.coluna] = true;
           }

           // NO

            pos.definirValores(posicao.linha - 1, posicao.coluna - 1);
           if (Tab.posicaoValida(pos) && podeMover(pos))
           {
            mat[pos.linha, pos.coluna] = true;
           }

           return mat;
        }
    }
}