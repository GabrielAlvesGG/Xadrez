using System;
using System.Collections.Generic;
using System.Text;
using Xadrez.tabuleiro;

namespace Xadrez.JogoXadrez
{
    class Peao : Peca
    {
        private PartidaXadrez partida;
        public Peao(Tabuleiro tab, Cor cor, PartidaXadrez partida ) : base(cor, tab)
        {
            this.partida = partida;
        }
        public override string ToString()
        {
            return "P";
        }
        public bool ExisteInimigo(Posicao pos)
        {
            Peca p = Tab.peca(pos);
            return p != null && Tab.peca(pos).Cor != Cor;
        }
        private bool Livre(Posicao pos)
        {
            return Tab.peca(pos) == null;
        }
        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];

            Posicao pos = new Posicao(0, 0);

            if(Cor == Cor.Branca)
            {
                pos.DefinirValores(Posicao.Linha - 1 , Posicao.Coluna);
                if(Tab.PosicaoValida(pos) && Livre(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(Posicao.Linha - 2, Posicao.Coluna);
                if(Tab.PosicaoValida(pos) && Livre(pos) && QuantMovimento == 0)
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
                if(Tab.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
                if(Tab.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                // #JogadaEspecial EnPassant

                if(Posicao.Linha == 3)
                {
                    Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    if (Tab.PosicaoValida(esquerda) && Tab.peca(esquerda) is Peao && Tab.peca(esquerda).Cor != Cor && Tab.peca(esquerda) == partida.VulneravelEnPassant)
                    {
                        mat[Posicao.Linha - 1, Posicao.Coluna - 1 ] = true;
                    }

                    Posicao direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    if(Tab.PosicaoValida(direita) && Tab.peca(direita) is Peao && Tab.peca(direita).Cor != Cor && Tab.peca(direita) == partida.VulneravelEnPassant) {
                        mat[Posicao.Linha - 1, Posicao.Coluna + 1 ] = true;
                    }
                }
            }
            else
            {
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
                if (Tab.PosicaoValida(pos) && Livre(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(Posicao.Linha + 2, Posicao.Coluna);
                if (Tab.PosicaoValida(pos) && Livre(pos) && QuantMovimento == 0)
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
                if (Tab.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
                if (Tab.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                // #JogadaEspecial En Passant 
                if (Posicao.Linha == 4)
                {
                    Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    if (Tab.PosicaoValida(esquerda) && Tab.peca(esquerda) is Peao && Tab.peca(esquerda).Cor != Cor && Tab.peca(esquerda) == partida.VulneravelEnPassant)
                    {
                        mat[Posicao.Linha + 1, Posicao.Coluna - 1] = true;
                    }

                    Posicao direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    if (Tab.PosicaoValida(direita) && Tab.peca(direita) is Peao && Tab.peca(direita).Cor != Cor && Tab.peca(direita) == partida.VulneravelEnPassant)
                    {
                        mat[Posicao.Linha + 1, Posicao.Coluna +1] = true;
                    }
                }
            }
            return mat;
        }
    }
}
