namespace Citrouille.Application.Services;

public interface ICollectionReadService
{
    Task<bool> ExistsByNameAsync(string name);
}