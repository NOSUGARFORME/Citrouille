using AutoMapper;
using Citrouille.Data.Entities;
using Citrouille.Shared.Mapping;

namespace Citrouille.Infrastructure.Queries.Models;

public class CollectionThemeDto : IMapFrom<CollectionTheme>
{
    public Guid Id { get; set; }
    public string Theme { get; set; }
    
    public virtual void Mapping(Profile mapper)
        => mapper
            .CreateMap<CollectionTheme, CollectionThemeDto>();
}