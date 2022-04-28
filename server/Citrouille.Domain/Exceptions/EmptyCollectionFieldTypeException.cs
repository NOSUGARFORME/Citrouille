using Citrouille.Shared.Exceptions;

namespace Citrouille.Domain.Exceptions;

public class EmptyCollectionFieldTypeException : BaseException
{
    public EmptyCollectionFieldTypeException() : base("Collection field type cannot be empty.") {}
}