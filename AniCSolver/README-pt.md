Select Language: [English](https://github.com/AnisanWesley/anisan-college/blob/master/Anisoket/README.md), **Portuguese**
AniCSolver
========

####Requisito: Conhecimento básico no programa Expert Sinta.

Esta é uma extensão para a shell do Expert Sinta
Basicamente seu objetivo é ler os arquivos gerados pela base do Expert Sinta e traduzir tudo em objetos e disponibilizar isso para o usuário da classe, você terá acesso à:
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


#### 1 - Criar uma instância da classe `Sistem`
Passando dois argumentos que são o path dos arquivos gerados pelo ExSinta:
```csharp
var sistema = new Sistema("c:\\temp\\base.txt", "c:\\temp\\codigos.txt");
```

A este ponto já temos `solucao.Nome`, `solucao.Autores` e `solucao.Resumo` que foram extraídos do Metadata do ExSinta.

#### 2 - Pergunte para o sistema com `sistema.Perguntar()` **(opcional)**
Este método vai simplesmente retornar a próxima pergunta a ser feita, ela consiste de
* `Variavel` que está relacionada àquela pergunta
* Descrição da pergunta (que é ela de fato)
* Motivo da pergunta

Lembrando que tudo isso também foi extraído do Metadata do ExSinta, então deve-se configurar a interface da base para que venha tudo correto;


#### 3 - Responda ao sistema com `sistema.Responder(params string)`
Basicamente responda ao sistema com strings curtas dependendo do tipo das respostas.
Pode passar uma quantidade indeterminada de parâmetros, se souber as respostas das seguintes perguntas

Exemplo: `sistema.Responder("sim","s","n","alternativa a","alternativa c","nao")`. 
Cada parametro responderá a pergunta correspondente à ordem que as regras forem selecionadas.

Mas o mais correto é chamar `sistema.Perguntar()` para saber o que se está respondendo.
Pode mandar estes valores:
* **Verdadeiro/Falso:** para perguntas deste tipo, ele simplesmente atribui como verdade o texto que conter a letra S
	*	Exemplo: Pode mandar `"sim"`, `"yes"` ou `"s"` que será como responder `verdadeiro`, caso contrario, será como responder `falso`.
* **valores predefinidos**: Deve-se responder exatamente como as opções, mas não é case-sensitive.
	*	Exemplo: `Qual tipo de bips da placa mãe?` Opções: `curtos`,`longos` e `ambos`. Neste caso `Ambos` ou `ambos` seria uma resposta válida
* **Acessar o motivo da pergunta** pode ser feita de duas formas:
	*	Adicionando um ponto de interrogação à resposta. Ex: `sistema.Responder("motivo?")`,`sistema.Responder("por que?")`.
	*	Acessando `sistema.Perguntar().Motivo`, já que este retorna um objeto do tipo `Pergunta`
	

##Métodos Auxiliares
* `GetRegrasRegeitadas()`
* `GetRegrasAtivas()`

---
##Contribuição

**Este projeto foi feito para um trabalho da faculdade, apenas resolveu o meu problema, funciona com bastante coisa mas não está sulficientemente genérico.
Fork-me e ajude a deixar esta Shell ainda mais completa.**
####Limitações conhecidas (implementações pendentes)

* Valores e variaveis dever ter nomes diferentes se forem diferentes de Sim e Nao
* Não atribuir valores como `sim` e `nao`, deixar vazio, pois se for boleano, o sistema já trata isso
* Não suporta condição `OU`
* Fator de confiança deve ser sempre `100%`
* Apenas variaveis univaloradas
* Nomes de valores, variaveis não podem ser UPPERCASE
* Somente termos de igualdade, ainda não faz com `<>`
* Valores, perguntas, soluções não deve conter os seguintes caracteres `:`,`,`,`"`. (usa para fazer o `split`, um `regex` resolveria)
* Metadata deve conter apenas um objetivo.
* Perguntas necessitam estar no metadata.
 */
