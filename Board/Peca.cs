using System.Reflection.Metadata;
using xadrez_Console.Tabuleiro;

namespace xadrez
{
    abstract class Peca
    {
        public Posicao? posicao { get; set; }
        public Cor cor { get; protected set; }
        public int qtdMovimentos { get; protected set; }
        public Tabuleiro Tab { get; protected set; }


        public Peca(Tabuleiro tab, Cor cor)
        {
            this.posicao = null;
            Tab = tab;
            this.cor = cor;
            this.qtdMovimentos = 0;
        }

         public void incrementarQtdMovimento()
        {
            qtdMovimentos++;
        }

        public void decrementarQtdMovimento()
        {
            qtdMovimentos--;
        }

        public bool existeMovimentosPossiveis()
        {
             bool[,] mat = movimentosPossiveis();
             for (int i = 0; i < Tab.linhas; i++)
             {
                for (int j = 0; j < Tab.colunas; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
                
             }
             return false;
        }

        public bool movimentoPossivel(Posicao pos)
        {
            return movimentosPossiveis()[pos.linha, pos.coluna];
        }

        public abstract bool[,] movimentosPossiveis();

    }
}