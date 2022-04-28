using Citrouille.Domain.Entities;
using Citrouille.Shared.Domain;

namespace Citrouille.Domain.Events;

public record CollectionItemRemoved(Collection Collection, CollectionItem CollectionItem) : IDomainEvent;