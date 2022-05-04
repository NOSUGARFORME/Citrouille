using Citrouille.Shared.Exceptions;

namespace Citrouille.Data.Exceptions.Tag;

public class EmptyCollectionTagNameException : BadRequestException
{
    public EmptyCollectionTagNameException() : base("Collection tag name cannot be empty.") {}
}