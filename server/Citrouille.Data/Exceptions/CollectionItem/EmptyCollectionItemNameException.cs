using Citrouille.Shared.Exceptions;

namespace Citrouille.Data.Exceptions.CollectionItem;

public class EmptyCollectionItemNameException: BadRequestException
{
    public EmptyCollectionItemNameException() : base("Collection item name cannot be empty.") {}
}