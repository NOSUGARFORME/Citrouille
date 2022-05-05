using Citrouille.Data.Entities;

namespace Citrouille.Infrastructure.Commands.Models;

public record AddCollectionItem(Guid CollectionId, string Name, List<Field> Fields);