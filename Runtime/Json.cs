using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Globalization;

namespace AggroBird.Json
{
    public enum JsonType
    {
        Number,
        String,
        Bool,
        Array,
        Object,
        Null,
    }

    public sealed class JsonArray : List<JsonValue>
    {
        public override string ToString()
        {
            return JsonValue.Serialize(this);
        }
    }

    public sealed class JsonObject : Dictionary<string, JsonValue>
    {
        public override string ToString()
        {
            return JsonValue.Serialize(this);
        }
    }

    public sealed class JsonValue
    {
        private JsonValue(object obj, JsonType type)
        {
            this.obj = obj;
            this.type = type;
        }

        public bool isNumber => type == JsonType.Number;
        public bool isString => type == JsonType.String;
        public bool isBool => type == JsonType.Bool;
        public bool isArray => type == JsonType.Array;
        public bool isObject => type == JsonType.Object;
        public bool isNull => type == JsonType.Null;
        public JsonType type { get; private set; }

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


        private object obj;

        private string internalObjectTypeName
        {
            get
            {
                return obj == null ? "null" : obj.GetType().ToString();
            }
        }

        public override string ToString()
        {
            if (obj is string str)
            {
                return str;
            }
            return Serialize(this);
        }


        private sealed class JsonParser
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

            public JsonParser(string str, StringBuilder stringBuffer)
            {
                this.str = str;
                this.stringBuffer = stringBuffer;
                len = str.Length;
            }

            private readonly string str;
            private readonly int len;
            private StringBuilder stringBuffer;
            private int pos = 0;
            private int lineNum = 1;

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
                                    case '\0':
                                    case '\f':
                                    case '\n':
                                    case '\r':
                                    case '\t':
                                    case '\v':
                                    case '\b':
                                        throw new FormatException("Unsupported control character in string");

                                    case '\\':
                                    {
                                        if (pos < len - 1)
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
                                                    if (len - pos >= 6 && uint.TryParse(str.Substring(pos + 2, 4), NumberStyles.AllowHexSpecifier, null, out uint charCode))
                                                    {
                                                        stringBuffer.Append((char)charCode);
                                                        pos += 5;
                                                        continue;
                                                    }
                                                }
                                                goto InvalidEscape;
                                                default: goto InvalidEscape;
                                            }
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
                                else if (sub == "true")
                                {
                                    val = new JsonValue(true, JsonType.Bool);
                                    return TokenType.Value;
                                }
                                else if (sub == "false")
                                {
                                    val = new JsonValue(false, JsonType.Bool);
                                    return TokenType.Value;
                                }
                                else if (sub == "null")
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

            private JsonValue ParseObject()
            {
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
                            obj.Add(key.stringValue, ParseObject());
                            break;
                        case TokenType.BracketOpen:
                            obj.Add(key.stringValue, ParseArray());
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
            private JsonValue ParseArray()
            {
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
                            arr.Add(ParseObject());
                            break;
                        case TokenType.BracketOpen:
                            arr.Add(ParseArray());
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

            public JsonValue Deserialize()
            {
                JsonValue result;
                switch (ParseNext(out JsonValue val))
                {
                    case TokenType.BraceOpen:
                        result = ParseObject();
                        break;
                    case TokenType.BracketOpen:
                        result = ParseArray();
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

        public static JsonValue Deserialize(string str, StringBuilder stringBuffer = null)
        {
            if (str == null) throw new ArgumentNullException(nameof(str));
            return new JsonParser(str, stringBuffer).Deserialize();
        }

        public static object Deserialize(string str, Type targetType)
        {
            if (str == null) throw new ArgumentNullException(nameof(str));
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));
            JsonValue jsonObject = Deserialize(str);
            return DeserializeRecursive(targetType, jsonObject);
        }
        public static object Deserialize(JsonValue jsonObject, Type targetType)
        {
            if (jsonObject == null) throw new ArgumentNullException(nameof(jsonObject));
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));
            return DeserializeRecursive(targetType, jsonObject);
        }
        public static T Deserialize<T>(string str) => (T)Deserialize(str, typeof(T));
        public static T Deserialize<T>(JsonValue jsonObject) => (T)Deserialize(jsonObject, typeof(T));

        private static object DeserializeRecursive(Type targetType, JsonValue jsonObject)
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
                    if (jsonObject.TryGetValue(out string stringValue))
                    {
                        if (typeCode == TypeCode.Char)
                        {
                            return stringValue.Length > 0 ? stringValue[0] : '\0';
                        }

                        return stringValue;
                    }
                    break;
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
                    obj.SetValue(DeserializeRecursive(elementType, subObjects[i]), i);
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
                    obj.Add(kv.Key, DeserializeRecursive(valueType, kv.Value));
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
                    obj.Add(DeserializeRecursive(valueType, subObjects[i]));
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
                        field.SetValue(obj, DeserializeRecursive(field.FieldType, kv.Value));
                    }
                }
                return obj;
            }

            throw new InvalidCastException();
        }

        public static string Serialize(object value, int maxRecursion = 32)
        {
            StringBuilder output = new StringBuilder(1024);
            SerializeRecursive(value, output, 0, maxRecursion);
            return output.ToString();
        }
        public static void Serialize(object value, StringBuilder output, int maxRecursion = 32)
        {
            if (output == null) throw new ArgumentNullException(nameof(output));
            SerializeRecursive(value, output, 0, maxRecursion);
        }

        private static void SerializeRecursive(object value, StringBuilder output, int level, int maxRecursion)
        {
            if (level >= maxRecursion)
            {
                throw new OverflowException($"Max recursion level reached ({maxRecursion})");
            }
            level++;

            if (value == null)
            {
                output.Append("null");
                return;
            }

            if (value is JsonValue asJsonValue)
            {
                value = asJsonValue.obj;
            }

            Type type = value.GetType();
            TypeCode typeCode = Type.GetTypeCode(type);
            switch (typeCode)
            {
                case TypeCode.String:
                    SerializeValue((string)value, output);
                    return;
                case TypeCode.Char:
                    SerializeValue((char)value, output);
                    return;
                case TypeCode.Boolean:
                    SerializeValue((bool)value, output);
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
                        SerializeEnumValue(value, typeCode, output);
                        return;
                    }
                    goto case TypeCode.Decimal;

                case TypeCode.Single:
                    SerializeValue((float)value, output);
                    return;
                case TypeCode.Double:
                    SerializeValue((double)value, output);
                    return;
                case TypeCode.Decimal:
                    SerializeValue(value as IConvertible, output);
                    return;
            }

            if (value is IDictionary dictionary)
            {
                output.Append('{');
                bool first = true;
                foreach (DictionaryEntry entry in dictionary)
                {
                    if (!first) output.Append(',');
                    first = false;
                    if (!(entry.Key is string key))
                    {
                        throw new InvalidCastException($"Dictionary key type has to be string");
                    }
                    SerializeValue(entry.Key as string, output);
                    output.Append(':');
                    SerializeRecursive(entry.Value, output, level, maxRecursion);
                }
                output.Append('}');
                return;
            }
            else if (value is IEnumerable list)
            {
                output.Append('[');
                bool first = true;
                foreach (object entry in list)
                {
                    if (!first) output.Append(',');
                    first = false;
                    SerializeRecursive(entry, output, level, maxRecursion);
                }
                output.Append(']');
                return;
            }
            else
            {
                output.Append('{');
                bool first = true;
                foreach (FieldInfo field in type.GetFields(BindingFlags.Instance | BindingFlags.Public))
                {
                    if (!first) output.Append(',');
                    first = false;
                    SerializeValue(field.Name, output);
                    output.Append(':');
                    SerializeRecursive(field.GetValue(value), output, level, maxRecursion);
                }
                if (IsAnonymousType(type))
                {
                    foreach (PropertyInfo property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
                    {
                        if (!property.CanRead) continue;
                        if (!first) output.Append(',');
                        first = false;
                        SerializeValue(property.Name, output);
                        output.Append(':');
                        SerializeRecursive(property.GetValue(value), output, level, maxRecursion);
                    }
                }
                output.Append('}');
                return;
            }

            throw new FormatException($"Failed to serialize field '{type}'");
        }
        private static bool IsAnonymousType(Type type)
        {
            return type.GetCustomAttributes(typeof(CompilerGeneratedAttribute), false).Length > 0 && type.FullName.Contains("AnonymousType");
        }

        private static bool SerializeValue(char value, StringBuilder output)
        {
            switch (value)
            {
                case '\t':
                    output.Append("\\t");
                    return true;
                case '\n':
                    output.Append("\\n");
                    return true;
                case '\r':
                    output.Append("\\r");
                    return true;
                case '\f':
                    output.Append("\\f");
                    return true;
                case '\b':
                    output.Append("\\b");
                    return true;
                //case '<':
                //    output.Append("\\u003c");
                //    continue;
                //case '>':
                //    output.Append("\\u003e");
                //    continue;
                case '"':
                    output.Append("\\\"");
                    return true;
                //case '\'':
                //    output.Append("\\u0027");
                //    continue;
                case '\\':
                    output.Append("\\\\");
                    return true;
            }
            if (value > '\u001f')
            {
                output.Append(value);
                return true;
            }
            return false;
        }
        private static void SerializeValue(string value, StringBuilder output)
        {
            output.Append('"');
            foreach (char c in value)
            {
                if (!SerializeValue(c, output))
                {
                    output.Append("\\u00");
                    int num = c;
                    output.Append((char)(48 + (num >> 4)));
                    num &= 0xF;
                    output.Append((char)((num < 10) ? (48 + num) : (97 + (num - 10))));
                }
            }
            output.Append('"');
        }
        private static void SerializeValue(bool value, StringBuilder output)
        {
            output.Append(value ? "true" : "false");
        }
        private static void SerializeValue(float value, StringBuilder output)
        {
            output.Append(value.ToString("r", CultureInfo.InvariantCulture));
        }
        private static void SerializeValue(double value, StringBuilder output)
        {
            output.Append(value.ToString("r", CultureInfo.InvariantCulture));
        }
        private static void SerializeValue(IConvertible value, StringBuilder output)
        {
            output.Append(value.ToString(CultureInfo.InvariantCulture));
        }

        private static void SerializeEnumValue(object value, TypeCode typeCode, StringBuilder output)
        {
            switch (typeCode)
            {
                case TypeCode.SByte:
                    output.Append((sbyte)value);
                    break;
                case TypeCode.Int16:
                    output.Append((short)value);
                    break;
                case TypeCode.UInt16:
                    output.Append((ushort)value);
                    break;
                case TypeCode.Int32:
                    output.Append((int)value);
                    break;
                case TypeCode.Byte:
                    output.Append((byte)value);
                    break;
                case TypeCode.UInt32:
                    output.Append((uint)value);
                    break;
                case TypeCode.Int64:
                    output.Append((long)value);
                    break;
                case TypeCode.UInt64:
                    output.Append((ulong)value);
                    break;
                default:
                    throw new InvalidOperationException($"Invalid type code for enum: '{typeCode}'");
            }
        }
    }
}