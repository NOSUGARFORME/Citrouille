using Citrouille.Shared.Abstractions.Exceptions;

namespace Citrouille.Domain.Exceptions;

public class EmptyCollectionFieldTypeException : BaseException
{
    public EmptyCollectionFieldTypeException() : base("Collection field type cannot be empty.") {}
}