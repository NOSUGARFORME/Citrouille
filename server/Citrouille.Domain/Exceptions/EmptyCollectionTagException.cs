using Citrouille.Shared.Abstractions.Exceptions;

namespace Citrouille.Domain.Exceptions;

public class EmptyCollectionTagException : BaseException
{
    public EmptyCollectionTagException() : base("Collection tag cannot be empty.") {}
}