using System.Reflection.Metadata;
using tabuleiro;
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
            posicao = null;
            Tab = tab;
            this.cor = cor;
            qtdMovimentos = 0;
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

        public bool podeMoverPara(Posicao pos)
        {
            return movimentosPossiveis()[pos.linha, pos.coluna];
        }

        public abstract bool[,] movimentosPossiveis();

    }
}