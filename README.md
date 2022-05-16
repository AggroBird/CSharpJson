# Reflection Json Utility

Lightweight Json parser for Unity/C# projects that don't support newer .NET versions. The difference between this Json parser and Unity's JsonUtility is that it supports nested Json types in structs that can be typecasted at runtime later.

Example:
```csharp
public struct TestStruct
{
    public int i;
    public float j;
    public JsonValue k;
}

string json = "{ \"i\": 15, \"j\": 27.5, \"k\": \"foo\" }";
TestStruct testStruct = JsonValue.FromJson<TestStruct>(json);

// Decide what to do with k depending on its type
switch (testStruct.k.type)
{
    case JsonType.Number:
        Console.WriteLine($"Number: {(float)testStruct.k}");
        break;
    case JsonType.String:
        Console.WriteLine($"String: {(string)testStruct.k}");
        break;
    case JsonType.Bool:
        Console.WriteLine($"Boolean: {(bool)testStruct.k}");
        break;
}
```

The JsonValue class uses boxing to store its values and reflection to serialize struct fields.

The parser is tested against the parsing test files from [Parsing JSON is a Minefield](https://seriot.ch/projects/parsing_json.html). Currently it passes all accept cases except duplicate keys, since C# dictionaries throw when adding duplicate keys. From the reject cases, some invalid number formats are accepted due to C#'s double.TryParse being more forgiving than the JSON standard.