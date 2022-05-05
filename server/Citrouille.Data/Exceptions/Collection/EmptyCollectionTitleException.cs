using Citrouille.Shared.Exceptions;

namespace Citrouille.Data.Exceptions.Collection;

public class EmptyCollectionTitleException : BadRequestException
{
    public EmptyCollectionTitleException() : base("Collection title cannot be empty.") {}
}