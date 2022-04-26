using Citrouille.Domain.Entities;
using Citrouille.Shared.Abstractions.Domain;

namespace Citrouille.Domain.Events;

public record CollectionItemAdded(Collection Collection, CollectionItem CollectionItem) : IDomainEvent;