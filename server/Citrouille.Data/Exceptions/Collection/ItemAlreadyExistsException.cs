using Citrouille.Shared.Exceptions;

namespace Citrouille.Data.Exceptions.Collection;

public class ItemAlreadyExistsException : BadRequestException
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