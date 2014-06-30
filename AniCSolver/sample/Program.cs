using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniCSolver.Core
{
    internal static class Program
    {
        private static void Main()
        {
            var sair = false;
            for (; !sair; )
            try
            {
                Console.Clear();
                Console.WriteLine("Digite 'help' para obter ajuda.\n");
				
                var sistema = new Sistema("c:\\temp\\base.txt", "c:\\temp\\codigos.txt");
                Console.WriteLine(sistema.Resumo);
                for (; sistema.Solucao == null;)
                {
                    var pergunta = sistema.Perguntar();

                    Console.WriteLine(pergunta.Descricao);

                    var variavel = pergunta.Variavel;

                    if (!variavel.Valores.IsEmpty())
                    {
                        Console.WriteLine("\nOpções:\n");
                        foreach (var valor in variavel.Valores)
                            Console.WriteLine(valor.Value.Nome);

                    }

                    var resposta = Console.ReadLine();

                    Console.WriteLine(sistema.Responder(resposta));
                

            }

                Console.WriteLine("Solução: " + sistema.Solucao);
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine(
                    "Digitou alguma variavel errado, procure digitar exatamente igual às alternativas caso forem diferentes de Sim e Não");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Coloque os arquivos base.txt e codigos.txt gerados pelo Expert Sinta em C:\\temp\\");
            }
            finally
            {
                Console.WriteLine("\n'Enter para continuar ou 'sair' para sair'.");
                sair = Console.ReadLine() == "sair";
            }

        }


        
       

    }
}