using System.Dynamic;

namespace BoxScript.Core;

/// <summary>
/// A dynamic proxy for script objects that allows access to properties and methods
/// </summary>
/// <param name="_instance">The object instance to proxy</param>
public class ScriptProxy(object _instance) : DynamicObject
{
    private readonly Dictionary<string, PropertyInfo> _properties = new(StringComparer.InvariantCultureIgnoreCase);
    private readonly Dictionary<string, Delegate> _methods = new(StringComparer.InvariantCultureIgnoreCase);
    private bool _isInitialized = false;

    private void FillCache()
    {
        if (_isInitialized) return;

        _isInitialized = true;
        var type = _instance.GetType();
        foreach (var prop in type.GetProperties())
        {
            var name = prop.GetCustomAttribute<ModuleExportAttribute>()?.Name?.ForceNull() ?? prop.Name;
            _properties[name] = prop;
        }

        foreach (var method in type.GetMethods())
        {
            var name = method.GetCustomAttribute<ModuleExportAttribute>()?.Name?.ForceNull() ?? method.Name;
            _methods[name] = method.ToDelegate(_instance);
        }
    }

    /// <inheritdoc />
    public override bool TryGetMember(GetMemberBinder binder, out object? result)
    {
        FillCache();
        if (_properties.TryGetValue(binder.Name, out var property))
        {
            result = property.GetValue(_instance);
            return true;
        }

        if (_methods.TryGetValue(binder.Name, out var method))
        {
            result = method;
            return true;
        }

        result = null;
        return false;
    }

    /// <inheritdoc />
    public override bool TrySetMember(SetMemberBinder binder, object? value)
    {
        FillCache();
        if (!_properties.TryGetValue(binder.Name, out var property))
            return false;

        property.SetValue(_instance, value);
        return true;
    }
}
