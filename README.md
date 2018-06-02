# .net-utility-kit
.NET reusable components.

##### Type Definition

```c#
public class Address
{
    public string Street { get; set; }
    public int Post { get; set; }

    public string GetAddress()
    {
        return $"Street: {Street}, Post: {Post}";
    }
}
```

```c#
public class User
{
    public string Name { get; set; }
    public Address Address { get; set; }
}
```

```c#
public class Calculator
{
    public int Add(int a, int b)
    {
        return a + b;
    }
}
```

##### Object Creation
```c#
var address = new Address { Street = "Dhaka", Post = 1200 };
var user = new User { Name = "Shahid", Address = address };
var calc = new Calculator();
```

```c#
  // find type by name and reference type. here Program class is reference type where User class is located.
  var type = TypeUtil.FindByName<Program>("User");
```

```c#
var address = new Address { Street = "Dhaka", Post = 1200 };
var user = new User { Name = "Shahid", Address = address };
var calc = new Calculator();
```

```c#
// get property value
var name = ObjectUtil.GetValue(user, "name"); "Shahid"
```

```c#
// get nested property value
var post = ObjectUtil.GetValue(user, "address.post"); // 1200
```

```c#
// set property value
ObjectUtil.SetValue(user, "Shahidul Islam", "name"); // user.Name = "Shahidul Islam"
```

```c#
// set nested property value
ObjectUtil.SetValue(user, "Tangail", "address.street"); // user.Address.Street = "Tangail"
```

```c#
// invoke method
var addr = ObjectUtil.InvokeMethod(user, "address.getaddress"); // Street: Tangail, Post: 1200
```

```c#
// invoke method with params
var sum = ObjectUtil.InvokeMethod(calc, "add", 1, 2); // 3
```
