using Citrouille.Shared.Exceptions;

namespace Citrouille.Data.Exceptions.Collection;

public class CollectionItemNotFoundException : BadRequestException
{
    public string ItemName { get; }

    public CollectionItemNotFoundException(string name) : base($"Collection item '{name}' was not found.")
        => ItemName = name;
}