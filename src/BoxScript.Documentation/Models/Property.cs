namespace BoxScript.Documentation.Models;

/// <summary>
/// Represents a property on a class for documentation generation
/// </summary>
/// <param name="Name">The name of the property</param>
/// <param name="Type">The type of the property</param>
/// <param name="ReadOnly">Whether or not the property only has a getter, or if it has a setter as well</param>
/// <param name="Nullable">Whether or not the property is optional / nullable</param>
/// <param name="ArrayParam">If this is a method parameter, and it's a spread array operator this will be true</param>
/// <param name="Comments">The comments for the item</param>
public record class Property(
    string Name,
    Type Type,
    bool ReadOnly,
    bool Nullable,
    bool ArrayParam,
    Comments Comments);
