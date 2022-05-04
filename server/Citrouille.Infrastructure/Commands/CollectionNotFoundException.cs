using Citrouille.Shared.Exceptions;

namespace Citrouille.Infrastructure.Commands;

public class CollectionNotFoundException : BadRequestException
{
    public Guid Id { get; }

    public CollectionNotFoundException(Guid id) : base($"Collection with ID '{id}' was not found.")
        => Id = id;
}