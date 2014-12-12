AniWebsql
========

Depois faço um leiame mais elaborado

Mas à principio, é uma interface para criar, inserir dados, deletar dados e dropar tabelas dinamicamente
Também exibe resultados e aceita condições personalizadas. Ex: `AND Nome == 'teste'`

Há quatro arquivos principais:
* SQLController
 * Responsável por interagir com o html, e direcionar os dados para as outras camadas.
* SQLFactory
 * Simplemente, retorna a query em formato de string.
* SQLManager
 * Responsável executar as queries geradas pelo SQLFactory.
