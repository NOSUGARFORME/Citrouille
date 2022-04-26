using Citrouille.Shared.Abstractions.Exceptions;

namespace Citrouille.Domain.Exceptions;

public class EmptyCollectionIdException : BaseException
{
    public EmptyCollectionIdException() : base("Collection ID cannot be empty.") {}
}