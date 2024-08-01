using System;
using tabuleiro;
namespace xadrez_Console
{
    class Program
    {
        static void Main(string[] args)
        {
           Table tab = new Table(8, 8);

            Tela.imprimirTabuleiro(tab);
            
        }
    }
}