using Citrouille.Shared.Commands;

namespace Citrouille.Application.Commands;

public record CreateCollectionItem(Guid Id, Guid CollectionId, string Name) : ICommand;