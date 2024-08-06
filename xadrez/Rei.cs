using System.Runtime.CompilerServices;
using tabuleiro;
using xadrez_Console.Tabuleiro;

namespace xadrez
{
    class Rei : Peca
    {
        private PartidaDeXadrez partida;
        public Rei(Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base(tab, cor)
        {
            this.partida = partida;
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

        private bool testeTorreParaRoque(Posicao pos)
        {
            var p = Tab.peca(pos);
            return p != null && p is Torre && p.cor == cor && p.qtdMovimentos == 0;
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.linhas, Tab.colunas];

            Posicao pos = new Posicao('a', 0);


            // Acima

            pos.definirValores(posicao!.linha - 1, posicao.coluna);
            if (Tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;

            }

            // NE

            pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
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

            // #Jogadaespecial Roque
            if (qtdMovimentos == 0 && !partida.xeque)
            {
                //Jogadaespecial Roque pequeno
                Posicao posT1 = new Posicao((char)posicao.linha, posicao.coluna + 3);
                if (!testeTorreParaRoque(posT1))
                {
                }
                else
                {
                    Posicao p1 = new Posicao((char)posicao.linha, posicao.coluna + 1);
                    Posicao p2 = new Posicao((char)posicao.linha, posicao.coluna + 2);
                    if (Tab.peca(p1) == null && Tab.peca(p2) == null)
                    {
                        mat[posicao.linha, posicao.coluna + 2] = true;
                    }
                }
                // #jogadaespecial Roque grande
                Posicao posT2 = new Posicao((char)posicao.linha, posicao.coluna - 4);
            if (testeTorreParaRoque(posT2))
            {
                Posicao p1 = new Posicao((char)posicao.linha, posicao.coluna - 1);
                Posicao p2 = new Posicao((char)posicao.linha, posicao.coluna - 2);
                Posicao p3 = new Posicao((char)posicao.linha, posicao.coluna - 3);
                if (Tab.peca(p1) == null && Tab.peca(p2) == null && Tab.peca(p3) == null)
                {
                    mat[posicao.linha, posicao.coluna - 2] = true;
                }
            }
        }
        return mat;
        }
    }
}
    
