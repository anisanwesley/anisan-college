Select Language: [English](https://github.com/AnisanWesley/anisan-college/blob/master/Anisoket/README.md), **Portuguese**
Anisoket
========

Estas classes tem por objetivo simplificar a configuração e utilização de sockets nas linguagens suportadas:
* [C#](https://github.com/AnisanWesley/anisan-college/blob/master/Anisoket/src/csharp/SoketSupport.cs)
* [Java](https://github.com/AnisanWesley/anisan-college/blob/master/Anisoket/src/java/SoketSupport.java)
* Baseado neste [Pseudocode](https://github.com/AnisanWesley/anisan-college/blob/master/Anisoket/src/pseudo-code/SoketSupport.pc)
 
Em apenas três passos simples é possivel evitar todo o retrabalho de inicializar, configurar, e usar sockets diferentes para clientes e servidores.
Anisoket permite utilizar em **3** linhas o que tradicionalmente se usaria **11** linhas.

##Usabilidade


#### 1 - Criar uma instância da classe `SoketSupport`
Basta adicionar a classe ao projeto e instancia-la:
```csharp
var soket = new SoketSupport();
```

#### 2 - Invoke `Configure()` 
Passando por argumentos:
* Tipo do soket (`As.Server` ou `As.Client`)
* IP do servidor
* Porta do servidor

```csharp
soket.Configure(As.Client,"192.169.10.1","41000"); //Configurado como cliente
                                                   //ou
soket.Configure(As.Server,"192.169.10.1","41000"); //Configurado como servidor
```
#### 3 - Iniciar o ciclo
Agora você tem acesso aos seguintes métodos para trocar dados entre cliente e servidor:
* `bool SendMessage(string text)`: Recebe o texto a ser enviado e retorna um valor boleano que representa o sucesso da operação.
* `string ReceiveMessage()`: Retorna a mensagem da outra ponta, ou a mensagem de exception em caso de falha.

Se foi configurado com:
* `As.Client` : primeiro deve fazer a chamada para o servidor usando o `SendMessage`, e então invocar o `ReceiveMessage` para obter sua resposta.
* `As.Server` : primeiro deve ouvir a resposta do cliente com `ReceiveMessage`, para então invocar o `SendMessage` para retornar a resposta para o cliente.

##Comparação
####Sem Anisoket 
Este é um código utilizando o padrão de sockets convencional para fazer uma conexão do cliente para o servidor:

```csharp
private string Conect(Get getMethod,string query)
{
try
{
     var response = String.Empty;
 
     var client = new TcpClient();
 
     client.Connect(IPAddress.Parse(OptionIp.GetIp()), 12345);
      
     var stream = client.GetStream();

     var reader = new StreamReader(stream);
     var writer = new StreamWriter(stream);

     if (client.Connected)
     {
         var data = String.Format("{0}:{1}:{2}",(int)getMethod,query,Environment.MachineName)
         writer.WriteLine(data);
         writer.Flush();
 
          response = reader.ReadToEnd();
                
  }
  client.Close();
}
catch (SocketException se)
    {
        return "Some problem: "+se.Message;
    }
  
  return response;
}
```
####Com Anisoket
Agora o mesmo método aprimorado com a classe SoketSupport, note quantidade de código reduzido e a facilidade de leitura:

```csharp
private string Conect(Get getMethod,string query)
{
    var soket = new SoketSupport();
                
    soket.Configure(As.Client,OptionIp.GetIp(),"40001");
             
     var data = String.Format("{0}:{1}:{2}",(int)getMethod,query,Environment.MachineName)
             
    soket.SendData(data);

    return soket.ReceiveData();
}
```
##Métodos Auxiliares
* `GetTypeConnection()`: Retorna o tipo de conexão informado no parâmetro `As`.
* `GetLocalIp()`: Retorna uma lista de IPs fisicos ou virtuais usados pela máquina local.
* `ForceClose()`: Cancela o request, interrompendo o ciclo e deixando disponível para o próximo. No caso de
  * Cliente: Deve ser chamado após `SendMessage()` para cancelar o recebimento de resposta do server.
  * Servidor: Deve ser chamado após `ReceiveMessage()` para cancelar o envio da resposta para o client

---
##Contribuição
####Cabeças por traz do projeto

* [Wesley Lemos](https://github.com/AnisanWesley) - csharp
* [Felipe Amaral](https://github.com/furflez) - java
 
####Colabore também
Fork-me e ajude a deixar este código melhor (;

##Changelog
[Veja todos os changesets](https://github.com/AnisanWesley/anisan-college/blob/master/Anisoket/Changelog.md) e [exemplos](https://github.com/AnisanWesley/anisan-college/tree/master/Anisoket/samples)

####Ultimo changeset #140606 ver 1.2.5
**[c#]** [java]

* **Configure**
   * Verifica `typeConnection` dessa forma: `return _isStarted = _typeConnection != As.NotSet;`
   * Adiciona sobre-cargas mais úteis para `Configure()`
````csharp

   Configure(As typeConnection, IPAddress ip, int port){...}
   Configure(As typeConnection, string ip, int port){...}
   Configure(As typeConnection, IPAddress ip, string port){...}
   Configure(As typeConnection, string ip, string port){...}
````
* **TypeConnection**
   * Adicionado valor `NotSet` 
* **ReceiveData** 
   * `return data ?? _typeConnection.ToString();`, irá retornar `NotSet` se ainda não configurado
* **GetLocalIp** agora é estático
