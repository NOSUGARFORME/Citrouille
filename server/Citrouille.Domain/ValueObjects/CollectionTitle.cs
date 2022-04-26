using Citrouille.Domain.Exceptions;

namespace Citrouille.Domain.ValueObjects;

public record CollectionTitle
{
    public string Value { get; }

    public CollectionTitle(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyCollectionTitleException();
        }

        Value = value;
    }
    
    public static implicit operator string(CollectionTitle title)
        => title.Value;
        
    public static implicit operator CollectionTitle(string title)
        => new(title);
}