using Citrouille.Data.Entities;

namespace Citrouille.Infrastructure.Commands.Models;

public record UpdateCollectionItem(Guid CollectionId, Guid CollectionItemId, string Name, List<Field> Fields);