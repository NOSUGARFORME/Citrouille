namespace Citrouille.Infrastructure.Queries.Models.Collections;

public class CollectionQuery
{
    public Guid? Theme { get; set; }
    public string SortBy { get; set; }
    public string Order { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}