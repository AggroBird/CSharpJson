using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Globalization;

namespace AggroBird.Json
{
    // Supported json types
    public enum JsonType
    {
        Null,
        Number,
        String,
        Bool,
        Array,
        Object,
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

    public struct JsonValue
    {
        // Constructors
        private JsonValue(int val)
        {
            obj = val;
            type = JsonType.Number;
        }
        private JsonValue(uint val)
        {
            obj = val;
            type = JsonType.Number;
        }
        private JsonValue(long val)
        {
            obj = val;
            type = JsonType.Number;
        }
        private JsonValue(ulong val)
        {
            obj = val;
            type = JsonType.Number;
        }
        private JsonValue(float val)
        {
            obj = val;
            type = JsonType.Number;
        }
        private JsonValue(double val)
        {
            obj = val;
            type = JsonType.Number;
        }
        private JsonValue(string val)
        {
            obj = val;
            type = val == null ? JsonType.Null : JsonType.String;
        }
        private JsonValue(bool val)
        {
            obj = val;
            type = JsonType.Bool;
        }
        private JsonValue(JsonArray val)
        {
            obj = val;
            type = val == null ? JsonType.Null : JsonType.Array;
        }
        private JsonValue(JsonObject val)
        {
            obj = val;
            type = val == null ? JsonType.Null : JsonType.Object;
        }

        // Get value type
        public bool isNull => type == JsonType.Null;
        public bool isNumber => type == JsonType.Number;
        public bool isString => type == JsonType.String;
        public bool isBool => type == JsonType.Bool;
        public bool isArray => type == JsonType.Array;
        public bool isObject => type == JsonType.Object;
        public JsonType type { get; private set; }

        // Try-get value (won't throw on invalid cast)
        public bool TryGetValue(out int val)
        {
            try
            {
                val = (int)this;
                return true;
            }
            catch (Exception)
            {

            }

            val = default;
            return false;
        }
        public bool TryGetValue(out uint val)
        {
            try
            {
                val = (uint)this;
                return true;
            }
            catch (Exception)
            {

            }

            val = default;
            return false;
        }
        public bool TryGetValue(out long val)
        {
            try
            {
                val = (long)this;
                return true;
            }
            catch (Exception)
            {

            }

            val = default;
            return false;
        }
        public bool TryGetValue(out ulong val)
        {
            try
            {
                val = (ulong)this;
                return true;
            }
            catch (Exception)
            {

            }

            val = default;
            return false;
        }
        public bool TryGetValue(out float val)
        {
            try
            {
                val = (float)this;
                return true;
            }
            catch (Exception)
            {

            }

            val = default;
            return false;
        }
        public bool TryGetValue(out double val)
        {
            if (obj is double d)
            {
                val = d;
                return true;
            }

            val = default;
            return false;
        }
        public bool TryGetValue(out string val)
        {
            if (obj is string str)
            {
                val = str;
                return true;
            }

            val = default;
            return false;
        }
        public bool TryGetValue(out bool val)
        {
            if (obj is bool b)
            {
                val = b;
                return true;
            }

            val = default;
            return false;
        }
        public bool TryGetValue(out JsonArray val)
        {
            if (obj is JsonArray jsonArray)
            {
                val = jsonArray;
                return true;
            }

            val = default;
            return false;
        }
        public bool TryGetValue(out JsonObject val)
        {
            if (obj is JsonObject jsonObject)
            {
                val = jsonObject;
                return true;
            }

            val = default;
            return false;
        }

        // Explicit cast operators (will throw on invalid cast)
        public static explicit operator int(JsonValue value)
        {
            if (!(value.obj is double val))
            {
                throw new InvalidCastException($"Invalid Json cast: '{value.internalObjectTypeName}' to '{typeof(int)}'");
            }
            return Convert.ToInt32(val);
        }
        public static explicit operator uint(JsonValue value)
        {
            if (!(value.obj is double val))
            {
                throw new InvalidCastException($"Invalid Json cast: '{value.internalObjectTypeName}' to '{typeof(uint)}'");
            }
            return Convert.ToUInt32(val);
        }
        public static explicit operator long(JsonValue value)
        {
            if (!(value.obj is double val))
            {
                throw new InvalidCastException($"Invalid Json cast: '{value.internalObjectTypeName}' to '{typeof(long)}'");
            }
            return Convert.ToInt64(val);
        }
        public static explicit operator ulong(JsonValue value)
        {
            if (!(value.obj is double val))
            {
                throw new InvalidCastException($"Invalid Json cast: '{value.internalObjectTypeName}' to '{typeof(ulong)}'");
            }
            return Convert.ToUInt64(val);
        }
        public static explicit operator float(JsonValue value)
        {
            if (!(value.obj is double val))
            {
                throw new InvalidCastException($"Invalid Json cast: '{value.internalObjectTypeName}' to '{typeof(float)}'");
            }
            return Convert.ToSingle(val);
        }
        public static explicit operator double(JsonValue value)
        {
            if (!(value.obj is double val))
            {
                throw new InvalidCastException($"Invalid Json cast: '{value.internalObjectTypeName}' to '{typeof(double)}'");
            }
            return val;
        }
        public static explicit operator string(JsonValue value)
        {
            if (!(value.obj is string val))
            {
                throw new InvalidCastException($"Invalid Json cast: '{value.internalObjectTypeName}' to '{typeof(string)}'");
            }
            return val;
        }
        public static explicit operator bool(JsonValue value)
        {
            if (!(value.obj is bool val))
            {
                throw new InvalidCastException($"Invalid Json cast: '{value.internalObjectTypeName}' to '{typeof(bool)}'");
            }
            return val;
        }
        public static explicit operator JsonArray(JsonValue value)
        {
            if (!(value.obj is JsonArray val))
            {
                throw new InvalidCastException($"Invalid Json cast: '{value.internalObjectTypeName}' to '{typeof(JsonArray)}'");
            }
            return val;
        }
        public static explicit operator JsonObject(JsonValue value)
        {
            if (!(value.obj is JsonObject val))
            {
                throw new InvalidCastException($"Invalid Json cast: '{value.internalObjectTypeName}' to '{typeof(JsonObject)}'");
            }
            return val;
        }

        // Implicit assignment operators
        public static implicit operator JsonValue(int value) => new JsonValue(value);
        public static implicit operator JsonValue(uint value) => new JsonValue(value);
        public static implicit operator JsonValue(long value) => new JsonValue(value);
        public static implicit operator JsonValue(ulong value) => new JsonValue(value);
        public static implicit operator JsonValue(float value) => new JsonValue(value);
        public static implicit operator JsonValue(double value) => new JsonValue(value);
        public static implicit operator JsonValue(string value) => new JsonValue(value);
        public static implicit operator JsonValue(bool value) => new JsonValue(value);
        public static implicit operator JsonValue(JsonArray value) => new JsonValue(value);
        public static implicit operator JsonValue(JsonObject value) => new JsonValue(value);

        // Parsing constants
        internal const string DoubleFormat = "G17";
        internal const string DateTimeFormat = "o";
        internal const string TrueConstant = "true";
        internal const string FalseConstant = "false";
        internal const string NullConstant = "null";
        internal const int ReadMaxRecursion = 128;
        internal const int WriteMaxRecursion = 32;
        internal const int StringBufferCapacity = 256;
        internal const int OutputBufferCapacity = 1024;


        private object obj;

        internal string internalObjectTypeName => obj == null ? NullConstant : obj.GetType().Name;

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
            else
            {
                return ToJson(this);
            }
        }
        public object ToObject()
        {
            return obj;
        }


        public static JsonValue FromJson(string str, StringBuilder stringBuffer = null)
        {
            return new JsonReader { stringBuffer = stringBuffer }.FromJson(str);
        }

        public static object FromJson(string str, Type targetType, StringBuilder stringBuffer = null)
        {
            return FromJson(FromJson(str, stringBuffer), targetType);
        }
        public static T FromJson<T>(string str, StringBuilder stringBuffer = null)
        {
            return (T)FromJson(FromJson(str, stringBuffer), typeof(T));
        }

        public static object FromJson(JsonValue jsonValue, Type targetType)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));

            if (jsonValue.isNull)
            {
                return null;
            }

            TypeCode typeCode = Type.GetTypeCode(targetType);
            switch (typeCode)
            {
                case TypeCode.String:
                case TypeCode.Char:
                {
                    string str = (string)jsonValue;
                    if (typeCode == TypeCode.Char)
                    {
                        if (str.Length == 1)
                        {
                            return str[0];
                        }
                        throw new InvalidCastException("Attempted to convert a multi-character string to a singular character");
                    }
                    return str;
                }

                case TypeCode.Boolean:
                    return (bool)jsonValue;

                case TypeCode.SByte:
                    return (sbyte)(int)jsonValue;
                case TypeCode.Byte:
                    return (byte)(int)jsonValue;
                case TypeCode.Int16:
                    return (short)(int)jsonValue;
                case TypeCode.UInt16:
                    return (ushort)(int)jsonValue;
                case TypeCode.Int32:
                    return (int)jsonValue;
                case TypeCode.UInt32:
                    return (uint)jsonValue;
                case TypeCode.Int64:
                    return (long)jsonValue;
                case TypeCode.UInt64:
                    return (ulong)jsonValue;

                case TypeCode.Single:
                    return (float)jsonValue;
                case TypeCode.Double:
                    return (double)jsonValue;

                case TypeCode.DateTime:
                    return DateTime.Parse((string)jsonValue, CultureInfo.InvariantCulture);
            }

            if (targetType == typeof(JsonValue))
            {
                return jsonValue;
            }
            else if (targetType == typeof(JsonArray))
            {
                return (JsonArray)jsonValue;
            }
            else if (targetType == typeof(JsonObject))
            {
                return (JsonObject)jsonValue;
            }
            else if (targetType.IsArray)
            {
                JsonArray jsonArray = (JsonArray)jsonValue;
                Type elementType = targetType.GetElementType();
                Array array = Array.CreateInstance(elementType, jsonArray.Count);
                for (int i = 0; i < jsonArray.Count; i++)
                {
                    array.SetValue(FromJson(jsonArray[i], elementType), i);
                }
                return array;
            }
            else if (typeof(IList).IsAssignableFrom(targetType))
            {
                JsonArray jsonArray = (JsonArray)jsonValue;
                Type[] arguments = targetType.GetGenericArguments();
                if (arguments.Length == 0)
                {
                    throw new InvalidCastException($"Unable to derive generic types for collection '{targetType}'");
                }
                IList list = Activator.CreateInstance(targetType) as IList;
                Type valueType = arguments[0];
                for (int i = 0; i < jsonArray.Count; i++)
                {
                    list.Add(FromJson(jsonArray[i], valueType));
                }
                return list;
            }
            else if (typeof(IDictionary).IsAssignableFrom(targetType))
            {
                JsonObject jsonObject = (JsonObject)jsonValue;
                Type[] arguments = targetType.GetGenericArguments();
                if (arguments.Length < 2)
                {
                    throw new InvalidCastException($"Unable to derive generic types for dictionary '{targetType}'");
                }
                if (arguments[0] != typeof(string))
                {
                    throw new InvalidCastException($"Dictionary key type has to be string");
                }
                IDictionary dictionary = Activator.CreateInstance(targetType) as IDictionary;
                Type valueType = arguments[1];
                foreach (var kv in jsonObject)
                {
                    dictionary.Add(kv.Key, FromJson(kv.Value, valueType));
                }
                return dictionary;
            }
            else
            {
                JsonObject jsonObject = (JsonObject)jsonValue;
                object obj = Activator.CreateInstance(targetType);
                foreach (var kv in jsonObject)
                {
                    FieldInfo field = targetType.GetField(kv.Key, BindingFlags.Instance | BindingFlags.Public);
                    if (field == null)
                    {
                        throw new MissingFieldException($"Failed to find field '{kv.Key}' in type '{targetType}'");
                    }
                    field.SetValue(obj, FromJson(kv.Value, field.FieldType));
                }
                return obj;
            }

            throw new InvalidCastException($"Invalid Json cast: '{jsonValue.internalObjectTypeName}' to '{targetType}'");
        }
        public static T FromJson<T>(JsonValue jsonObject)
        {
            return (T)FromJson(jsonObject, typeof(T));
        }

        public static string ToJson(object value, StringBuilder stringBuffer = null)
        {
            return new JsonWriter { stringBuffer = stringBuffer }.ToJson(value);
        }
    }

    public sealed class JsonReader
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

        private enum CommentState
        {
            None,
            SingleLine,
            MultiLine,
        }

        // Max read recursion (how deep do we allow json files to go)
        public int maxRecursion = JsonValue.ReadMaxRecursion;
        // String buffer for building strings (optional if the input does not contain strings)
        public StringBuilder stringBuffer = null;
        // Allow trailing and inline comments (not part of the JSON specifications)
        public bool allowComments = false;

        private unsafe char* ptr = null;
        private unsafe char* end = null;
        private int lineNum = 1;
        private CommentState commentState = CommentState.None;


        // This function will skip ahead if it encounters valid comment tags
        // A result of true indicates that the parser must skip the current character
        private unsafe bool UpdateCommentState()
        {
            if (commentState == CommentState.None)
            {
                char c = *ptr;
                if (c == '/')
                {
                    if (allowComments && ptr < end - 1)
                    {
                        c = *++ptr;
                        ptr++;
                        switch (c)
                        {
                            case '/': commentState = CommentState.SingleLine; return true;
                            case '*': commentState = CommentState.MultiLine; return true;
                        }
                    }
                    throw new FormatException($"Unexpected character '{c}' (line {lineNum})");
                }
            }
            else
            {
                char c = *ptr++;
                switch (c)
                {
                    case '*':
                    {
                        if (commentState == CommentState.MultiLine && ptr < end)
                        {
                            c = *ptr++;
                            if (c == '/')
                            {
                                commentState = CommentState.None;
                                return true;
                            }
                        }
                    }
                    throw new FormatException($"Unexpected character '{c}' (line {lineNum})");

                    case '\n':
                    {
                        if (commentState == CommentState.SingleLine)
                        {
                            commentState = CommentState.None;
                        }

                        lineNum++;
                    }
                    break;
                }
                return true;
            }

            return false;
        }

        private void EndOfFile()
        {
            // Ensure no dangling comments
            if (commentState == CommentState.MultiLine)
            {
                throw new FormatException($"Unterminated comment (line {lineNum})");
            }
        }

        private unsafe TokenType ParseNext(out JsonValue val)
        {
            val = new JsonValue();

            while (ptr < end)
            {
                if (UpdateCommentState())
                {
                    continue;
                }

                char* beg = ptr;
                char c = *ptr++;
                switch (c)
                {
                    // Skip whitespaces
                    case ' ':
                    case '\t':
                    case '\r':
                    case '\v':
                        continue;

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
                        if (stringBuffer == null)
                        {
                            stringBuffer = new StringBuilder(JsonValue.StringBufferCapacity);
                        }
                        else
                        {
                            stringBuffer.Clear();
                        }

                        // Iterate string characters
                        while (ptr < end)
                        {
                            c = *ptr++;
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

                                // Character escapes
                                case '\\':
                                {
                                    long remaining = end - ptr;
                                    if (remaining > 0)
                                    {
                                        switch (*ptr)
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
                                                if (remaining >= 5 && uint.TryParse(new string(ptr, 1, 4), NumberStyles.AllowHexSpecifier, null, out uint charCode))
                                                {
                                                    stringBuffer.Append((char)charCode);

                                                    // Skip escaped character + char code
                                                    ptr += 5;
                                                    continue;
                                                }
                                            }
                                            goto InvalidEscape;
                                            default: goto InvalidEscape;
                                        }

                                        // Skip escaped character
                                        ptr++;
                                        continue;
                                    }
                                InvalidEscape:
                                    throw new FormatException($"Invalid character escape sequence (line {lineNum})");
                                }

                                // End of string
                                case '"':
                                {
                                    val = stringBuffer.Length > 0 ? stringBuffer.ToString() : string.Empty;
                                    return TokenType.String;
                                }
                            }
                            stringBuffer.Append(c);
                        }
                        throw new FormatException($"Unterminated string (line {lineNum})");
                    }
                    default:
                    {
                        // Only allow numbers, letters and minus
                        if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9') || c == '-')
                        {
                            // Continue until token or whitespace
                            for (; ptr < end; ptr++)
                            {
                                switch (*ptr)
                                {
                                    case ' ':
                                    case '\t':
                                    case '\v':
                                    case '\r':
                                    case '\n':
                                    case '/':
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
                            // Try parse value
                            long len = ptr - beg;
                            if (len > 0)
                            {
                                if (len > int.MaxValue)
                                {
                                    throw new OverflowException();
                                }

                                string subStr = new string(beg, 0, (int)len);
                                if (char.IsDigit(subStr[0]) || subStr[0] == '-')
                                {
                                    if (double.TryParse(subStr, NumberStyles.Float, CultureInfo.InvariantCulture, out double d))
                                    {
                                        val = d;
                                        return TokenType.Value;
                                    }
                                }
                                else if (subStr == JsonValue.TrueConstant)
                                {
                                    val = true;
                                    return TokenType.Value;
                                }
                                else if (subStr == JsonValue.FalseConstant)
                                {
                                    val = false;
                                    return TokenType.Value;
                                }
                                else if (subStr == JsonValue.NullConstant)
                                {
                                    return TokenType.Value;
                                }

                                throw new FormatException($"Unknown expression '{subStr}' (line {lineNum})");
                            }
                        }
                        throw new FormatException($"Unexpected character '{c}' (line {lineNum})");
                    }
                }
            }

            // End of file
            EndOfFile();
            return TokenType.Eof;
        }

        private unsafe TokenType PeekNext(bool consume)
        {
            // Continue iterating until a known token is encountered
            while (ptr < end)
            {
                if (UpdateCommentState())
                {
                    continue;
                }

                char c = *ptr;
                switch (c)
                {
                    // Skip whitespaces
                    case ' ':
                    case '\t':
                    case '\v':
                    case '\r':
                        ptr++;
                        continue;

                    case '\n':
                        ptr++;
                        lineNum++;
                        continue;

                    // If its a known token, we can consume it
                    case '{': if (consume) ptr++; return TokenType.BraceOpen;
                    case '}': if (consume) ptr++; return TokenType.BraceClose;
                    case '[': if (consume) ptr++; return TokenType.BracketOpen;
                    case ']': if (consume) ptr++; return TokenType.BracketClose;
                    case ',': if (consume) ptr++; return TokenType.Comma;
                    case ':': if (consume) ptr++; return TokenType.Colon;

                    default: return TokenType.Unknown;
                }
            }

            // Ensure no dangling comments
            EndOfFile();
            return TokenType.Eof;
        }
        private unsafe bool PeekNext(TokenType expected)
        {
            TokenType encountered = PeekNext(false);
            if (encountered == expected)
            {
                if (encountered != TokenType.Eof)
                {
                    ptr++;
                }
                return true;
            }
            return false;
        }

        private unsafe JsonValue ParseObjectRecursive(int level)
        {
            if (level >= maxRecursion)
            {
                throw new OverflowException($"Max recursion level reached ({maxRecursion})");
            }
            level++;

            JsonObject jsonObject = new JsonObject();
            if (!PeekNext(TokenType.BraceClose))
            {
            Next:
                if (ParseNext(out JsonValue key) != TokenType.String)
                {
                    throw new FormatException($"Expected key string (line {lineNum})");
                }

                if (!PeekNext(TokenType.Colon))
                {
                    throw new FormatException($"Missing colon after key string (line {lineNum})");
                }

                switch (ParseNext(out JsonValue val))
                {
                    case TokenType.String:
                    case TokenType.Value:
                        jsonObject.Add((string)key, val);
                        break;
                    case TokenType.BraceOpen:
                        jsonObject.Add((string)key, ParseObjectRecursive(level));
                        break;
                    case TokenType.BracketOpen:
                        jsonObject.Add((string)key, ParseArrayRecursive(level));
                        break;

                    default:
                        throw new FormatException($"Expected value (line {lineNum})");
                }

                switch (PeekNext(true))
                {
                    case TokenType.BraceClose: goto Exit;
                    case TokenType.Comma: goto Next;
                    default: throw new FormatException($"Expected closing brace (line {lineNum})");
                }
            }

        Exit:
            return jsonObject;
        }
        private unsafe JsonValue ParseArrayRecursive(int level)
        {
            if (level >= maxRecursion)
            {
                throw new OverflowException($"Max recursion level reached ({maxRecursion})");
            }
            level++;

            JsonArray jsonArray = new JsonArray();
            if (!PeekNext(TokenType.BracketClose))
            {
            Next:
                switch (ParseNext(out JsonValue val))
                {
                    case TokenType.String:
                    case TokenType.Value:
                        jsonArray.Add(val);
                        break;
                    case TokenType.BraceOpen:
                        jsonArray.Add(ParseObjectRecursive(level));
                        break;
                    case TokenType.BracketOpen:
                        jsonArray.Add(ParseArrayRecursive(level));
                        break;

                    default:
                        throw new FormatException($"Expected value (line {lineNum})");
                }

                switch (PeekNext(true))
                {
                    case TokenType.BracketClose: goto Exit;
                    case TokenType.Comma: goto Next;
                    default: throw new FormatException($"Expected closing bracket (line {lineNum})");
                }
            }

        Exit:
            return jsonArray;
        }

        private unsafe JsonValue Read(string str)
        {
            fixed (char* p = str)
            {
                ptr = p;
                end = p + str.Length;
                lineNum = 1;
                commentState = CommentState.None;

                switch (ParseNext(out JsonValue result))
                {
                    case TokenType.BraceOpen:
                        result = ParseObjectRecursive(0);
                        break;
                    case TokenType.BracketOpen:
                        result = ParseArrayRecursive(0);
                        break;
                    case TokenType.Value:
                    case TokenType.String:
                        break;
                    default:
                        throw new FormatException("Invalid Json string");
                }

                // Ensure eof
                if (!PeekNext(TokenType.Eof))
                {
                    throw new FormatException($"Unexpected expression at the end of file (line {lineNum})");
                }

                return result;
            }
        }

        public JsonValue FromJson(string str)
        {
            if (str == null) throw new ArgumentNullException(nameof(str));
            if (str.Length == 0) throw new ArgumentException($"Invalid Json string");
            return Read(str);
        }
        public object FromJson(string str, Type targetType)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));
            return JsonValue.FromJson(FromJson(str), targetType);
        }
        public T FromJson<T>(string str)
        {
            return (T)JsonValue.FromJson(FromJson(str), typeof(T));
        }
    }

    public sealed class JsonWriter
    {
        private const string AnonymousTypeName = "AnonymousType";
        private const string UnicodePrefix = "\\u00";

        // Max read recursion (how deep do we allow object references to go)
        public int maxRecursion = JsonValue.WriteMaxRecursion;
        // Stringbuffer used to build the output (will be reused if left unchanged)
        public StringBuilder stringBuffer = null;

        public string ToJson(object value)
        {
            if (stringBuffer == null)
            {
                stringBuffer = new StringBuilder(JsonValue.OutputBufferCapacity);
            }
            else
            {
                stringBuffer.Clear();
            }

            WriteRecursive(value, 0);

            return stringBuffer.ToString();
        }

        private void WriteRecursive(object value, int level)
        {
            if (level >= maxRecursion)
            {
                throw new OverflowException($"Max recursion level reached ({maxRecursion})");
            }
            level++;

            if (value is JsonValue asJsonValue)
            {
                value = asJsonValue.ToObject();
            }

            // Null
            if (value == null)
            {
                stringBuffer.Append(JsonValue.NullConstant);
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
                    stringBuffer.Append(value.ToString());
                    return;

                case TypeCode.Single:
                    WriteValue((float)value);
                    return;
                case TypeCode.Double:
                    WriteValue((double)value);
                    return;
                case TypeCode.DateTime:
                    WriteValue(((DateTime)value).ToString(JsonValue.DateTimeFormat, CultureInfo.InvariantCulture));
                    return;
            }

            bool written = false;
            if (value is IDictionary dictionary)
            {
                // Dictionaries/objects
                stringBuffer.Append('{');
                foreach (DictionaryEntry entry in dictionary)
                {
                    if (written) stringBuffer.Append(',');
                    written = true;
                    if (!(entry.Key is string key))
                    {
                        throw new InvalidCastException($"Dictionary key type has to be string");
                    }
                    WriteValue(entry.Key as string);
                    stringBuffer.Append(':');
                    WriteRecursive(entry.Value, level);
                }
                stringBuffer.Append('}');
                return;
            }
            else if (value is IEnumerable list)
            {
                // Arrays
                stringBuffer.Append('[');
                foreach (object entry in list)
                {
                    if (written) stringBuffer.Append(',');
                    written = true;
                    WriteRecursive(entry, level);
                }
                stringBuffer.Append(']');
                return;
            }
            else
            {
                // Structs/classes
                stringBuffer.Append('{');
                foreach (FieldInfo field in type.GetFields(BindingFlags.Instance | BindingFlags.Public))
                {
                    if (written) stringBuffer.Append(',');
                    written = true;
                    WriteValue(field.Name);
                    stringBuffer.Append(':');
                    WriteRecursive(field.GetValue(value), level);
                }
                if (IsAnonymousType(type))
                {
                    // Read properties (anonymous types only)
                    foreach (PropertyInfo property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
                    {
                        if (!property.CanRead) continue;
                        if (written) stringBuffer.Append(',');
                        written = true;
                        WriteValue(property.Name);
                        stringBuffer.Append(':');
                        WriteRecursive(property.GetValue(value), level);
                    }
                }
                stringBuffer.Append('}');
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
                    stringBuffer.Append('\\');
                    stringBuffer.Append('t');
                    return true;
                case '\n':
                    stringBuffer.Append('\\');
                    stringBuffer.Append('n');
                    return true;
                case '\r':
                    stringBuffer.Append('\\');
                    stringBuffer.Append('r');
                    return true;
                case '\f':
                    stringBuffer.Append('\\');
                    stringBuffer.Append('f');
                    return true;
                case '\b':
                    stringBuffer.Append('\\');
                    stringBuffer.Append('b');
                    return true;
                case '"':
                    stringBuffer.Append('\\');
                    stringBuffer.Append('\"');
                    return true;
                case '\\':
                    stringBuffer.Append('\\');
                    stringBuffer.Append('\\');
                    return true;
            }

            // Anything above 1f (space and onwards) can be represented as character,
            // the output string will be converted to utf8 at save time
            if (value >= '\u001f')
            {
                stringBuffer.Append(value);
                return true;
            }

            return false;
        }
        private void WriteValue(string value)
        {
            stringBuffer.Append('"');
            foreach (char c in value)
            {
                if (!WriteValue(c))
                {
                    // Handle unsupported characters
                    stringBuffer.Append(UnicodePrefix);
                    int num = c;
                    stringBuffer.Append((char)(48 + (num >> 4)));
                    num &= 0xF;
                    stringBuffer.Append((char)((num < 10) ? (48 + num) : (97 + (num - 10))));
                }
            }
            stringBuffer.Append('"');
        }
        private void WriteValue(bool value)
        {
            stringBuffer.Append(value ? JsonValue.TrueConstant : JsonValue.FalseConstant);
        }
        private void WriteValue(float value)
        {
            stringBuffer.Append(value.ToString(JsonValue.DoubleFormat, CultureInfo.InvariantCulture));
        }
        private void WriteValue(double value)
        {
            stringBuffer.Append(value.ToString(JsonValue.DoubleFormat, CultureInfo.InvariantCulture));
        }

        private void WriteEnumValue(object value, TypeCode typeCode)
        {
            switch (typeCode)
            {
                case TypeCode.SByte:
                    stringBuffer.Append((sbyte)value);
                    break;
                case TypeCode.Int16:
                    stringBuffer.Append((short)value);
                    break;
                case TypeCode.UInt16:
                    stringBuffer.Append((ushort)value);
                    break;
                case TypeCode.Int32:
                    stringBuffer.Append((int)value);
                    break;
                case TypeCode.Byte:
                    stringBuffer.Append((byte)value);
                    break;
                case TypeCode.UInt32:
                    stringBuffer.Append((uint)value);
                    break;
                case TypeCode.Int64:
                    stringBuffer.Append((long)value);
                    break;
                case TypeCode.UInt64:
                    stringBuffer.Append((ulong)value);
                    break;
                default:
                    throw new InvalidOperationException($"Invalid type code for enum: '{typeCode}'");
            }
        }
    }
}