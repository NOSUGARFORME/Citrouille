namespace Citrouille.Infrastructure.Commands;

public interface ICollectionReadService
{
    Task<bool> ExistsByTitleAsync(string name);
}