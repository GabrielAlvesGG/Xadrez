﻿namespace Xadrez.tabuleiro
{
    class Peca
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
    }
}

