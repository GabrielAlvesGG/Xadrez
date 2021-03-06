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
                PartidaXadrez partida = new PartidaXadrez();
                while(!partida.Terminada)
                {
                    try
                    {
                        Console.Clear();
                        Tela.ImprimirPartida(partida);
                        Console.WriteLine();
                        Console.Write("Digite a posição de origem: ");
                        Posicao origem = Tela.lerPosicaoXadrez().ToPosicao();
                        partida.ValidarPosicaoOrigem(origem);

                        bool[,] posicoesPossiveis = partida.tab.peca(origem).MovimentosPossiveis();
                        Console.Clear();

                        Tela.ImprimirTabuleiro(partida.tab, posicoesPossiveis);
                        Console.WriteLine();
                        Console.Write("Digite posição destino: ");
                        Posicao destino = Tela.lerPosicaoXadrez().ToPosicao();
                        partida.ValidarPosicaoDestino(origem, destino); 

                        partida.RealizaJogada(origem, destino);
                    }
                    catch(DomainException e)
                    {
                        Console.WriteLine("Erro ! " + e.Message);
                        Console.ReadLine();
                    }
                }
                Console.Clear();
                Tela.ImprimirPartida(partida); 
                
            }
            catch(DomainException e)
            {
                Console.Write(e.Message);
            }
            Console.ReadLine();
        }
    }
}
