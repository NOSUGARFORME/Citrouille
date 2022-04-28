using Citrouille.Shared.Exceptions;

namespace Citrouille.Domain.Exceptions;

public class EmptyCollectionDescriptionException : BaseException
{
    public EmptyCollectionDescriptionException() : base("Collection description cannot be empty.") {}
}