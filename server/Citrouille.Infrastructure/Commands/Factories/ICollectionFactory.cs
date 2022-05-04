using Citrouille.Data.Entities;

namespace Citrouille.Infrastructure.Commands.Factories;

public interface ICollectionFactory
{
    Collection Create(Guid id, string name, string description);

    Collection CreateWithItems(Guid id, string name, string description, List<CollectionItem> items);
}