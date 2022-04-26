using Citrouille.Domain.Exceptions;

namespace Citrouille.Domain.ValueObjects;

public record CollectionTheme
{
    public string Value { get; }

    public CollectionTheme(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyCollectionThemeException();
        }

        Value = value;
    }
    
    public static implicit operator string(CollectionTheme theme)
        => theme.Value;
        
    public static implicit operator CollectionTheme(string theme)
        => new(theme);
}
