Select Language: [English](https://github.com/AnisanWesley/anisan-college/blob/master/Anisoket/README.md), **Portuguese**
AniCSolver
========
(Any Common Solver)
####Requisito: Conhecimento básico no programa Expert Sinta.

Esta é uma extensão para a shell do [Expert Sinta](http://www.lia.ufc.br/~bezerra/exsinta/). [(download)](ftp://ftp.lia.ufc.br/sinta/sinta.zip)
Basicamente seu objetivo é ler os arquivos gerados pela base do Expert Sinta e traduzir tudo em objetos e disponibilizar isso para o usuário da classe, você terá acesso à componentes da Metadata¹ como:
* Variaveis
* Valores
* Regras
* Perguntas
* Condições
* Lógica para definir a solução da base.
* Interface
	* Perguntas personalizadas
	* Motivos para tais perguntas


##Usabilidade


#### 1 - Crie uma instância da classe `Sistema`
Passando dois argumentos que são o path dos arquivos gerados pelo ExSinta:
```csharp
var sistema = new Sistema("c:\\temp\\base.txt", "c:\\temp\\codigos.txt");
```

A este ponto já temos `solucao.Nome`, `solucao.Autores` e `solucao.Resumo` e todos os demais dados listados acima que foram extraídos do Metadata do ExSinta.

#### 2 - Pergunte para o sistema com `sistema.Perguntar()` **(opcional)**
Este método vai simplesmente retornar a próxima pergunta a ser feita, ela consiste de:
* `Variavel` que está relacionada àquela pergunta
* `Descrição` da pergunta (que é ela de fato)
* `Motivo` da pergunta

Caso não informe motivo ou descrição da pergunta ou ela não esteja configurada na interface do ExSinta, ele vai geral isso com valores padrão.


#### 3 - Responda ao sistema com `sistema.Responder(params string)`
Basicamente responda ao sistema com strings curtas dependendo do tipo das respostas.
Pode passar uma quantidade indeterminada de parâmetros, se souber as respostas das perguntas seguintes.

**Exemplo:** `sistema.Responder("sim","s","n","alternativa a","alternativa c","nao")`. 
Cada parametro responderá a pergunta correspondente à ordem que as regras forem selecionadas.

Mas o mais correto é chamar `sistema.Perguntar()` para saber o que se está respondendo.

`Responder`pode aceitar estes tipos valores dependendo da próxima pergunta:
* **Verdadeiro/Falso:** para perguntas deste tipo, ele simplesmente atribui como verdade o texto que conter a letra `s`
	*	Exemplo: Pode mandar `"sim"`, `"yes"` ou `"s"` que será como responder `verdadeiro`, caso contrario, será como responder `falso`.
* **Valores predefinidos**: Deve-se responder exatamente como as opções.
	*	Exemplo: `Qual tipo de bips da placa mãe?` Opções: `curtos`,`longos` e `ambos`. Neste caso `Ambos` ou `ambos` seriam respostas válidas
* **Acessar o motivo da pergunta:** pode ser feita de duas formas:
	*	Adicionando um ponto de interrogação à resposta. Ex: `sistema.Responder("motivo?")`,`sistema.Responder("por que?")`.
	*	Acessando `sistema.Perguntar().Motivo`, já que este retorna um objeto do tipo `Pergunta`

**Note:** Nada é case-sensitive, `'motivo' == 'mOtiVO'`

##Métodos Auxiliares
* `GetRegrasRegeitadas()`
* `GetRegrasAtivas()`

---
##Contribuição

**Este projeto foi feito para um trabalho da faculdade, apenas resolveu o problema meu e de alguns colegas da turma, funciona com bastante coisa mas ainda apresenta varios bugs com metadados diversificados pois não está sulficientemente genérico.
Fique-se a vontade para fazer um Fork e ajudar a deixar esta Expansion Shell ainda mais completa.**
####Limitações conhecidas ( a.k.a. implementações pendentes)

* Valores e variaveis dever ter nomes distintos em todo o sistema
	* E.g. Não faça `gato.raca` e `cao.raca` e sim `gato.raca_gato` e `cao.raca_cao`
* Não atribuir valores como `sim` e `nao`, deixar vazio, pois se for boleano, o sistema já trata isso
* Não suporta condição `OU`
* Fator de confiança deve ser sempre `100%`
* Apenas variaveis `univaloradas`
* Nomes de valores, variaveis não podem ser `uppercase`
* Somente termos de igualdade, ainda não faz com `<>`
* Valores, perguntas, soluções não devem conter os seguintes caracteres `:`,`,`,`"`. (usa para fazer o `split`, um `regex` resolveria)
* Metadata deve conter apenas um objetivo. 

##Glossário
¹ **Metadata** - é o resultado lógico proveniente dos dois arquivos que são gerados pelo Expert Sinta: "Base de conhecimento" e "Códigos"