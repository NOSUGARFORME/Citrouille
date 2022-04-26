using Citrouille.Shared.Abstractions.Exceptions;

namespace Citrouille.Domain.Exceptions;

public class ItemAlreadyExistsException : BaseException
{
    public string CollectionName { get; }
    public string ItemName { get; }

    public ItemAlreadyExistsException(string collectionName, string itemName)
        : base($"Collection: {collectionName} already defined item {itemName}")
    {
        CollectionName = collectionName;
        ItemName = itemName;
    }
}