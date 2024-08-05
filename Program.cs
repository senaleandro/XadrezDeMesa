using System;
using xadrez;
namespace xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PartidaDeXadrez partida = new PartidaDeXadrez();

                while (!partida.terminada)
                {
                    try {
                    Console.Clear();
                    Tela.imprimirPartida(partida);

                    Console.WriteLine();
                    Console.Write("Origem: ");
                    Posicao origem = Tela.lerPosicaoXadrez().ToPosicao();
                    partida.validarPosicaoDeOrigem(origem);
                    
                    bool[,] posicoesPossiveis = partida.Tab.peca(origem).movimentosPossiveis();

                    
                    Console.Clear();
                    Tela.imprimirTabuleiro(partida.Tab, posicoesPossiveis);

                    Console.WriteLine();
                    Console.Write("Destino: ");
                    Posicao destino = Tela.lerPosicaoXadrez().ToPosicao();
                    partida.validarPosicaoDestino(origem, destino);
                    
                    partida.executaMovimento(origem, destino);
                    }
                    catch (TabuleiroException e )
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }

            }
            catch (TabuleiroException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            Console.ReadLine();

        }
    }
}