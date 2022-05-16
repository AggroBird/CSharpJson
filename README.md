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
TestStruct testStruct = JsonValue.Deserialize<TestStruct>(json);

// Decide what to do with k depending on its type
if (testStruct.k.isNumber)
{
	Console.WriteLine($"Number: {(float)testStruct.k}");
}
else if (testStruct.k.isString)
{
	Console.WriteLine($"String: {(string)testStruct.k}");
}
else if (testStruct.k.isBool)
{
	Console.WriteLine($"Boolean: {(bool)testStruct.k}");
}
```

The JsonValue class uses boxing to store its values and reflection to serialize struct fields.

The parser is tested using the parsing test files from [Parsing JSON is a Minefield](https://seriot.ch/projects/parsing_json.html). Currently it passes all the accept cases except duplicate keys, since C# dictionaries throw when adding duplicate keys. From the reject cases, some invalid number formats are accepted regardless due to C#'s double.TryParse being more forgiving than the JSON standard.