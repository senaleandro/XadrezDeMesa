using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using tabuleiro;
using xadrez_Console.Tabuleiro;



namespace xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro Tab { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
        public bool xeque { get; private set; }
        public Peca? vulneravelEnPassant { get; private set; }

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            xeque = false;
            vulneravelEnPassant = null;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }

        public Peca executaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.retirarPeca(origem);
            p.incrementarQtdMovimento();
            Peca pecaCapturada = Tab.retirarPeca(destino);
            Tab.colocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }

            // #Jogadaespecial Roque pequeno
            if (p is Rei && destino.coluna == origem.coluna + 3)
            {
                Posicao origemT = new((char)origem.linha, origem.coluna + 3);
                Posicao destinoT = new((char)origem.linha, origem.coluna + 1);
                Peca T = Tab.retirarPeca(origem);
                T.incrementarQtdMovimento();
                Tab.colocarPeca(T, destinoT);
            }


            // #Jogadaespecial Roque grande
            if (p is Rei && destino.coluna == origem.coluna - 2)
            {
                Posicao origemT = new((char)origem.linha, origem.coluna + 4);
                Posicao destinoT = new((char)origem.linha, origem.coluna - 1);
                Peca T = Tab.retirarPeca(origem);
                T.incrementarQtdMovimento();
                Tab.colocarPeca(T, destinoT);
            }

              // #jogadaespecial En passant
            if (p is Peao) {
                if (origem.coluna != destino.coluna && pecaCapturada == null) {
                    Posicao posP;
                    if (p.cor == Cor.Branca) {
                        posP = new Posicao(destino.linha + 1, destino.coluna);
                    }
                    else {
                        posP = new Posicao(destino.linha - 1, destino.coluna);
                    }
                    pecaCapturada = Tab.retirarPeca(posP);
                    capturadas.Add(pecaCapturada);
                }
            }
            return pecaCapturada!;
        }

        public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = Tab.retirarPeca(destino);
            p.decrementarQtdMovimento();
            if (pecaCapturada != null)
            {
                Tab.colocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            Tab.colocarPeca(p, origem);

            // #Jogadaespecial Roque pequeno
            if (p is Rei && destino.coluna == origem.coluna + 2)
            {
                Posicao origemT = new Posicao((char)origem.linha, origem.coluna + 3);
                Posicao destinoT = new Posicao((char)origem.linha, origem.coluna + 1);
                Peca T = Tab.retirarPeca(destinoT);
                T.decrementarQtdMovimento();
                Tab.colocarPeca(T, origemT);
            }

            // #Jogadaespecial Roque grande
            if (p is Rei && destino.coluna == origem.coluna - 2)
            {
                Posicao origemT = new Posicao((char)origem.linha, origem.coluna - 4);
                Posicao destinoT = new Posicao((char)origem.linha, origem.coluna - 1);
                Peca T = Tab.retirarPeca(destinoT);
                T.decrementarQtdMovimento();
                Tab.colocarPeca(T, origemT);
            }

            // #Jogadaespecial En Passant
            if (p is Peao)
            {
                if (origem.coluna != destino.coluna && pecaCapturada == null)
                {
                    Peca peao = Tab.retirarPeca(destino);
                    Posicao posP;
                    if (p.cor == Cor.Branca)
                    {
                        posP = new Posicao(3, destino.coluna);
                    }
                    else
                    {
                        posP = new Posicao(4, destino.coluna);
                    }
                    pecaCapturada = Tab.retirarPeca(posP);
                    capturadas.Add(pecaCapturada);
                }
            }
        }

        public void realizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = executaMovimento(origem, destino);

            if (!estaEmXeque(jogadorAtual))
            {
                Peca p = Tab.peca(destino);

                // #Jogadaespecial Promocao
                if (p is Peao)
                {
                    if ((p.cor == Cor.Branca && destino.linha == 0) || (p.cor == Cor.Preta && destino.linha == 7))
                    {
                        p = Tab.retirarPeca(destino);
                        pecas.Remove(p);

                        ConsoleColor aux = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine();
                        Console.WriteLine("#--- PROMOÇÃO! ---#");
                        Console.WriteLine("Opções de promoção:");
                        Console.WriteLine(" - Dama[D]\n - Torre[T]\n - Bispo[B]\n - Cavalo[C]");
                        Console.ForegroundColor = aux;
                        Console.Write("Digite o caractere da opção escolhida: ");

                        char escolha = char.Parse(Console.ReadLine()!);

                        switch (escolha)
                        {
                            //DAMA
                            case 'D':
                                Peca dama = new Dama(Tab, p.cor);
                                Tab.colocarPeca(dama, destino);
                                pecas.Add(dama);
                                break;
                            case 'd':
                                Peca dama1 = new Dama(Tab, p.cor);
                                Tab.colocarPeca(dama1, destino);
                                pecas.Add(dama1);
                                break;

                            //TORRE
                            case 'T':
                                Peca torre = new Torre(Tab, p.cor);
                                Tab.colocarPeca(torre, destino);
                                pecas.Add(torre);
                                break;
                            case 't':
                                Peca torre1 = new Torre(Tab, p.cor);
                                Tab.colocarPeca(torre1, destino);
                                pecas.Add(torre1);
                                break;

                            //BISPO
                            case 'B':
                                Peca bispo = new Bispo(Tab, p.cor);
                                Tab.colocarPeca(bispo, destino);
                                pecas.Add(bispo);
                                break;
                            case 'b':
                                Peca bispo1 = new Bispo(Tab, p.cor);
                                Tab.colocarPeca(bispo1, destino);
                                pecas.Add(bispo1);
                                break;

                            //CAVALO
                            case 'C':
                                Peca cavalo = new Cavalo(Tab, p.cor);
                                Tab.colocarPeca(cavalo, destino);
                                pecas.Add(cavalo);
                                break;
                            case 'c':
                                Peca cavalo1 = new Cavalo(Tab, p.cor);
                                Tab.colocarPeca(cavalo1, destino);
                                pecas.Add(cavalo1);
                                break;
                        }

                    }
                }


                if (estaEmXeque(adversaria(jogadorAtual)))
                {
                    xeque = true;
                }
                else
                {
                    xeque = false;
                }
                if (testeXequemate(adversaria(jogadorAtual)))
                {
                    terminada = true;
                }
                else
                {
                    turno++;
                    mudaJogador();
                }

                // #Jogadaespecial En Passant
                if (p is Peca && (destino.linha == origem.linha - 2 || destino.linha == origem.linha + 2))
                {
                    vulneravelEnPassant = p;
                }
                else
                {
                    vulneravelEnPassant = null;
                }
            }
            else
            {
                desfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }
        }

        public void validarPosicaoDeOrigem(Posicao pos)
        {
            if (Tab.peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if (jogadorAtual != Tab.peca(pos).cor)
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }
            if (!Tab.peca(pos).existeMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        public void validarPosicaoDestino(Posicao origem, Posicao destino)
        {
            if (!Tab.peca(origem).podeMoverPara(destino))
            {
                throw new TabuleiroException("Posição de destino inválida!:" + (origem, destino));
            }
        }

        private void mudaJogador()
        {
            if (jogadorAtual == Cor.Branca)
            {
                jogadorAtual = Cor.Preta;
            }
            else
            {
                    jogadorAtual = Cor.Branca;
            }
        }

        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in capturadas)
            {
                if (x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;

        }

        private Cor adversaria(Cor cor)
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }

        }

        private Peca rei(Cor cor)
        {
            foreach (Peca x in pecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null!;
        }

        public bool estaEmXeque(Cor cor)
        {
            Peca R = rei(cor);
            if (R == null)
            {
                throw new TabuleiroException("Não tem rei da cor " + cor + " no tabuleiro");
            }
            foreach (Peca x in pecasEmJogo(adversaria(cor)))
            {
                var mat = x.movimentosPossiveis();
                if (mat[R.posicao!.linha, R.posicao.coluna])
                {
                    return true;
                }
            }
            return false;
        }

        public bool testeXequemate(Cor cor)
        {
            if (!estaEmXeque(cor))
            {
                return false;
            }
            foreach (Peca x in pecasEmJogo(cor))
            {
                bool[,] mat = x.movimentosPossiveis();
                for (int i = 0; i < Tab.linhas; i++)
                {
                    for (int j = 0; j < Tab.colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = x.posicao!;
                            Posicao destino = new Posicao((char)i, j);
                            Peca pecaCapturada = executaMovimento(origem, destino);
                            bool testeXeque = estaEmXeque(cor);
                            desfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            pecas.Add(peca);
        }
        private void colocarPecas()
        {
            colocarNovaPeca('a', 1, new Torre(Tab, Cor.Branca));
            colocarNovaPeca('b', 1, new Cavalo(Tab, Cor.Branca));
            colocarNovaPeca('c', 1, new Bispo(Tab, Cor.Branca));
            colocarNovaPeca('d', 1, new Dama(Tab, Cor.Branca));
            colocarNovaPeca('e', 1, new Rei(Tab, Cor.Branca, this));
            colocarNovaPeca('f', 1, new Bispo(Tab, Cor.Branca));
            colocarNovaPeca('g', 1, new Cavalo(Tab, Cor.Branca));
            colocarNovaPeca('h', 1, new Torre(Tab, Cor.Branca));
            colocarNovaPeca('a', 2, new Peao(Tab, Cor.Branca, this));
            colocarNovaPeca('b', 2, new Peao(Tab, Cor.Branca, this));
            colocarNovaPeca('c', 2, new Peao(Tab, Cor.Branca, this));
            colocarNovaPeca('d', 2, new Peao(Tab, Cor.Branca, this));
            colocarNovaPeca('e', 2, new Peao(Tab, Cor.Branca, this));
            colocarNovaPeca('f', 2, new Peao(Tab, Cor.Branca, this));
            colocarNovaPeca('g', 2, new Peao(Tab, Cor.Branca, this));
            colocarNovaPeca('h', 2, new Peao(Tab, Cor.Branca, this));

            //----------------------------------------------------\\

            colocarNovaPeca('a', 8, new Torre(Tab, Cor.Preta));
            colocarNovaPeca('b', 8, new Cavalo(Tab, Cor.Preta));
            colocarNovaPeca('c', 8, new Bispo(Tab, Cor.Preta));
            colocarNovaPeca('d', 8, new Dama(Tab, Cor.Preta));
            colocarNovaPeca('e', 8, new Rei(Tab, Cor.Preta, this));
            colocarNovaPeca('f', 8, new Bispo(Tab, Cor.Preta));
            colocarNovaPeca('g', 8, new Cavalo(Tab, Cor.Preta));
            colocarNovaPeca('h', 8, new Torre(Tab, Cor.Preta));
            colocarNovaPeca('a', 7, new Peao(Tab, Cor.Preta, this));
            colocarNovaPeca('b', 7, new Peao(Tab, Cor.Preta, this));
            colocarNovaPeca('c', 7, new Peao(Tab, Cor.Preta, this));
            colocarNovaPeca('d', 7, new Peao(Tab, Cor.Preta, this));
            colocarNovaPeca('e', 7, new Peao(Tab, Cor.Preta, this));
            colocarNovaPeca('f', 7, new Peao(Tab, Cor.Preta, this));
            colocarNovaPeca('g', 7, new Peao(Tab, Cor.Preta, this));
            colocarNovaPeca('h', 7, new Peao(Tab, Cor.Preta, this));
        }
    }
}

