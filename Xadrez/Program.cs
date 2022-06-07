using System;
using Xadrez.tabuleiro;
using Xadrez.JogoXadrez;
using Xadrez.tabuleiro.TabuleiroException;
using Xadrez.JogoXadrez;

namespace Xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            PosicaoXadrez pos = new PosicaoXadrez('C', 7);

            Console.WriteLine(pos);
            Console.WriteLine(pos.ToPosicao());
            Console.ReadLine();
        }
    }
}
 