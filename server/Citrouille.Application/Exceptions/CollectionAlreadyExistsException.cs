using Citrouille.Shared.Exceptions;

namespace Citrouille.Application.Exceptions;

public class CollectionAlreadyExistsException : BaseException
{
    public string Name { get; }

    public CollectionAlreadyExistsException(string name) 
        : base($"Collection with name '{name}' already exists.")
    {
        Name = name;
    }
}