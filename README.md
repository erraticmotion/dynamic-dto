# Dynamic DTO

Work with abstractions, don't expose types. But what happens when I want to send something down the wire. 

Use Reflection.Emit to dynamically build data transfer objects based on an interface not a type. 

```c#
using DynamicDto;
using Newtonsoft.Json;

public interface IDtoWithOnlyGet
{
    int OnlyGet { get; }
}

internal class DtoWithOnlyGet : IDtoWithOnlyGet
{
    public DtoWithOnlyGet(int value)
    {
        this.OnlyGet = value;
    }
    
    public int OnlyGet { get; private set; }
}

IDtoWithOnlyGet obj = new DtoWithOnlyGet(10);

var dto = Dynamic.Builder.Create<IDtoWithOnlyGet>(obj);

// dto is now serializable
var json = JsonConvert.SerializeObject(dto);
```

```json
{"OnlyGet":12}
```
