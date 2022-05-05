using Citrouille.Shared.Exceptions;

namespace Citrouille.Infrastructure.Commands.Exceptions;

public class CollectionAlreadyExistsException : BadRequestException
{
    public string Title { get; }

    public CollectionAlreadyExistsException(string title) 
        : base($"Collection with title '{title}' already exists.")
    {
        Title = title;
    }
}