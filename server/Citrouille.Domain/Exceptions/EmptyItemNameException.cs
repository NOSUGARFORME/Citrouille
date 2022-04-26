using Citrouille.Shared.Abstractions.Exceptions;

namespace Citrouille.Domain.Exceptions;

public class EmptyItemNameException: BaseException
{
    public EmptyItemNameException() : base("Collection item name cannot be empty.") {}
}