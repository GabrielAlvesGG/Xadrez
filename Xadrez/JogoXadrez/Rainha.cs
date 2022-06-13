using System;
using System.Collections.Generic;
using System.Text;
using Xadrez.tabuleiro;

namespace Xadrez.JogoXadrez
{
    class Rainha : Peca
    {
        public Rainha (Tabuleiro tab, Cor cor) : base(cor, tab)
        {
        }
        public override string ToString()
        {
            return "D";
        }
        public  bool PodeMover(Posicao pos)
        {
            Peca p = Tab.peca(pos);
            return p != null || Tab.peca(pos).Cor != Cor;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Posicao.Linha, Posicao.Coluna];

            Posicao pos = new Posicao(0, 0);

            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            while(Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if(Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor)
                {
                    break;
                }
            }
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            while(Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if(Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor)
                {
                    break;
                }
            }
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
            while(Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if(Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor)
                {
                    break;
                }
            }
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            while(Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor)
                {
                    break;
                }
            }
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            while (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true; 
                if(Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor)
                {
                    break;
                }
            }
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            while(Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true; 
                if(Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor)
                {
                    break;
                }
            }
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
            while(Tab.PosicaoValida(pos ) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if(Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor)
                {
                    break;
                }
            }
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            while(Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor)
                {
                    break;
                }
            }
            return mat;
        }
       
    }
}
