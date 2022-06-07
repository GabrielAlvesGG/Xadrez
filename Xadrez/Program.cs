using System;
using Xadrez.tabuleiro;
using Xadrez.JogoXadrez;
using Xadrez.tabuleiro.TabuleiroException;

namespace Xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Tabuleiro tab = new Tabuleiro(8, 8);
                tab.ColocarPeca(new Torre(tab, Cor.Preta), new Posicao(0, 0));
                tab.ColocarPeca(new Torre(tab, Cor.Preta), new Posicao(1, 3));
                tab.ColocarPeca(new Rei(tab, Cor.Preta), new Posicao(0, 2));

                tab.ColocarPeca(new Torre(tab, Cor.Branca), new Posicao(3, 4));
                tab.ColocarPeca(new Torre(tab, Cor.Branca), new Posicao(6, 2));
                
                Tela.ImprimirTabuleiro(tab);
            }
            catch(DomainException e)
            {
                Console.Write(e.Message);
            }
            Console.ReadLine();
        }
    }
}
