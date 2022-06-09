﻿namespace Xadrez.tabuleiro
{
    abstract class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QuantMovimento { get; protected set; }
        public Tabuleiro Tab { get; protected set; }

        public Peca(Cor cor, Tabuleiro tab)
        {
            Posicao = null;
            Cor = cor;
            Tab = tab;
            QuantMovimento = 0;
        }          
        public void IncrementarQuantidadeMovimento()
        {
            QuantMovimento++;
        }
        public bool ExisteMovimentosPossiveis()
        {
            bool[,] mat = MovimentosPossiveis();
            for(int i=0; i<Tab.Linhas; i++)
            {
                for(int j=0; j< Tab.Colunas; j++)
                {
                        if(mat [i,j]== true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool PodeMoverPara(Posicao pos)
        {
            return MovimentosPossiveis()[pos.Linha, pos.Coluna];
        }
        public abstract bool[,] MovimentosPossiveis();       
    }
}

