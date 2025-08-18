namespace BoxScript.Documentation;

using Core;
using Models;

/// <summary>
/// A service for rendering index.d.ts files for modules
/// </summary>
public interface IIndexRenderService
{
    /// <summary>
    /// Write the index.d.ts file
    /// </summary>
    /// <param name="writer">The stream writer to use</param>
    /// <param name="moduleName">The name of the module</param>
    Task WriteIndex(StreamWriter writer, string moduleName);
}

internal class IndexRenderService(
    IJsTypeService _types,
    IDocumentReflectionService _classes,
    IEnumerable<IScriptModule> _modules,
    IScriptEngineSettings _settings) : IIndexRenderService
{
    private const string INDENT_CHAR = "\t";

    public IEnumerable<string> Property(Property property)
    {
        var type = _types.TypeName(property.Type);
        var nullable = property.Nullable ? "?" : string.Empty;
        var ro = property.ReadOnly ? "readonly " : string.Empty;

        if (!string.IsNullOrEmpty(property.Comments.Summary))
        {
            var remarks = string.IsNullOrEmpty(property.Comments.Remarks)
                ? string.Empty
                : $" - {property.Comments.Remarks}";
            yield return $"/** {property.Comments.Summary}{remarks} */";
        }

        yield return $"{ro}{property.Name}{nullable}: {type};";
    }

    public IEnumerable<string> Method(Method method)
    {
        string Declaration(string type)
        {
            var bob = new StringBuilder();
            bob.Append(method.Name);
            bob.Append('(');

            for(var i = 0; i < method.Parameters.Length; i++)
            {
                if (i != 0) bob.Append(", ");
                var param = method.Parameters[i];
                var paramType = _types.TypeName(param.Type);
                var nullable = param.Nullable ? "?" : string.Empty;
                var isSpread = param.ArrayParam ? "..." : string.Empty;
                bob.Append($"{isSpread}{param.Name}{nullable}: {paramType}");
            }

            bob.Append("): ");
            bob.Append(type);
            bob.Append(';');
            return bob.ToString();
        }

        var type = _types.TypeName(method.ReturnType);

        yield return $"/**";
        var remarks = string.IsNullOrEmpty(method.Comments.Remarks)
            ? string.Empty
            : $" - {method.Comments.Remarks}";
        yield return $" * {method.Comments.Summary}{remarks}";

        foreach(var par in method.Parameters)
        {
            var paramType = _types.TypeName(par.Type);
            var name = par.Name;
            if (par.Nullable)
                name = $"[{name}]";
            yield return $" * @param {{{paramType}}} {name} {par.Comments.Summary}";
        }

        var returns = string.IsNullOrEmpty(method.Comments.Returns)
            ? string.Empty
            : $" {method.Comments.Returns}";
        yield return $" * @returns {{{type}}}{returns}";
        yield return $" */";
        yield return Declaration(type);
    }

    public static IEnumerable<string> Class(Class item)
    {
        if (!string.IsNullOrEmpty(item.Comments.Summary))
        {
            var remarks = string.IsNullOrEmpty(item.Comments.Remarks)
                ? string.Empty
                : $" - {item.Comments.Remarks}";
            yield return $"/** {item.Comments.Summary}{remarks} */";
        }

        yield return $"interface {item.Name} {{";
    }

    public static IEnumerable<string> Enum(string name, Comments desc)
    {
        if (!string.IsNullOrEmpty(desc.Summary))
        {
            var remarks = string.IsNullOrEmpty(desc.Remarks)
                ? string.Empty
                : $" - {desc.Remarks}";
            yield return $"/** {desc.Summary}{remarks} */";
        }

        yield return $"export class {name} {{";

        foreach(var value in desc.Options)
        {
            if (!string.IsNullOrEmpty(value.Description))
            {
                yield return $"\t/**";
                yield return $"\t * {value.Description} - {value.Value}";
                yield return $"\t * @type {{number}}";
                yield return "\t */";
            }

            yield return $"\tstatic get {value.Name}(): number;";
        }

        yield return "}";
    }

    public static string Scope(string line, int indent)
    {
        return string.Join("", Enumerable.Repeat(INDENT_CHAR, indent - 1)) + line;
    }

    public async Task WriteInterface(StreamWriter writer, Type type, int indent)
    {
        await writer.WriteLineAsync();
        var def = _classes.Get(type);

        foreach(var line in Class(def))
            await writer.WriteLineAsync(Scope(line, indent));

        foreach (var prop in def.Properties)
            foreach (var line in Property(prop))
                await writer.WriteLineAsync(Scope(line, indent + 1));

        foreach (var method in def.Methods)
            foreach (var line in Method(method))
                await writer.WriteLineAsync(Scope(line, indent + 1));

        await writer.WriteLineAsync(Scope("}", indent));
    }

    public async Task WriteEnum(StreamWriter writer, Comments desc, string name, int indent)
    {
        await writer.WriteLineAsync();
        foreach(var line in Enum(name, desc))
            await writer.WriteLineAsync(Scope(line, indent));
    }

    public IEnumerable<Type> YieldTypes(Type type, HashSet<Type> found)
    {
        //Skip types that have already been found
        if (found.Contains(type)) yield break;
        //Skip default types
        if (_types.DefaultTypes.ContainsKey(type)) yield break;
        //Enums have no children - yield the current and skip rest
        if (type.IsEnum)
        {
            found.Add(type);
            yield return type;
            yield break;
        }
        //Handle dictionaries
        if (type.IsDictionary(out var key, out var val))
        {
            foreach(var res in YieldTypes(key, found))
                yield return res;
            foreach (var res in YieldTypes(val, found))
                yield return res;
            yield break;
        }
        //Handle collections
        if (type.IsCollection(out var arg))
        {
            foreach (var res in YieldTypes(arg, found))
                yield return res;
            yield break;
        }
        //Handle tasks
        if (type.IsTask(out var taskArg))
        {
            if (taskArg is null)
                yield break;

            foreach (var res in YieldTypes(taskArg, found))
                yield return res;
            yield break;
        }
        //Found unrestricted type
        found.Add(type);
        foreach(var explode in ExplodeClass(type, found))
            yield return explode;

        yield return type;
    }

    public IEnumerable<Type> ExplodeClass(Type type, HashSet<Type> found)
    {
        var item = _classes.Get(type);
        var props = item.Properties
                .Select(t => t.Type)
                .Distinct()
                .SelectMany(t => YieldTypes(t, found));
        var methods = item.Methods
            .SelectMany(t => t.Parameters
                .Select(t => t.Type)
                .Append(t.ReturnType)
                .Distinct()
                .SelectMany(t => YieldTypes(t, found)));
        return methods.Concat(props);
    }

    public async Task WriteIndex(StreamWriter writer, string moduleName)
    {
        await writer.WriteLineAsync("// This file is auto-generated by BoxScript documentation service.");
        await writer.WriteLineAsync($"declare module \"{moduleName}\" {{");
        int indent = 1;
        var types = new HashSet<Type>();
        var defaults = new HashSet<string>();

        foreach(var type in _settings.Enums)
        {
            types.Add(type);
            var comment = _classes.Enum(type);
            await WriteEnum(writer, comment, type.Name, indent + 1);
        }

        foreach(var module in _modules)
        {
            var modType = module.GetType();
            var item = _classes.Get(modType);
            defaults.Add(item.Name);

            var allTypes = ExplodeClass(modType, types);
            foreach (var type in allTypes)
                await WriteInterface(writer, type, indent + 1);

            await WriteInterface(writer, modType, indent + 1);
        }

        foreach(var def in defaults)
            await writer.WriteLineAsync(Scope($"export var {def}: {def};", indent + 1));

        await writer.WriteLineAsync("}");
    }
}
