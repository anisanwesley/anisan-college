using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Linq;
using System.Text;

namespace TrabalhoSGBD
{
    class Program
    {
        static void Main(string[] args)
        {
            Informer("Gerando a ConnectionString.", 4);

            //Gerando a conectionString, que contém as informações de acesso ao banco
            string connectionString =
                @"Data Source=localhost\sqlexpress;Initial Catalog=Materiais;Integrated Security=True;Pooling=False";
            Console.WriteLine(connectionString);

            //Criando conexão com o Banco [Material.dbo]
            Informer("Criando conexão com o Banco [Material.dbo]", 7);
            SqlConnection conexaoBanco = new SqlConnection(connectionString);

            //Abrindo conexão com o banco.
            Informer("Abrindo conexão com o banco.", 4);
            conexaoBanco.Open();
            Informer("Conexão aberta", 0);

            //Atribuindo ligação entre comando-conexão.
            Informer("Atribuindo ligação entre comando-conexão.", 2);
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexaoBanco;

            Informer("Lendo scripts", 3);

            //Atribuindo os nomes dos arquivos em uma lista de argumentos
            args = new[]{
            "script_tabelas.sql",
            "script_pks.sql",
            "script_fks.sql",
            "script_index.sql",
            "script_stored.sql",
            "script_triggers.sql",
            "script_insert_cadastro.sql",
            "script_insert_unidade.sql",
            "script_insert_grupo.sql",
            "script_insert_fornecedor.sql",
            "script_insert_requisicao.sql",
            "script_insert_entrada.sql",
            "script_insert_material.sql",
            "script_insert_requisicao1.sql",
            "script_insert_entrada1.sql"
            
            };

            Informer("Carregando scripts", 4);
            foreach (string s in args)
            {
                Informer(String.Format("Carregando arquivo: [{0}]", s), 2);
            }

            Informer("Pronto...", 0, true);

            //Para cada arquivo na lista de argumentos
            foreach (string arquivo in args)
            {
                //Lê todas as linhas do arquivo e atribui à uma lista de querys
                Informer(String.Format("Executando o arquivo [{0}]", arquivo), 5);
                var querys = File.ReadAllLines(arquivo);

                foreach (string query in querys)
                {
                    //Atribui a query a ser executada ao objeto Comando
                    if (String.IsNullOrEmpty(query)) continue;
                    comando.CommandText = query;
                    Console.WriteLine("Execute: " + query);
                    if (arquivo != "script_insert_requisicao1.sql")
                        Thread.Sleep(20);
                    //Executa a query que pode ser um create, um insert ou um alter
                    comando.ExecuteNonQuery();
                }

                Informer(String.Format("\n\n-=-=-=-=-=-=\n\nFinalizado, querys de [{0}] executadas com sucesso.\n\nPronto para executar o proximo script.", arquivo), 0, true);
            }
            
            
           



            Informer("\nFinalizado! Todos os scripts foram executados com sucesso!",0);
            Console.ReadKey();
            Informer("\n\nEncerrando conexão com o banco.", 4);
            //Fecha a conexão com o banco.
            conexaoBanco.Close();
            Informer("\n\nCreditos:\n-Felipe Amaral\n-Gabriel de Lourensi\n-Wesley A.Lemos.\n\nUniplac - 2012.",2);
            Console.WriteLine("\n\nPrecione qualquer tecla para sair.");
            Console.ReadKey();




        }

        private static void Informer(string mensagem, int tempo, bool auto = false)
        {
            Console.WriteLine();
            Console.Write(mensagem);
            for (int i = 0; i < tempo; i++)
            {
                Thread.Sleep(i * 300);
                Console.Write(".");
            }

            if (auto)
            {
                Console.WriteLine("\n\nPressione qualquer tecla para continuar.");
                Console.ReadKey();
            }
        }
    }
}
