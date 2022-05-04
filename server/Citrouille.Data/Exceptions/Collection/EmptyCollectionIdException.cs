using Citrouille.Shared.Exceptions;

namespace Citrouille.Data.Exceptions.Collection;

public class EmptyCollectionIdException : BadRequestException
{
    public EmptyCollectionIdException() : base("Collection ID cannot be empty.") {}
}