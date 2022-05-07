using Citrouille.Data.Entities;
using Citrouille.Shared.Mapping;

namespace Citrouille.Infrastructure.Queries.Models;

public class TagDto : IMapFrom<Tag>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}