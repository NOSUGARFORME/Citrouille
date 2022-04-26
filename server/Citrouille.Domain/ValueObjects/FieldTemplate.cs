using Citrouille.Domain.Exceptions;

namespace Citrouille.Domain.ValueObjects;

public record FieldTemplate
{
    public string Name { get; }
    public string Type { get; }

    public FieldTemplate(string name, string type)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new EmptyCollectionFieldNameException();
        }
        
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new EmptyCollectionFieldTypeException();
        }

        Name = name;
        Type = type;
    }
}
