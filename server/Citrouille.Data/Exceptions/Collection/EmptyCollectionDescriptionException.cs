using Citrouille.Shared.Exceptions;

namespace Citrouille.Data.Exceptions.Collection;

public class EmptyCollectionDescriptionException : BadRequestException
{
    public EmptyCollectionDescriptionException() : base("Collection description cannot be empty.") {}
}