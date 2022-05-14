# Reflection Json Utility

Lightweight Json parser for Unity/C# projects that don't support newer .NET versions. The advantage of this Json parser over Unity's JsonUtility is that it supports nested Json types in structs that can be typecasted at runtime later.

```csharp
public struct TestStruct
{
    public int i;
    public float j;
    public JsonValue k;
}

public static void JsonTest()
{
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
}
```