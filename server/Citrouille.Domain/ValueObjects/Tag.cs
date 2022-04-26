using Citrouille.Domain.Exceptions;

namespace Citrouille.Domain.ValueObjects;

public record Tag
{
    public string Value { get; }

    public Tag(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyCollectionTagException();
        }
            
        Value = value;
    }

    public static implicit operator string(Tag tag)
        => tag.Value;
        
    public static implicit operator Tag(string tag)
        => new(tag);
}