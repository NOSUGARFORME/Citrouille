using AutoMapper;
using Citrouille.Data.Entities;
using Citrouille.Shared.Mapping;

namespace Citrouille.Infrastructure.Queries.Models;

public class CollectionItemDto : IMapFrom<CollectionItem>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<CollectionItemFieldDto> Fields { get; set; }
    
    public virtual void Mapping(Profile mapper)
        => mapper
            .CreateMap<CollectionItem, CollectionItemDto>();
}