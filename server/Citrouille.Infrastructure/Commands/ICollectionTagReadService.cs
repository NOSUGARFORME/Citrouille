using Citrouille.Data.Entities;

namespace Citrouille.Infrastructure.Commands;

public interface ICollectionTagReadService
{
    Task<Tag> GetByNameAsync(string name);
}