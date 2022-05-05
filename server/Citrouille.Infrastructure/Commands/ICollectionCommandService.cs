using Citrouille.Data.Entities;
using Citrouille.Infrastructure.Commands.Models;

namespace Citrouille.Infrastructure.Commands;

public interface ICollectionCommandService
{
    Task CreateCollectionAsync(Collection collection);
    Task AddCollectionItemAsync(AddCollectionItem command);
    Task UpdateCollectionAsync(Collection collection); // ?
    Task UpdateCollectionItemAsync(UpdateCollectionItem command);
    Task RemoveCollectionAsync(RemoveCollection command);
    Task RemoveCollectionItemAsync(RemoveCollectionItem command);
}