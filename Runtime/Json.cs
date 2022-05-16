using System.Text;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Globalization;

namespace AggroBird.Json
{
    // Supported json types
    public enum JsonType
    {
        Number,
        String,
        Bool,
        Array,
        Object,
        Null,
    }

    // Array of values
    public sealed class JsonArray : List<JsonValue>
    {
        public override string ToString()
        {
            return JsonValue.ToJson(this);
        }
    }

    // Object of key/values
    public sealed class JsonObject : Dictionary<string, JsonValue>
    {
        public override string ToString()
        {
            return JsonValue.ToJson(this);
        }
    }

    public sealed class JsonValue
    {
        private JsonValue(object obj, JsonType type)
        {
            this.obj = obj;
            this.type = type;
        }

        // Get value type
        public bool isNumber => type == JsonType.Number;
        public bool isString => type == JsonType.String;
        public bool isBool => type == JsonType.Bool;
        public bool isArray => type == JsonType.Array;
        public bool isObject => type == JsonType.Object;
        public bool isNull => type == JsonType.Null;
        public JsonType type { get; private set; }

        // Try-get value (won't throw on invalid cast)
        public bool TryGetValue(out int val)
        {
            try
            {
                if (obj is double d)
                {
                    val = Convert.ToInt32(d);
                    return true;
                }
            }
            catch (Exception)
            {

            }

            val = 0;
            return false;
        }
        public bool TryGetValue(out uint val)
        {
            try
            {
                if (obj is double d)
                {
                    val = Convert.ToUInt32(d);
                    return true;
                }
            }
            catch (Exception)
            {

            }

            val = 0;
            return false;
        }
        public bool TryGetValue(out long val)
        {
            try
            {
                if (obj is double d)
                {
                    val = Convert.ToInt64(d);
                    return true;
                }
            }
            catch (Exception)
            {

            }

            val = 0;
            return false;
        }
        public bool TryGetValue(out ulong val)
        {
            try
            {
                if (obj is double d)
                {
                    val = Convert.ToUInt64(d);
                    return true;
                }
            }
            catch (Exception)
            {

            }

            val = 0;
            return false;
        }
        public bool TryGetValue(out float val)
        {
            try
            {
                if (obj is double d)
                {
                    val = Convert.ToSingle(d);
                    return true;
                }
            }
            catch (Exception)
            {

            }

            val = 0;
            return false;
        }
        public bool TryGetValue(out double val)
        {
            if (obj is double d)
            {
                val = d;
                return true;
            }

            val = 0;
            return false;
        }
        public bool TryGetValue(out decimal val)
        {
            try
            {
                if (obj is double d)
                {
                    val = Convert.ToDecimal(d);
                    return true;
                }
            }
            catch (Exception)
            {

            }

            val = 0;
            return false;
        }
        public bool TryGetValue(out string val)
        {
            if (obj is string str)
            {
                val = str;
                return true;
            }
            val = null;
            return false;
        }
        public bool TryGetValue(out bool val)
        {
            if (obj is bool b)
            {
                val = b;
                return true;
            }
            val = false;
            return false;
        }
        public bool TryGetValue(out JsonArray val)
        {
            if (obj is JsonArray arr)
            {
                val = arr;
                return true;
            }
            val = null;
            return false;
        }
        public bool TryGetValue(out JsonObject val)
        {
            if (obj is JsonObject o)
            {
                val = o;
                return true;
            }
            val = null;
            return false;
        }

        // Explicit get/set (will throw on invalid cast)
        public int intValue
        {
            get
            {
                if (!TryGetValue(out int val))
                {
                    throw new InvalidCastException($"Invalid Json cast: '{internalObjectTypeName}' to int");
                }
                return val;
            }
            set
            {
                obj = (double)value;
                type = JsonType.Number;
            }
        }
        public uint uintValue
        {
            get
            {
                if (!TryGetValue(out uint val))
                {
                    throw new InvalidCastException($"Invalid Json cast: '{internalObjectTypeName}' to uint");
                }
                return val;
            }
            set
            {
                obj = (double)value;
                type = JsonType.Number;
            }
        }
        public long longValue
        {
            get
            {
                if (!TryGetValue(out long val))
                {
                    throw new InvalidCastException($"Invalid Json cast: '{internalObjectTypeName}' to long");
                }
                return val;
            }
            set
            {
                obj = (double)value;
                type = JsonType.Number;
            }
        }
        public ulong ulongValue
        {
            get
            {
                if (!TryGetValue(out ulong val))
                {
                    throw new InvalidCastException($"Invalid Json cast: '{internalObjectTypeName}' to ulong");
                }
                return val;
            }
            set
            {
                obj = (double)value;
                type = JsonType.Number;
            }
        }
        public float floatValue
        {
            get
            {
                if (!TryGetValue(out float val))
                {
                    throw new InvalidCastException($"Invalid Json cast: '{internalObjectTypeName}' to float");
                }
                return val;
            }
            set
            {
                obj = (double)value;
                type = JsonType.Number;
            }
        }
        public double doubleValue
        {
            get
            {
                if (!TryGetValue(out double val))
                {
                    throw new InvalidCastException($"Invalid Json cast: '{internalObjectTypeName}' to double");
                }
                return val;
            }
            set
            {
                obj = value;
                type = JsonType.Number;
            }
        }
        public decimal decimalValue
        {
            get
            {
                if (!TryGetValue(out decimal val))
                {
                    throw new InvalidCastException($"Invalid Json cast: '{internalObjectTypeName}' to decimal");
                }
                return val;
            }
            set
            {
                obj = (double)value;
                type = JsonType.Number;
            }
        }
        public string stringValue
        {
            get
            {
                if (!isString)
                {
                    throw new InvalidCastException($"Invalid Json cast: '{internalObjectTypeName}' to string");
                }
                return (string)obj;
            }
            set
            {
                obj = value;
                type = value == null ? JsonType.Null : JsonType.String;
            }
        }
        public bool boolValue
        {
            get
            {
                if (!isBool)
                {
                    throw new InvalidCastException($"Invalid Json cast: '{internalObjectTypeName}' to bool");
                }
                return (bool)obj;
            }
            set
            {
                obj = value;
                type = JsonType.Bool;
            }
        }
        public JsonArray arrayValue
        {
            get
            {
                if (!TryGetValue(out JsonArray val))
                {
                    throw new InvalidCastException($"Invalid Json cast: '{internalObjectTypeName}' to JsonArray");
                }
                return val;
            }
            set
            {
                obj = value;
                type = value == null ? JsonType.Null : JsonType.Array;
            }
        }
        public JsonObject objectValue
        {
            get
            {
                if (!TryGetValue(out JsonObject val))
                {
                    throw new InvalidCastException($"Invalid Json cast: '{internalObjectTypeName}' to JsonObject");
                }
                return val;
            }
            set
            {
                obj = value;
                type = value == null ? JsonType.Null : JsonType.Object;
            }
        }

        // Explicit cast operators (will throw on invalid cast)
        public static explicit operator int(JsonValue value) => value.intValue;
        public static explicit operator uint(JsonValue value) => value.uintValue;
        public static explicit operator long(JsonValue value) => value.longValue;
        public static explicit operator ulong(JsonValue value) => value.ulongValue;
        public static explicit operator float(JsonValue value) => value.floatValue;
        public static explicit operator double(JsonValue value) => value.doubleValue;
        public static explicit operator decimal(JsonValue value) => value.decimalValue;
        public static explicit operator string(JsonValue value) => value.stringValue;
        public static explicit operator bool(JsonValue value) => value.boolValue;
        public static explicit operator JsonArray(JsonValue value) => value.arrayValue;
        public static explicit operator JsonObject(JsonValue value) => value.objectValue;

        // Parsing constants
        private const string DoubleFormat = "G17";
        private const string DateTimeFormat = "o";
        private const string TrueConstant = "true";
        private const string FalseConstant = "false";
        private const string NullConstant = "null";


        private object obj;

        private string internalObjectTypeName
        {
            get
            {
                return obj == null ? NullConstant : obj.GetType().Name;
            }
        }

        public override int GetHashCode()
        {
            return obj == null ? 0 : obj.GetHashCode();
        }
        public override string ToString()
        {
            if (obj is string str)
            {
                return str;
            }
            return ToJson(this);
        }


        private sealed class JsonReader
        {
            private enum TokenType
            {
                Eof,
                BraceOpen,
                BraceClose,
                BracketOpen,
                BracketClose,
                Comma,
                Colon,
                String,
                Value,
                Unknown = -1,
            }

            public JsonReader(string str, StringBuilder stringBuffer, int maxRecursion)
            {
                this.str = str;
                this.stringBuffer = stringBuffer;
                len = str.Length;
                this.maxRecursion = maxRecursion;
            }

            private readonly string str;
            private readonly int len;
            private readonly int maxRecursion;
            private StringBuilder stringBuffer;
            private int pos = 0;
            private int lineNum = 1;
            private int level = 0;

            private TokenType ParseNext(out JsonValue val)
            {
                val = null;

                while (true)
                {
                    if (pos >= len)
                    {
                        return TokenType.Eof;
                    }

                    int beg = pos++;
                    char c = str[beg];
                    switch (c)
                    {
                        // Skip whitespaces
                        case ' ':
                        case '\t':
                        case '\r':
                        case '\v':
                            continue;

                        // Increment newline
                        case '\n':
                            lineNum++;
                            continue;

                        // Recognized tokens
                        case '{': return TokenType.BraceOpen;
                        case '}': return TokenType.BraceClose;
                        case '[': return TokenType.BracketOpen;
                        case ']': return TokenType.BracketClose;
                        case ',': return TokenType.Comma;
                        case ':': return TokenType.Colon;
                        case '"':
                        {
                            // Iterate string characters
                            stringBuffer = stringBuffer ?? new StringBuilder(256);
                            for (; pos < len; pos++)
                            {
                                c = str[pos];
                                switch (c)
                                {
                                    // Catch unsupported control characters
                                    case '\0':
                                    case '\f':
                                    case '\n':
                                    case '\r':
                                    case '\t':
                                    case '\v':
                                    case '\b':
                                        throw new FormatException($"Unsupported control character in string (line {lineNum})");

                                    case '\\':
                                    {
                                        // Character escapes
                                        int remaining = len - pos;
                                        if (remaining >= 2)
                                        {
                                            switch (str[pos + 1])
                                            {
                                                case '\\': stringBuffer.Append('\\'); break;
                                                case '/': stringBuffer.Append('/'); break;
                                                case '"': stringBuffer.Append('\"'); break;
                                                case 'b': stringBuffer.Append('\b'); break;
                                                case 'f': stringBuffer.Append('\f'); break;
                                                case 'n': stringBuffer.Append('\n'); break;
                                                case 'r': stringBuffer.Append('\r'); break;
                                                case 't': stringBuffer.Append('\t'); break;
                                                case 'u':
                                                {
                                                    // Parse hex char code
                                                    if (remaining >= 6 && uint.TryParse(str.Substring(pos + 2, 4), NumberStyles.AllowHexSpecifier, null, out uint charCode))
                                                    {
                                                        stringBuffer.Append((char)charCode);

                                                        // Skip escaped character + char code
                                                        pos += 5;
                                                        continue;
                                                    }
                                                }
                                                goto InvalidEscape;
                                                default: goto InvalidEscape;
                                            }

                                            // Skip escaped character
                                            pos++;
                                            continue;
                                        }
                                    InvalidEscape:
                                        throw new FormatException($"Invalid character escape sequence (line {lineNum})");
                                    }
                                }
                                if (c == '"')
                                {
                                    // End of string
                                    string str = stringBuffer.Length > 0 ? stringBuffer.ToString() : string.Empty;
                                    val = new JsonValue(str, JsonType.String);
                                    stringBuffer.Clear();
                                    pos++;
                                    return TokenType.String;
                                }
                                stringBuffer.Append(c);
                            }
                            throw new FormatException($"Unterminated string (line {lineNum})");
                        }
                        default:
                        {
                            // Continue until token or whitespace
                            for (; pos < len; pos++)
                            {
                                c = str[pos];
                                switch (c)
                                {
                                    case ' ':
                                    case '\t':
                                    case '\v':
                                    case '\r':
                                    case '\n':
                                    case '{':
                                    case '}':
                                    case '[':
                                    case ']':
                                    case ',':
                                    case ':':
                                    case '"':
                                        goto ParseValue;
                                }
                            }

                        ParseValue:
                            if (pos - beg > 0)
                            {
                                string sub = str.Substring(beg, pos - beg);
                                if (char.IsDigit(sub[0]) || sub[0] == '-')
                                {
                                    if (double.TryParse(sub, NumberStyles.Float, CultureInfo.InvariantCulture, out double d))
                                    {
                                        val = new JsonValue(d, JsonType.Number);
                                        return TokenType.Value;
                                    }
                                }
                                else if (sub == TrueConstant)
                                {
                                    val = new JsonValue(true, JsonType.Bool);
                                    return TokenType.Value;
                                }
                                else if (sub == FalseConstant)
                                {
                                    val = new JsonValue(false, JsonType.Bool);
                                    return TokenType.Value;
                                }
                                else if (sub == NullConstant)
                                {
                                    val = new JsonValue(null, JsonType.Null);
                                    return TokenType.Value;
                                }
                                throw new FormatException($"Unknown expression '{sub}' (line {lineNum})");
                            }
                            throw new FormatException($"Unexpected character '{c}' (line {lineNum})");
                        }
                    }
                }
            }

            private TokenType PeekNext()
            {
                // Continue iterating until a token is encountered
                while (true)
                {
                    if (pos >= len)
                    {
                        return TokenType.Eof;
                    }

                    switch (str[pos])
                    {
                        case ' ':
                        case '\t':
                        case '\v':
                        case '\r':
                            pos++;
                            continue;

                        case '\n':
                            pos++;
                            lineNum++;
                            continue;

                        case '{': return TokenType.BraceOpen;
                        case '}': return TokenType.BraceClose;
                        case '[': return TokenType.BracketOpen;
                        case ']': return TokenType.BracketClose;
                        case ',': return TokenType.Comma;
                        case ':': return TokenType.Colon;

                        default: return TokenType.Unknown;
                    }
                }
            }

            private JsonValue ParseObjectRecursive()
            {
                if (level >= maxRecursion)
                {
                    throw new OverflowException($"Max recursion level reached ({maxRecursion})");
                }
                level++;

                JsonObject obj = new JsonObject();
                if (PeekNext() != TokenType.BraceClose)
                {
                Next:
                    if (ParseNext(out JsonValue key) != TokenType.String)
                    {
                        throw new FormatException($"Expected key string (line {lineNum})");
                    }
                    if (PeekNext() != TokenType.Colon)
                    {
                        throw new FormatException($"Missing colon after key string (line {lineNum})");
                    }
                    pos++;

                    switch (ParseNext(out JsonValue val))
                    {
                        case TokenType.String:
                        case TokenType.Value:
                            obj.Add(key.stringValue, val);
                            break;
                        case TokenType.BraceOpen:
                            obj.Add(key.stringValue, ParseObjectRecursive());
                            break;
                        case TokenType.BracketOpen:
                            obj.Add(key.stringValue, ParseArrayRecursive());
                            break;

                        default:
                            throw new FormatException($"Expected value (line {lineNum})");
                    }
                    switch (PeekNext())
                    {
                        case TokenType.BraceClose: goto Exit;
                        case TokenType.Comma: pos++; goto Next;
                        default: throw new FormatException($"Expected comma or closing brace after value (line {lineNum})");
                    }
                }

            Exit:
                pos++;
                return new JsonValue(obj, JsonType.Object);
            }
            private JsonValue ParseArrayRecursive()
            {
                if (level >= maxRecursion)
                {
                    throw new OverflowException($"Max recursion level reached ({maxRecursion})");
                }
                level++;

                JsonArray arr = new JsonArray();
                if (PeekNext() != TokenType.BracketClose)
                {
                Next:
                    switch (ParseNext(out JsonValue val))
                    {
                        case TokenType.String:
                        case TokenType.Value:
                            arr.Add(val);
                            break;
                        case TokenType.BraceOpen:
                            arr.Add(ParseObjectRecursive());
                            break;
                        case TokenType.BracketOpen:
                            arr.Add(ParseArrayRecursive());
                            break;

                        default:
                            throw new FormatException($"Expected value (line {lineNum})");
                    }
                    switch (PeekNext())
                    {
                        case TokenType.BracketClose: goto Exit;
                        case TokenType.Comma: pos++; goto Next;
                        default: throw new FormatException($"Expected comma or closing bracket after value (line {lineNum})");
                    }
                }

            Exit:
                pos++;
                return new JsonValue(arr, JsonType.Array);
            }

            public JsonValue Read()
            {
                JsonValue result;
                switch (ParseNext(out JsonValue val))
                {
                    case TokenType.BraceOpen:
                        result = ParseObjectRecursive();
                        break;
                    case TokenType.BracketOpen:
                        result = ParseArrayRecursive();
                        break;
                    case TokenType.Value:
                    case TokenType.String:
                        result = val;
                        break;
                    default:
                        throw new FormatException("Invalid Json string");
                }

                // Ensure eof
                if (PeekNext() != TokenType.Eof)
                {
                    throw new FormatException($"Unexpected expression at the end of file (line {lineNum})");
                }

                return result;
            }
        }

        public static JsonValue FromJson(string str, StringBuilder stringBuffer = null, int maxRecursion = 128)
        {
            if (str == null) throw new ArgumentNullException(nameof(str));
            return new JsonReader(str, stringBuffer, maxRecursion).Read();
        }

        public static object FromJson(string str, Type targetType, StringBuilder stringBuffer = null, int maxRecursion = 128)
        {
            if (str == null) throw new ArgumentNullException(nameof(str));
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));
            JsonValue jsonObject = FromJson(str, stringBuffer, maxRecursion);
            return ReadRecursive(targetType, jsonObject);
        }
        public static object FromJson(JsonValue jsonObject, Type targetType)
        {
            if (jsonObject == null) throw new ArgumentNullException(nameof(jsonObject));
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));
            return ReadRecursive(targetType, jsonObject);
        }
        public static T FromJson<T>(string str, StringBuilder stringBuffer = null, int maxRecursion = 128) => (T)FromJson(str, typeof(T), stringBuffer, maxRecursion);
        public static T FromJson<T>(JsonValue jsonObject) => (T)FromJson(jsonObject, typeof(T));

        private static object ReadRecursive(Type targetType, JsonValue jsonObject)
        {
            if (jsonObject.isNull)
            {
                return null;
            }

            TypeCode typeCode = Type.GetTypeCode(targetType);
            switch (typeCode)
            {
                case TypeCode.String:
                case TypeCode.Char:
                {
                    string str = jsonObject.stringValue;
                    if (typeCode == TypeCode.Char)
                    {
                        if (str.Length == 1)
                        {
                            return str[0];
                        }
                        throw new InvalidCastException("Attempted to convert a string to a singular character");
                    }
                    return str;
                }

                case TypeCode.Boolean:
                    return jsonObject.boolValue;

                case TypeCode.SByte:
                    return (sbyte)jsonObject.intValue;
                case TypeCode.Byte:
                    return (byte)jsonObject.intValue;
                case TypeCode.Int16:
                    return (short)jsonObject.intValue;
                case TypeCode.UInt16:
                    return (ushort)jsonObject.intValue;
                case TypeCode.Int32:
                    return jsonObject.intValue;
                case TypeCode.UInt32:
                    return jsonObject.uintValue;
                case TypeCode.Int64:
                    return jsonObject.longValue;
                case TypeCode.UInt64:
                    return jsonObject.ulongValue;

                case TypeCode.Single:
                    return jsonObject.floatValue;
                case TypeCode.Double:
                    return jsonObject.doubleValue;
                case TypeCode.Decimal:
                    return jsonObject.decimalValue;

                case TypeCode.DateTime:
                {
                    string str = jsonObject.stringValue;
                    return DateTime.Parse(str, CultureInfo.InvariantCulture);
                }
                break;
            }

            if (targetType == typeof(JsonValue))
            {
                return jsonObject;
            }
            else if (targetType == typeof(JsonArray))
            {
                return jsonObject.arrayValue;
            }
            else if (targetType == typeof(JsonObject))
            {
                return jsonObject.objectValue;
            }
            else if (targetType.IsArray)
            {
                JsonArray subObjects = jsonObject.arrayValue;
                Type elementType = targetType.GetElementType();
                Array obj = Array.CreateInstance(elementType, subObjects.Count);
                for (int i = 0; i < subObjects.Count; i++)
                {
                    obj.SetValue(ReadRecursive(elementType, subObjects[i]), i);
                }
                return obj;
            }
            else if (typeof(IDictionary).IsAssignableFrom(targetType))
            {
                Type[] arguments = targetType.GetGenericArguments();
                if (arguments.Length < 2)
                {
                    throw new InvalidCastException($"Unable to derive generic types for dictionary '{targetType}'");
                }
                if (arguments[0] != typeof(string))
                {
                    throw new InvalidCastException($"Dictionary key type has to be string");
                }
                JsonObject dictionary = jsonObject.obj as JsonObject;
                if (dictionary == null)
                {
                    throw new InvalidCastException($"Invalid Json cast: '{jsonObject.obj.GetType()}' to '{typeof(JsonObject)}'");
                }
                IDictionary obj = Activator.CreateInstance(targetType) as IDictionary;
                Type valueType = arguments[1];
                foreach (var kv in dictionary)
                {
                    obj.Add(kv.Key, ReadRecursive(valueType, kv.Value));
                }
                return obj;
            }
            else if (typeof(IList).IsAssignableFrom(targetType))
            {
                Type[] arguments = targetType.GetGenericArguments();
                if (arguments.Length == 0)
                {
                    throw new InvalidCastException($"Unable to derive generic types for collection '{targetType}'");
                }
                IList obj = Activator.CreateInstance(targetType) as IList;
                JsonArray subObjects = jsonObject.arrayValue;
                Type valueType = arguments[0];
                for (int i = 0; i < subObjects.Count; i++)
                {
                    obj.Add(ReadRecursive(valueType, subObjects[i]));
                }
                return obj;
            }
            else
            {
                JsonObject dictionary = jsonObject.obj as JsonObject;
                if (dictionary == null)
                {
                    throw new InvalidCastException($"Invalid Json cast: '{jsonObject.obj.GetType()}' to '{typeof(JsonObject)}'");
                }
                object obj = Activator.CreateInstance(targetType);
                foreach (var kv in dictionary)
                {
                    FieldInfo field = targetType.GetField(kv.Key, BindingFlags.Instance | BindingFlags.Public);
                    if (field != null)
                    {
                        field.SetValue(obj, ReadRecursive(field.FieldType, kv.Value));
                    }
                }
                return obj;
            }

            throw new InvalidCastException();
        }


        private sealed class JsonWriter
        {
            public JsonWriter(StringBuilder outputBuffer, int maxRecursion)
            {
                this.maxRecursion = maxRecursion;
                this.outputBuffer = outputBuffer;
            }

            private const string AnonymousTypeName = "AnonymousType";
            private const string UnicodePrefix = "\\u00";

            private readonly int maxRecursion;
            private StringBuilder outputBuffer;
            private int level = 0;

            public void Write(object value)
            {
                outputBuffer.Clear();
                WriteRecursive(value);
            }

            private void WriteRecursive(object value)
            {
                if (level >= maxRecursion)
                {
                    throw new OverflowException($"Max recursion level reached ({maxRecursion})");
                }
                level++;

                if (value is JsonValue asJsonValue)
                {
                    value = asJsonValue.obj;
                }

                // Null
                if (value == null)
                {
                    outputBuffer.Append(NullConstant);
                    return;
                }

                // Base types
                Type type = value.GetType();
                TypeCode typeCode = Type.GetTypeCode(type);
                switch (typeCode)
                {
                    case TypeCode.String:
                        WriteValue((string)value);
                        return;
                    case TypeCode.Char:
                        WriteValue((char)value);
                        return;
                    case TypeCode.Boolean:
                        WriteValue((bool)value);
                        return;

                    case TypeCode.SByte:
                    case TypeCode.Byte:
                    case TypeCode.Int16:
                    case TypeCode.UInt16:
                    case TypeCode.Int32:
                    case TypeCode.UInt32:
                    case TypeCode.Int64:
                    case TypeCode.UInt64:
                        if (type.IsEnum)
                        {
                            WriteEnumValue(value, typeCode);
                            return;
                        }
                        outputBuffer.Append(value.ToString());
                        return;

                    case TypeCode.Single:
                        WriteValue((float)value);
                        return;
                    case TypeCode.Double:
                        WriteValue((double)value);
                        return;
                    case TypeCode.DateTime:
                        WriteValue(((DateTime)value).ToString(DateTimeFormat, CultureInfo.InvariantCulture));
                        return;
                }

                bool written = false;
                if (value is IDictionary dictionary)
                {
                    // Dictionaries/objects
                    outputBuffer.Append('{');
                    foreach (DictionaryEntry entry in dictionary)
                    {
                        if (written) outputBuffer.Append(',');
                        written = true;
                        if (!(entry.Key is string key))
                        {
                            throw new InvalidCastException($"Dictionary key type has to be string");
                        }
                        WriteValue(entry.Key as string);
                        outputBuffer.Append(':');
                        WriteRecursive(entry.Value);
                    }
                    outputBuffer.Append('}');
                    return;
                }
                else if (value is IEnumerable list)
                {
                    // Arrays
                    outputBuffer.Append('[');
                    foreach (object entry in list)
                    {
                        if (written) outputBuffer.Append(',');
                        written = true;
                        WriteRecursive(entry);
                    }
                    outputBuffer.Append(']');
                    return;
                }
                else
                {
                    // Structs/classes
                    outputBuffer.Append('{');
                    foreach (FieldInfo field in type.GetFields(BindingFlags.Instance | BindingFlags.Public))
                    {
                        if (written) outputBuffer.Append(',');
                        written = true;
                        WriteValue(field.Name);
                        outputBuffer.Append(':');
                        WriteRecursive(field.GetValue(value));
                    }
                    if (IsAnonymousType(type))
                    {
                        // Read properties (anonymous types only)
                        foreach (PropertyInfo property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
                        {
                            if (!property.CanRead) continue;
                            if (written) outputBuffer.Append(',');
                            written = true;
                            WriteValue(property.Name);
                            outputBuffer.Append(':');
                            WriteRecursive(property.GetValue(value));
                        }
                    }
                    outputBuffer.Append('}');
                    return;
                }

                throw new FormatException($"Failed to serialize field type '{type}'");
            }

            private static bool IsAnonymousType(Type type)
            {
                return type.GetCustomAttributes(typeof(CompilerGeneratedAttribute), false).Length > 0 && type.FullName.Contains(AnonymousTypeName);
            }

            private bool WriteValue(char value)
            {
                // Escape unsupported control characters (as per JSON standard)
                switch (value)
                {
                    case '\t':
                        outputBuffer.Append('\\');
                        outputBuffer.Append('t');
                        return true;
                    case '\n':
                        outputBuffer.Append('\\');
                        outputBuffer.Append('n');
                        return true;
                    case '\r':
                        outputBuffer.Append('\\');
                        outputBuffer.Append('r');
                        return true;
                    case '\f':
                        outputBuffer.Append('\\');
                        outputBuffer.Append('f');
                        return true;
                    case '\b':
                        outputBuffer.Append('\\');
                        outputBuffer.Append('b');
                        return true;
                    case '"':
                        outputBuffer.Append('\\');
                        outputBuffer.Append('\"');
                        return true;
                    case '\\':
                        outputBuffer.Append('\\');
                        outputBuffer.Append('\\');
                        return true;
                }

                // Anything above 1f (space and onwards) can be represented as character,
                // the output string will be converted to utf8 at save time
                if (value >= '\u001f')
                {
                    outputBuffer.Append(value);
                    return true;
                }

                return false;
            }
            private void WriteValue(string value)
            {
                outputBuffer.Append('"');
                foreach (char c in value)
                {
                    if (!WriteValue(c))
                    {
                        // Handle unsupported characters
                        outputBuffer.Append(UnicodePrefix);
                        int num = c;
                        outputBuffer.Append((char)(48 + (num >> 4)));
                        num &= 0xF;
                        outputBuffer.Append((char)((num < 10) ? (48 + num) : (97 + (num - 10))));
                    }
                }
                outputBuffer.Append('"');
            }
            private void WriteValue(bool value)
            {
                outputBuffer.Append(value ? TrueConstant : FalseConstant);
            }
            private void WriteValue(float value)
            {
                outputBuffer.Append(value.ToString(DoubleFormat, CultureInfo.InvariantCulture));
            }
            private void WriteValue(double value)
            {
                outputBuffer.Append(value.ToString(DoubleFormat, CultureInfo.InvariantCulture));
            }

            private void WriteEnumValue(object value, TypeCode typeCode)
            {
                switch (typeCode)
                {
                    case TypeCode.SByte:
                        outputBuffer.Append((sbyte)value);
                        break;
                    case TypeCode.Int16:
                        outputBuffer.Append((short)value);
                        break;
                    case TypeCode.UInt16:
                        outputBuffer.Append((ushort)value);
                        break;
                    case TypeCode.Int32:
                        outputBuffer.Append((int)value);
                        break;
                    case TypeCode.Byte:
                        outputBuffer.Append((byte)value);
                        break;
                    case TypeCode.UInt32:
                        outputBuffer.Append((uint)value);
                        break;
                    case TypeCode.Int64:
                        outputBuffer.Append((long)value);
                        break;
                    case TypeCode.UInt64:
                        outputBuffer.Append((ulong)value);
                        break;
                    default:
                        throw new InvalidOperationException($"Invalid type code for enum: '{typeCode}'");
                }
            }
        }

        public static string ToJson(object value, int maxRecursion = 32)
        {
            StringBuilder outputBuffer = new StringBuilder(1024);
            new JsonWriter(outputBuffer, maxRecursion).Write(value);
            return outputBuffer.ToString();
        }
        public static void ToJson(object value, StringBuilder outputBuffer, int maxRecursion = 32)
        {
            if (outputBuffer == null) throw new ArgumentNullException(nameof(outputBuffer));
            new JsonWriter(outputBuffer, maxRecursion).Write(value);
        }
    }
}