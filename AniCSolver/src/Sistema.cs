using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AniCSolver.Core
{
    public class Sistema
    {
        #region Métodos e propriedades publicas principais

        public string Nome { get; set; }
        public string Autores { get; set; }
        public string Resumo { get; set; }
        public string Solucao { get; set; }

        public Pergunta Perguntar()
        {
            var condicao = _condicoes.First(c => c.Status == Status.Desconhecido);
            return _perguntas[condicao.Variavel.Nome];
        }


        public void Responder(params string[] respostas)
        {
            foreach (var resposta in respostas)
               Responder(resposta);
            
        }

        public string Responder(string resposta)
        {
            resposta = resposta.ToLower();
         
            var pergunta = Perguntar();

            if (resposta.Contains("?")) return pergunta.Motivo;

            if (resposta==("help")) return Help;

            if (resposta.Contains(":")) return GetMetadata(resposta.Split(':')[1]);

            if (!String.IsNullOrWhiteSpace(Solucao)) return Solucao;

            var condicoes = _condicoes.FindAll(c => c.Variavel == pergunta.Variavel);

            foreach (var condicao in condicoes)
            {

                if (condicao.Variavel.Valores.IsEmpty()) //Valores boleanos
                {
                    var afirmando = resposta.Contains("y") || resposta.Contains("s");

                    if (afirmando != condicao.Boolean)
                    {
                        condicao.Status = Status.Regeitada;
                        condicao.Regra.Regeitada = true;
                    }
                    else
                    {
                        condicao.Status = Status.Aceita;

                    }

                }
                else //valores distintos
                {
                    condicao.Status = condicao.Valor.Nome.ToLower() == resposta
                        ? Status.Aceita
                        : Status.Regeitada;
                    condicao.Regra.Regeitada = condicao.Status == Status.Regeitada;

                }
            }

            var regrasRegeitadas = GetRegrasRegeitadas();
            var regrasAtivas = GetRegrasAtivas();

            //para cada regra regeitada, regeita todas suas condições
            regrasRegeitadas.ToList().ForEach(r => _condicoes.FindAll(c => c.Regra == r).ForEach(c => c.Status = Status.Regeitada));

            //busca um regra onde todas as condições forem aceitas e atribui como solução
            foreach (var regra in regrasAtivas)
            {
                condicoes = _condicoes.FindAll(c => c.Regra == regra);
                if (condicoes.Any(c => c.Status != Status.Aceita)) continue;
                Solucao = regra.Assertiva.Valor.Nome;
                    break;
            }
            return null;
        }

        private string GetMetadata(string command)
        {
            var sb = new StringBuilder();

            switch (command)
            {
                case "variaveis":
                    foreach (var v in _variaveis)
                        sb.AppendLine(v.ToString());
                    break;
                case "valores":
                    foreach (var v in _valores)
                        sb.AppendLine(v.ToString());
                    break;
                case "regras":
                    foreach (var r in _regras)
                        sb.AppendLine(r.ToString());
                    break;
                case "perguntas":
                    foreach (var p in _perguntas)
                        sb.AppendLine(p.ToString());
                    break;
                case "condicoes":
                    foreach (var r in _regras.Select(r=>r.Value))
                    {
                        var condicoes = GetCondicoesByRegra(r.Codigo);
                        sb.AppendLine("\n REGRA "+r.Codigo+" "+(r.Ativa?"":"[Regeitada]"));
                        foreach (var c in condicoes)
                            sb.AppendLine("    " + c);
                        sb.AppendLine("    " + r.Assertiva);
                    }
                    break;
                case "help":
                    sb.AppendLine(Help);
                    break;
            }

            return sb.ToString();
        }


        #endregion

        #region Métodos helpers
        public IEnumerable<Regra> GetRegrasRegeitadas()
        {
            return _regras.ToList().FindAll(r => r.Value.Regeitada).Select(r=>r.Value);
        }
        public IEnumerable<Regra> GetRegrasAtivas()
        {
            return _regras.ToList().FindAll(r => r.Value.Ativa).Select(r => r.Value).ToList();
        }
        public IEnumerable<Condicao> GetCondicoesByRegra(int codigo)
        {
            return _condicoes.FindAll(c=>c.Regra.Codigo==codigo);
        }
        #endregion

        



        #region Listas
        
        private readonly Dictionary<string, Variavel> _variaveis = new Dictionary<string, Variavel>();
        private readonly Dictionary<string, Valor> _valores = new Dictionary<string, Valor>();
        private readonly Dictionary<int, Regra> _regras = new Dictionary<int, Regra>();
        private readonly Dictionary<string, Pergunta> _perguntas = new Dictionary<string, Pergunta>();
        private readonly List<Condicao> _condicoes = new List<Condicao>();

        #endregion

        #region Geradores do construtor

        public static string Help
        {
            get
            {
                return @"
1 . Responda uma pergunta do tipo Verdadeiro/Falso com Sim ou Não
2 . Responda perguntas com mais de três opções indicando exatamente um dos valores informados
3 . Para saber o motivo da pergunta, digite '?'
4 . Para um comando personalizado, digite ':<comando>'
4a. Lista de comandos:
    ':variaveis' - retorna todas as variaveis do sistema
    ':valores' - retorna todos os valores do sistema
    ':regras' - retorna todas as regras do sistema
    ':perguntas' - retorna todas as perguntas do sistema
    ':condicoes' - retorna todas as condicoes do sistema agrupadas por regras
    ':help' - exibe esta ajuda
";
            }
        }
        private readonly string[] _linhasCode;
        private readonly string[] _linhasDescription;

        public Sistema(string descriptionFile, string codeFile)
        {
            _linhasCode = File.ReadAllLines(codeFile, Encoding.UTF7);
            _linhasDescription = File.ReadAllLines(descriptionFile, Encoding.UTF7);

            SetCabecalho();
            SetVariaveis();
            SetValores();
            SetRegras();
            SetObjetivos();
            SetPerguntas();
        }

        private void SetPerguntas()
        {
            var start = false;

            Pergunta pergunta = null;

            foreach (var linha in _linhasDescription)
            {
                if (start)
                {
                    if (linha.Contains("-----------")) break;

                    //seta pergunta
                    if (linha.Contains("Variável"))
                    {
                        pergunta = new Pergunta {Variavel = _variaveis[linha.Split(':')[1].Trim()]};
                        continue;
                    }

                    //seta descricao
                    if (linha.Contains("Pergunta"))
                    {
                        pergunta.Descricao = linha.Split('"')[1];
                        _perguntas.Add(pergunta.Variavel.Nome, pergunta);
                        continue;
                    }
                    //seta motivo
                    if (linha.Contains("Motivo"))
                    {
                        pergunta.Motivo = linha.Split('"')[1];

                    }

                }
                if (linha.Contains("PERGUNTAS"))
                    start = true;
            }

            //Relaciona pergunta/variavel e cria pergunta caso nao exista
            foreach (var variavel in _variaveis)
            {
                var key = variavel.Value.Nome;
                if (!_perguntas.ContainsKey(key))
                {
                    _perguntas.Add(key, new Pergunta
                    {
                        Variavel = variavel.Value
                    });
                }
            }
        }



        private void SetObjetivos()
        {
            var start = false;
            foreach (var linha in _linhasDescription)
            {

                if (start)
                {
                    if (!String.IsNullOrEmpty(linha))
                    {
                        if (linha.Contains("REGRAS")) break;

                        var variavel = _variaveis[linha.Trim()];
                        variavel.IsObjetivo = true;

                    }

                }

                if (linha.Contains("OBJETIVOS"))
                    start = true;

            }
        }

        private void SetCabecalho()
        {
            var isResumo = false;

            foreach (var linha in _linhasCode)
            {
                if (linha.Contains("-- Nome"))
                    Nome = linha.Split(':')[1].Trim();

                if (linha.Contains("-- Autores"))
                    Autores = linha.Split(':')[1].Trim();

                if (linha.Contains("SOBRE OS ARQUIVOS")) break;

                if (isResumo)
                {
                    Resumo += "\n" + linha;
                }

                if (linha.Contains("-- Resumo:"))
                    isResumo = true;


            }
        }

        private void SetVariaveis()
        {
            var start = false;
            foreach (var linha in _linhasCode)
            {
                if (start)
                {
                    if (!String.IsNullOrEmpty(linha))
                    {
                        if (linha.Contains("Variáveis - NOME, CÓDIGO")) break;

                        var split = linha.Split(',');
                        var variavel = new Variavel
                        {
                            Codigo = Convert.ToInt32(split[0]),
                            Nome = split[1].Trim()
                        };
                        _variaveis.Add(variavel.Nome, variavel);
                    }
                }

                if (linha.Contains("Variáveis - CÓDIGO, NOME"))
                    start = true;
            }
        }

        private void SetValores()
        {
            var start = false;

            foreach (var linha in _linhasCode)
            {
                if (start)
                {
                    if (!String.IsNullOrEmpty(linha))
                    {
                        if (linha.Contains("Valores - NOME, CÓDIGO, CÓDIGO DA VARIÁVEL, POSIÇÃO")) break;

                        var split = linha.Split(',');
                        var valor = new Valor
                        {
                            Codigo = Convert.ToInt32(split[0]),
                            Nome = split[1].Trim(),
                            Posicao = Convert.ToInt32(split[3]),
                        };

                        valor.AddVariavel(_variaveis.First(v => v.Value.Codigo == Convert.ToInt32(split[2])).Value);
                        _valores.Add(valor.Nome, valor);
                    }
                }
                if (linha.Contains("Valores - CÓDIGO, NOME, CÓDIGO DA VARIÁVEL, POSIÇÃO"))
                    start = true;
            }
        }

        private void SetRegras()
        {
            var start = false;
            {

                foreach (var linha in _linhasCode)
                {
                    if (start)
                    {
                        if (!String.IsNullOrEmpty(linha))
                        {
                            if (linha.Contains("Regras - CÓDIGO, NOME, POSIÇÃO")) break;

                            var split = linha.Split(',');
                            var regra = new Regra()
                            {
                                Posicao = Convert.ToInt32(split[0]),
                                Nome = split[1],
                                Codigo = Convert.ToInt32(split[2]),
                            };
                            _regras.Add(regra.Posicao, regra);
                        }
                    }
                    if (linha.Contains("POSIÇÃO, NOME, CÓDIGO"))
                        start = true;
                }
            }

            {
                start = false;

                Regra regra = null;
                foreach (var linha in _linhasDescription)
                {

                    if (start)
                    {
                        if (!String.IsNullOrEmpty(linha))
                        {
                            if (linha.Contains("PERGUNTAS")) break;

                            //Pega regra
                            if (linha.Contains("Regra"))
                            {
                                regra = _regras[Convert.ToInt32(linha.Split('a')[1].Trim())];
                                continue;
                            }

                            //seta condição
                            if (linha.Contains(" SE ") || linha.Contains(" E "))
                            {
                                var split = linha.Split('E')[1].Split('=');

                                var variavel = split[0].Trim();
                                var valor = split[1].Trim();

                                var condicao = new Condicao();

                                condicao.Variavel = _variaveis[variavel];

                                if (valor != "Sim" && valor != "Não")
                                {
                                    condicao.Valor = _valores[valor];
                                }
                                else
                                {
                                    if (valor == "Sim") condicao.Boolean = true;
                                }

                                _condicoes.Add(condicao);
                                regra.AddCondicao(condicao);


                                continue;

                            }

                            //seta assertiva ('entao')
                            if (linha.Contains(" ENTÃO "))
                            {
                                var temp = linha.Split('O');
                                var temp2 = "";
                                string[] split;
                                if (temp.Length > 2)
                                {
                                    for (int i = 1; i < temp.Length; i++)
                                    {
                                        temp2 += (i == 1 ? "" : "O") + temp[i];
                                    }

                                }
                                else
                                {
                                    temp2 = temp[1];
                                }
                                split = temp2.Replace("CNF 100%", "").Split('=');



                                var variavel = split[0].Trim();
                                var valor = split[1].Trim();

                                var condicao = new Condicao();
                                condicao.Variavel = _variaveis[variavel];

                                if (valor != "Sim" && valor != "Não")
                                {
                                    condicao.Valor = _valores[valor];
                                }
                                else
                                {
                                    if (valor == "Sim") condicao.Boolean = true;
                                }

                                condicao.IsAcertiva = true;
                                regra.Assertiva = condicao;


                            }

                        }
                    }
                    if (linha.Contains("REGRAS"))
                        start = true;
                }
            }
        }

        /*-------PSEUDOCODE DAS ESTRUTURAS DE LEITURA
         * 
         *  INITIALIZE start AS FALSE;
            FOREACH fileline
                SE start
                    IF fileline IS NOT EMPTY
                        IF fileline CONTAINS end reading tag
                            BREAK;
                        END IF

                       Here goes implementation
                             {...}
          
                    END IF
                END SE

                IF fileline CONTAINS start reading tag
                    start IS TRUE;
                END IF
            END FOREACH
         * 
         */


        #endregion




    }

}
