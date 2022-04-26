using Citrouille.Shared.Abstractions.Exceptions;

namespace Citrouille.Domain.Exceptions;

public class EmptyCollectionFieldNameException : BaseException
{
    public EmptyCollectionFieldNameException() : base("Collection field name cannot be empty.") {}
}