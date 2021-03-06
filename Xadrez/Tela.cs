using System;
using Xadrez.tabuleiro;
using System.Collections.Generic;
using Xadrez.JogoXadrez;
using Xadrez.tabuleiro.TabuleiroException;

namespace Xadrez
{
    class Tela
    {
        public static void ImprimirPartida(PartidaXadrez partida) 
        {
            ImprimirTabuleiro(partida.tab);
            Console.WriteLine();
            ImprimirPecaCapturada(partida);
            Console.WriteLine("Turno :" + partida.Turno);

            if (!partida.Terminada)
            {
                Console.WriteLine("Aguadando jogada : " + partida.JogadorAtual);
                if (partida.Xeque)
                {
                    Console.WriteLine("Xeque!!");
                }
            }
            else
            {
                Console.WriteLine("Xeque mate ");
                Console.WriteLine("Vencedor : " + partida.JogadorAtual);
            }
        }
        public static void ImprimirPecaCapturada(PartidaXadrez partida)
        {
            Console.WriteLine("Peças capturadas: ");
            Console.Write("Brancas: ");
            ImprimirConjunto(partida.PecasCapturadas(Cor.Branca));
            Console.WriteLine();
            Console.Write("Pretas: ");
            ConsoleColor aux = Console.ForegroundColor; 
            Console.ForegroundColor = ConsoleColor.Yellow;
            ImprimirConjunto(partida.PecasCapturadas(Cor.Preta));
            Console.ForegroundColor = aux;
            Console.WriteLine();
            Console.WriteLine();
        }
        public static void ImprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("{");
            foreach(Peca x in conjunto)
            {
                Console.Write(x + " ");
            }
            Console.Write("}");
        }
        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.Colunas; j++)
                {
                    ImprimirPeca(tab.peca(i,j)); 
                }
                Console.WriteLine();                
            }
            Console.WriteLine("  a b c d e f g h");
        }
        public static void ImprimirTabuleiro(Tabuleiro tab , bool [,] posicoesPossiveis)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor FundoAlterado = ConsoleColor.DarkGray;
            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.Colunas; j++)
                {   
                    if(posicoesPossiveis [i,j] == true)
                    {
                        Console.BackgroundColor = FundoAlterado;
                    }
                    else
                    {
                        Console.BackgroundColor = fundoOriginal;
                    }
                    ImprimirPeca(tab.peca(i, j));
                    Console.BackgroundColor = fundoOriginal; 
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = fundoOriginal;
        }
        public static PosicaoXadrez lerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + " ");
            
            return new PosicaoXadrez(coluna, linha);
        }
        public static int Promocao()
        {
            Console.WriteLine("Digite o numero do tipo de promoção desejada: ");
            Console.WriteLine(" 1 - Dama");
            Console.WriteLine(" 2 - Cavalo");
            Console.WriteLine(" 3 - Torre");
            Console.WriteLine(" 4 - Bispo");
            int condicao = int.Parse(Console.ReadLine());
            return condicao;
        }
        public static void ImprimirPeca(Peca peca)
        {
            if (peca == null)
            {
                Console.Write("- ");
            }
            else
            {

                if (peca.Cor == Cor.Branca)
                {
                    Console.Write(peca);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }
    }
}
