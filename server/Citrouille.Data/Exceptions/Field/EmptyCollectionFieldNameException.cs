using Citrouille.Shared.Exceptions;

namespace Citrouille.Data.Exceptions.Field;

public class EmptyCollectionFieldNameException : BadRequestException
{
    public EmptyCollectionFieldNameException() : base("Collection field name cannot be empty.") {}
}