using Citrouille.Shared.Exceptions;

namespace Citrouille.Domain.Exceptions;

public class EmptyCollectionThemeException : BaseException
{
    public EmptyCollectionThemeException() : base("Collection theme cannot be empty.") {}
}