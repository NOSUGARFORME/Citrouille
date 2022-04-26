using Citrouille.Domain.Exceptions;

namespace Citrouille.Domain.ValueObjects;

public record CollectionId
{
    public Guid Value { get; }

    public CollectionId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new EmptyCollectionIdException();
        }

        Value = value;
    }

    public static implicit operator Guid(CollectionId id)
        => id.Value;

    public static implicit operator CollectionId(Guid id)
        => new(id);
}
