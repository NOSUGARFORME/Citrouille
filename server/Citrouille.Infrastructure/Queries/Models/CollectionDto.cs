namespace Citrouille.Infrastructure.Queries.Models;

public class CollectionDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public CollectionThemeDto Theme { get; set; } 
    public IEnumerable<TagDto> Tags { get; set; }
    public IEnumerable<CollectionItemDto> Items { get; set; }
}