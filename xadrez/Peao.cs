using xadrez_Console.Tabuleiro;

namespace xadrez
{

    class Peao : Peca
    {
        private PartidaDeXadrez partida;
        public Peao(Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base(tab, cor)
        {
        }

        public override string ToString()
        {
            return "P";
        }

        private bool existeInimigo(Posicao pos)
        {
            Peca p = Tab.peca(pos);
            return p != null && p.cor != cor;
        }

        private bool livre(Posicao pos)
        {
            return Tab.peca(pos) == null;
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.linhas, Tab.colunas];

            Posicao pos = new Posicao((char)0, 0);

            if (cor == Cor.Branca)
            {
                pos.definirValores(posicao.linha - 1, posicao.coluna);
                if (Tab.posicaoValida(pos) && livre(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(posicao.linha - 2, posicao.coluna);
                if (Tab.posicaoValida(pos) && livre(pos) && qtdMovimentos == 0)
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(posicao.linha - 1, posicao.coluna - 1);
                if (Tab.posicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
                if (Tab.posicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
            }

            // #Jogadaespecial En Passant
            if (posicao.linha == 3)
            {
                Posicao esquerda = new Posicao((char)posicao.linha, posicao.coluna - 1);
                if (Tab.posicaoValida(esquerda) && existeInimigo(esquerda) && Tab.peca(esquerda) == partida.vulneravelEnPassant)
                {
                    mat[esquerda.linha --, esquerda.coluna] = true;
                }
                Posicao direita = new Posicao((char)posicao.linha, posicao.coluna + 1);
                if (Tab.posicaoValida(direita) && existeInimigo(direita) && Tab.peca(direita) == partida.vulneravelEnPassant)
                {
                    mat[direita.linha --, direita.coluna] = true;
                }
            }

            else
            {
                pos.definirValores(posicao.linha + 1, posicao.coluna);
                if (Tab.posicaoValida(pos) && livre(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(posicao.linha + 2, posicao.coluna);
                if (Tab.posicaoValida(pos) && livre(pos) && qtdMovimentos == 0)
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(posicao.linha + 1, posicao.coluna - 1);
                if (Tab.posicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(posicao.linha + 1, posicao.coluna + 1);
                if (Tab.posicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }


                // #Jogadaespecial En Passant
                if (posicao.linha == 4)
                {
                    Posicao esquerda = new Posicao((char)posicao.linha, posicao.coluna - 1);
                    if (Tab.posicaoValida(esquerda) && existeInimigo(esquerda) && Tab.peca(esquerda) == partida.vulneravelEnPassant)
                    {
                        mat[esquerda.linha ++, esquerda.coluna] = true;
                    }
                    Posicao direita = new Posicao((char)posicao.linha, posicao.coluna + 1);
                    if (Tab.posicaoValida(direita) && existeInimigo(direita) && Tab.peca(direita) == partida.vulneravelEnPassant)
                    {
                        mat[direita.linha++, direita.coluna] = true;
                    }
                }
            }
            return mat;
        }
    }
}
