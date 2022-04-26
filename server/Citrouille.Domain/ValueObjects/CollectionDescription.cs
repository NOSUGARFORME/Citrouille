using Citrouille.Domain.Exceptions;

namespace Citrouille.Domain.ValueObjects;

public record CollectionDescription
{
    public string Value { get; }

    public CollectionDescription(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyCollectionDescriptionException();
        }
            
        Value = value;
    }

    public static implicit operator string(CollectionDescription description)
        => description.Value;
        
    public static implicit operator CollectionDescription(string description)
        => new(description);
}