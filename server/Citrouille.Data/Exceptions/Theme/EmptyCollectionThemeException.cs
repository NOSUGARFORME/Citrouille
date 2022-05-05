using Citrouille.Shared.Exceptions;

namespace Citrouille.Data.Exceptions.Theme;

public class EmptyCollectionThemeException : BadRequestException
{
    public EmptyCollectionThemeException() : base("Collection theme cannot be empty.") {}
}