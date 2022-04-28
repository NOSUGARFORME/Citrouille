using Citrouille.Shared.Exceptions;

namespace Citrouille.Domain.Exceptions;

public class EmptyCollectionTitleException : BaseException
{
    public EmptyCollectionTitleException() : base("Collection title cannot be empty.") {}
}