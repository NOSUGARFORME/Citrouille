using Citrouille.Shared.Exceptions;

namespace Citrouille.Domain.Exceptions;

public class CollectionItemNotFoundException : BaseException
{
    public string ItemName { get; }

    public CollectionItemNotFoundException(string name) : base($"Collection item '{name}' was not found.")
        => ItemName = name;
}