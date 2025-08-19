using JsonSerializer = Jint.Native.Json.JsonSerializer;
using JsonDeserializer = Jint.Native.Json.JsonParser;

namespace BoxScript.Modules;

using Core;

/// <summary>
/// A module that provides JSON functionality to box-scripts
/// </summary>
public interface IJsonModule : IScriptModule
{
    /// <summary>
    /// Serializes the given value to a JSON string.
    /// </summary>
    /// <param name="value">The value</param>
    /// <param name="spacer">The number of spaces to use for pretty printing</param>
    /// <returns>The JSON string</returns>
    string Serialize(JsValue value, int? spacer = null);

    /// <summary>
    /// Deserializes the given JSON string to a JsValue object.
    /// </summary>
    /// <param name="value">The value</param>
    /// <returns>The deserialized value</returns>
    JsValue Deserialize(string value);
}

/// <inheritdoc cref="IJsonModule" />
[Module("json")]
public class JsonModule(
    Engine _engine) : IJsonModule
{
    /// <inheritdoc />
    [ModuleExport]
    public string Serialize([ModuleExport(type: typeof(object))] JsValue value, int? spacer = null)
    {
        var ser = new JsonSerializer(_engine);
        return ser.Serialize(value, null!, spacer!).AsString();
    }

    /// <inheritdoc />
    [ModuleExport]
    public JsValue Deserialize(string value)
    {
        var ser = new JsonDeserializer(_engine);
        return ser.Parse(value);
    }
}
