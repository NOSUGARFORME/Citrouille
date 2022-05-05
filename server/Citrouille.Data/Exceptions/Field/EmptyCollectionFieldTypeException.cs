using Citrouille.Shared.Exceptions;

namespace Citrouille.Data.Exceptions.Field;

public class EmptyCollectionFieldTypeException : BadRequestException
{
    public EmptyCollectionFieldTypeException() : base("Collection field type cannot be empty.") {}
}