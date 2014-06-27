##Changelog
Tags like **[c#]** and **[java]** represents if the changes in languages were made.

####Known errors
* still not work with multi line in requests

---
## changeset #140606 ver 1.2.5
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

---
## changeset #140514 ver 1.0.5
**[c#]** [java]

* Added `bool _isStarted`
* **Configure**: returns `bool` and set `_isStarted` to `true`
````csharp
public bool Configure(As typeConnection, string ip, string port)
{
   _ip = ip;
   {...}
   return _isStarted = true;
}
````
* **SendData**: 
   * returns `_isStarted`, indicate whether the method was called before `Configure()`
   * `if (_isStarted) switch (_typeConnection) ...`
   * Added `try` with `catch` returning `false`
* **ReceiveData**: 
   * `if (_isStarted) switch (_typeConnection) ...`
   * Added `try` with `catch` returning `exception.Message`

---
## changeset #140505 ver 1.0.1
**[c#] [java]**

* `ForceClose()` check nullity for `_soket` and `_client`.
* `GetLocalIp` returns `string[]`.

---
## changeset #140408 ver. 1.0.0
--initial commit