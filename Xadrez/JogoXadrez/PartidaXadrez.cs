﻿using System;
using System.Collections.Generic;
using System.Text;
using Xadrez.tabuleiro;
using Xadrez.tabuleiro.TabuleiroException;
using Xadrez;



namespace Xadrez.JogoXadrez
{
    class PartidaXadrez
    {
        public Tabuleiro tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }
        private HashSet<Peca>  Pecas ;
        private HashSet<Peca> Capiturada;
        public bool Xeque { get; private set; }
        public Peca VulneravelEnPassant { get; private set; }

        public PartidaXadrez()
        {
            tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            Xeque = false;
            Pecas = new HashSet<Peca>();
            Capiturada = new HashSet<Peca>();
            VulneravelEnPassant = null;
            ColocarPecas();
        }
        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.RetirarPeca(origem);
            p.IncrementarQuantidadeMovimento();
            Peca capiturada = tab.RetirarPeca(destino);
            if (capiturada != null)
            {
                Capiturada.Add(capiturada);
            }
            
            // #Roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {                    
                    Peca T = tab.RetirarPeca(new Posicao(origem.Linha, origem.Coluna + 3));
                    T.IncrementarQuantidadeMovimento();
                    tab.ColocarPeca(T, new Posicao(origem.Linha, origem.Coluna + 1));                
            }

            // #Roque Grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Peca T = tab.RetirarPeca(new Posicao(origem.Linha, origem.Coluna - 4));
                T.IncrementarQuantidadeMovimento();
                tab.ColocarPeca(T, new Posicao(origem.Linha, origem.Coluna - 2));
            }
            //JogadaEspecial EnPassant
            if (p is Peao && destino.Coluna != origem.Coluna && capiturada == null)
            {
                if (p.Cor == Cor.Branca)
                {                    
                    Peca PeaoCap = tab.RetirarPeca(new Posicao(destino.Linha + 1, destino.Coluna));
                    Capiturada.Add(PeaoCap);
                }
                else
                {   
                    Peca peao = tab.RetirarPeca(new Posicao(destino.Linha - 1, destino.Coluna));
                    Capiturada.Add(peao);
                }
            }
            tab.ColocarPeca(p, destino);
            return capiturada;
        }
        public void DesfazMovimento(Posicao origem, Posicao destino, Peca capiturada)
        {
            Peca p = tab.RetirarPeca(destino);
            p.DecrementarQuantidadeMovimento();
            if(capiturada != null)
            {
                tab.ColocarPeca(capiturada, destino);
                Capiturada.Remove(capiturada);
            }
            // #Roque Pequeno
            if (p is Rei && origem.Coluna == destino.Coluna - 2 )
            {
                Peca T = tab.RetirarPeca(new Posicao(destino.Linha, destino.Coluna - 1));
                T.DecrementarQuantidadeMovimento();
                tab.ColocarPeca(T, new Posicao(origem.Linha, origem.Coluna + 2));
            }

            // Roque Grande
            if(p is Rei && destino.Coluna == origem.Coluna  + 2 )
            {
                Peca T = tab.RetirarPeca(new Posicao(origem.Linha, origem.Coluna - 1));
                T.DecrementarQuantidadeMovimento();
                tab.ColocarPeca(T, new Posicao(origem.Linha, origem.Coluna - 4));
            }

            // #JogadaEspecial En Passant
            if (p is Peao && VulneravelEnPassant == capiturada)
            {
                if (p.Cor == Cor.Branca)
                {
                    Posicao posicaoP = new Posicao(destino.Linha - 1, destino.Coluna);
                    Peca peao = tab.RetirarPeca(destino);
                    tab.ColocarPeca(peao, posicaoP);
                }
                else
                {
                    Posicao posicaoP = new Posicao(destino.Linha + 1, destino.Coluna);
                    Peca peao = tab.RetirarPeca(destino);
                    tab.ColocarPeca(peao, posicaoP);
                }
            }
            tab.ColocarPeca(p , origem);
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca capiturada = ExecutaMovimento(origem, destino);
            if (EstaEmXeque(JogadorAtual))
            {
                DesfazMovimento(origem, destino, capiturada);
                throw new DomainException("Você não pode se colocar em xeque! ");
            }
            if (EstaEmXeque(Adversaria(JogadorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }
            
            if (TesteXequeMate(Adversaria(JogadorAtual)))
            {
                Terminada = true;
            }
            else
            {
                Turno++;
                MudaJogador();
            }

            Peca p = tab.peca(destino);
            // #Jogada especial 
            if (p is Peao)
            {
                if (destino.Linha == 0 || destino.Linha == 7)
                {

                    int condicao = Tela.Promocao();
                    switch (condicao)
                    {
                        case 1:
                            Peca removida = tab.RetirarPeca(destino);
                            Pecas.Remove(removida);
                            tab.ColocarPeca(new Rainha(tab, p.Cor), destino);
                            Pecas.Add(new Rainha(tab, p.Cor));
                            break;
                        case 2:
                            Peca removida2 = tab.RetirarPeca(destino);
                            Pecas.Remove(removida2);
                            tab.ColocarPeca(new Cavalo(tab, p.Cor), destino);
                            Pecas.Add(new Cavalo(tab, p.Cor));
                            break;
                        case 3:
                            Peca removida3 = tab.RetirarPeca(destino);
                            Pecas.Remove(removida3);
                            tab.ColocarPeca(new Torre(tab, p.Cor), destino);
                            Pecas.Add(new Torre(tab, p.Cor));
                            break;
                        case 4:
                            Peca removida4 = tab.RetirarPeca(destino);
                            Pecas.Remove(removida4);
                            tab.ColocarPeca(new Bispo(tab, p.Cor), destino);
                            Pecas.Add(new Bispo(tab, p.Cor));
                            break;
                        default:
                            Console.WriteLine("Opção inválida");
                            break;
                    }
                }
            }
            // #JogadaEspecial en passant
            if (p is Peao)
            {
                if (destino.Linha == origem.Linha + 2 || destino.Linha == origem.Linha - 2)
                {
                    VulneravelEnPassant = p;
                }
            }            
        }
        private void MudaJogador()
        {
            if(JogadorAtual == Cor.Branca)
            {
                JogadorAtual = Cor.Preta;
            }
            else
            {
                JogadorAtual = Cor.Branca;
            }
        }
        public void ValidarPosicaoOrigem(Posicao pos)
        {
            if(tab.peca(pos) == null)
            {
                throw new DomainException("Não existe peça nessa posição");
            }
            if (JogadorAtual != tab.peca(pos).Cor)
            {
                throw new DomainException("A peça de origem não é a da roda atual.");
            }
            if (!tab.peca(pos).ExisteMovimentosPossiveis())
            {
                throw new DomainException("Não existe movimentos possíveis para essa peça");               
            }
        }
        public void ValidarPosicaoDestino(Posicao origem, Posicao destino)
        {
            if (!tab.peca(origem).MovimentoPossivel(destino))
            {
                throw new DomainException("A execução desse movimento de destino é invalida");
            }
        }
        public  HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca x in Capiturada)
            {
                if(x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }
        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in Pecas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }
        private Cor Adversaria(Cor cor)
        {
            if(cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }
        private Peca Rei(Cor cor)
        {
            foreach(Peca x in PecasEmJogo(cor))
            {
                if(x is Rei)
                {
                    return x;
                }
            }
            return null;
        }
        public bool EstaEmXeque(Cor cor)
        {
            Peca R = Rei(cor);
            if(R == null)
            {
                throw new DomainException("Não tem rei da cor " + cor + " no tabuleiro");
            }
            foreach(Peca x in PecasEmJogo(Adversaria(cor)))
            {
                bool[,] mat = x.MovimentosPossiveis();
                if (mat[R.Posicao.Linha, R.Posicao.Coluna])
                {
                    return true;
                }
            }
            return false;
        }
        public bool TesteXequeMate(Cor cor)
        {
            if (!EstaEmXeque(cor))
            {
                return false;
            }
            foreach(Peca x in PecasEmJogo(cor))
            {
                bool[,] mat = x.MovimentosPossiveis();

                for(int i = 0; i < tab.Linhas; i++)
                {
                    for (int j = 0; j < tab.Colunas; j++)
                    {
                        if(mat[i, j])
                        {
                            Posicao origem = x.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca Pecacapiturada = ExecutaMovimento(origem,destino);
                            bool TesteXeque = EstaEmXeque (cor);
                            DesfazMovimento(origem, destino, Pecacapiturada);
                            if (!TesteXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            Pecas.Add(peca);
        }
        private void ColocarPecas()
        {
            ColocarNovaPeca('h', 1, new Torre(tab, Cor.Branca));
            ColocarNovaPeca('g', 1, new Cavalo(tab, Cor.Branca));
            ColocarNovaPeca('f', 1, new Bispo(tab, Cor.Branca));
            ColocarNovaPeca('e', 1, new Rei(tab, Cor.Branca, this));
            ColocarNovaPeca('d', 1, new Rainha(tab, Cor.Branca));
            ColocarNovaPeca('c', 1, new Bispo(tab, Cor.Branca));
            ColocarNovaPeca('b', 1, new Cavalo(tab, Cor.Branca));
            ColocarNovaPeca('a', 1, new Torre(tab, Cor.Branca));
            ColocarNovaPeca('a', 2, new Peao(tab, Cor.Branca, this));
            ColocarNovaPeca('b', 2, new Peao(tab, Cor.Branca, this));
            ColocarNovaPeca('c', 2, new Peao(tab, Cor.Branca, this));
            ColocarNovaPeca('d', 2, new Peao(tab, Cor.Branca, this));
            ColocarNovaPeca('e', 2, new Peao(tab, Cor.Branca, this));
            ColocarNovaPeca('f', 2, new Peao(tab, Cor.Branca, this));
            ColocarNovaPeca('g', 2, new Peao(tab, Cor.Branca, this));
            ColocarNovaPeca('h', 2, new Peao(tab, Cor.Branca, this));


            ColocarNovaPeca('a', 8, new Torre(tab, Cor.Preta));
            ColocarNovaPeca('b', 8, new Cavalo(tab, Cor.Preta));
            ColocarNovaPeca('c', 8, new Bispo(tab, Cor.Preta));
            ColocarNovaPeca('d', 8, new Rainha(tab, Cor.Preta));
            ColocarNovaPeca('e', 8, new Rei(tab, Cor.Preta, this));
            ColocarNovaPeca('f', 8, new Bispo(tab, Cor.Preta));
            ColocarNovaPeca('g', 8, new Cavalo(tab, Cor.Preta));
            ColocarNovaPeca('h', 8, new Torre(tab, Cor.Preta));
            ColocarNovaPeca('a', 7, new Peao(tab, Cor.Preta, this));
            ColocarNovaPeca('b', 7, new Peao(tab, Cor.Preta, this));
            ColocarNovaPeca('c', 7, new Peao(tab, Cor.Preta, this));
            ColocarNovaPeca('d', 7, new Peao(tab, Cor.Preta, this));
            ColocarNovaPeca('e', 7, new Peao(tab, Cor.Preta, this));
            ColocarNovaPeca('f', 7, new Peao(tab, Cor.Preta, this));
            ColocarNovaPeca('g', 7, new Peao(tab, Cor.Preta, this));
            ColocarNovaPeca('h', 7, new Peao(tab, Cor.Preta, this));
        }
    }
}
