using Citrouille.Domain.Entities;
using Citrouille.Domain.ValueObjects;

namespace Citrouille.Domain.Repositories;

public interface ICollectionRepository
{
    Task<Collection> GetAsync(CollectionId id);
    Task AddAsync(Collection collection);
    Task UpdateAsync(Collection collection);
    Task DeleteAsync(Collection collection);
}