Select Language: **English**, [Portuguese](https://github.com/AnisanWesley/anisan-college/blob/master/Anisoket/README-pt.md)
Anisoket
========

Those classes have by objective simplify the configuration and usability of sockets in supported languages:
* [C#](https://github.com/AnisanWesley/anisan-college/blob/master/Anisoket/src/csharp/SoketSupport.cs)
* [Java](https://github.com/AnisanWesley/anisan-college/blob/master/Anisoket/src/java/SoketSupport.java)
* based in this [pseudocode](https://github.com/AnisanWesley/anisan-college/blob/master/Anisoket/src/pseudo-code/SoketSupport.pc)
 
In only three steps is possible avoid all the rework of initialize, configure and use different sockets for clients and servers.
Anisoket allows you to use three lines to do what is traditionally done in 11 lines.
##How to use


#### 1 - Create a instance of `SoketSupport`
Just add the class in project and instance this:
```csharp
var soket = new SoketSupport();
```

#### 2 - Invoke `Configure()` 
Passing by arguments:
* Type of socket (`As.Server` ou `As.Client`)
* Server's IP
* Server's Port

```csharp
soket.Configure(As.Client,"192.169.10.1","41000"); 
  //or
soket.Configure(As.Server,"192.169.10.1","41000");
```
#### 3 - Start the circle
Now you can call the following methods to trade data between client and server:
* `bool SendMessage(string text)`: Receives the text to be sent and returns a boolean that signifies the success of operation.
* `string ReceiveMessage()`: Return the message from another side, or the exception message.

If was configured with:
* `As.Client` : First must be requested server calling `SendMessage`, and so invoke `ReceiveMessage` to get response (or break with `ForceClose`).
* `As.Server` : First must be listen clients requests with `ReceibeMessage`, and so invoke `SednMessage` to return response to client (or break with `ForceClose`).

##Comparing
####Whitout Anisoket 
This code uses conventional pattern to do a simple connection from client to server:

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
####With Anisoket
Now the same method enhanced with `SoketSupport`, note the amount of code reduced lines and ease of reading:

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
##Auxiliary methods
* `GetTypeConnection()`: Returns kind of connection configured with enum `As`.
* `GetLocalIp()`: Returns a list with available IPs used for local machine.
* `ForceClose()`: Cancels the request, interrupting the cycle and making available to the next. (Not required)

---
##Contribution
####Humans behind the project

* [Wesley Lemos](https://github.com/AnisanWesley) - csharp
* [Felipe Amaral](https://github.com/furflez) - java
 
####Colabore too
Fork-me and help to improve this micro framework (;

##Extras
[Check all changesets](https://github.com/AnisanWesley/anisan-college/blob/master/Anisoket/Changelog.md) and [examples](https://github.com/AnisanWesley/anisan-college/tree/master/Anisoket/samples)

####Last Changeset #140606 ver 1.2.5
**[c#]** [java]

* **Configure**
   * Checks `typeConnection` with `return _isStarted = _typeConnection != As.NotSet;`
   * Added useful overloads for `Configure()`
````csharp

   Configure(As typeConnection, IPAddress ip, int port){...}
   Configure(As typeConnection, string ip, int port){...}
   Configure(As typeConnection, IPAddress ip, string port){...}
   Configure(As typeConnection, string ip, string port){...}
````
* **TypeConnection**
   * Added value `NotSet` 
* **ReceiveData** 
   * `return data ?? _typeConnection.ToString();`, will returns `NotSet` is not configured
* **GetLocalIp** is now `static`
