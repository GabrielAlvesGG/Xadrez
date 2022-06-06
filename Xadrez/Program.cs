using System;
using Xadrez.Tabuleiro;
namespace Xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            Posicao p;
            p = new Posicao(3,4);
            Console.WriteLine("Posição: " + p);
            Console.WriteLine("Hello World!");
        }
    }
}
