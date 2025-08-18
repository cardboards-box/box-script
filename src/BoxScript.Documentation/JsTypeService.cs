using BoxScript.Core;
using Jint.Native;
using System.Diagnostics.CodeAnalysis;

namespace BoxScript.Documentation;

/// <summary>
/// A service that converts C# types to JavaScript types
/// </summary>
public interface IJsTypeService
{
    /// <summary>
    /// The default types that shouldn't be added to interfaces
    /// </summary>
    IReadOnlyDictionary<Type, string> DefaultTypes { get; }

    /// <summary>
    /// Get the JS version of the type
    /// </summary>
    /// <param name="type">The type to fetch</param>
    /// <returns>The JS version of the type</returns>
    string TypeName(Type type);
}

internal class JsTypeService(
    IDocumentReflectionService _reflection) : IJsTypeService
{
    private readonly Dictionary<Type, string> _jsNames = [];

    public IReadOnlyDictionary<Type, string> DefaultTypes { get; } = new Dictionary<Type, string>
    {
        [typeof(string)] = "string",
        [typeof(char)] = "string",
        [typeof(bool)] = "boolean",
        [typeof(byte)] = "number",
        [typeof(sbyte)] = "number",
        [typeof(short)] = "number",
        [typeof(ushort)] = "number",
        [typeof(int)] = "number",
        [typeof(uint)] = "number",
        [typeof(long)] = "number",
        [typeof(ulong)] = "number",
        [typeof(float)] = "number",
        [typeof(double)] = "number",
        [typeof(decimal)] = "number",
        [typeof(object)] = "any",
        [typeof(void)] = "void",
        [typeof(JsValue)] = "any",
        [typeof(DateTime)] = "Date",
        [typeof(DateTimeOffset)] = "Date",
        [typeof(TimeSpan)] = "string",
        [typeof(Guid)] = "string",
        [typeof(Uri)] = "string",
        [typeof(Task)] = "Promise<void>",
        [typeof(ValueTask)] = "Promise<void>"
    };

    public void FillCache()
    {
        if (_jsNames.Count > 0) return;

        foreach (var (type, name) in DefaultTypes)
            _jsNames[type] = name;
    }

    public bool HandleCollections(Type type, [MaybeNullWhen(false)] out string name)
    {
        if (!type.IsCollection(out var elem))
        {
            name = null;
            return false;
        }

        name = _jsNames[type] = $"{TypeName(elem)}[]";
        return true;
    }

    public bool HandleDictionaries(Type type, [MaybeNullWhen(false)] out string name)
    {
        if (!type.IsDictionary(out var keyType, out var valueType))
        {
            name = null;
            return false;
        }

        name = _jsNames[type] = $"{{ [key: {TypeName(keyType)}]: {TypeName(valueType)} }}";
        return true;
    }

    public bool HandleTasks(Type type, [MaybeNullWhen(false)] out string name)
    {
        if (!type.IsTask(out var arg))
        {
            name = null;
            return false;
        }

        if (arg is null)
        {
            name = _jsNames[type] = "Promise<void>";
            return true;
        }

        name = _jsNames[type] = $"Promise<{TypeName(arg)}>";
        return true;
    }

    public string TypeName(Type type)
    {
        FillCache();

        if (_jsNames.TryGetValue(type, out var name))
            return name;

        if (HandleDictionaries(type, out name))
            return name;

        if (HandleCollections(type, out name))
            return name;

        if (HandleTasks(type, out name))
            return name;

        var doc = _reflection.Get(type);
        return _jsNames[type] = doc.Name;
    }
}
