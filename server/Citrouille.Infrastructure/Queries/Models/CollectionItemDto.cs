namespace Citrouille.Infrastructure.Queries.Models;

public class CollectionItemDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<CollectionItemFieldDto> Fields { get; set; }
}