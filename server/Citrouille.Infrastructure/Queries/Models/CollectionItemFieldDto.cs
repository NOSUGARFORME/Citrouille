using AutoMapper;
using Citrouille.Data.Entities;
using Citrouille.Shared.Mapping;

namespace Citrouille.Infrastructure.Queries.Models;

public class CollectionItemFieldDto : CollectionFieldTemplateDto, IMapFrom<Field>
{
    public string Value { get; set; }
    
    public override void Mapping(Profile mapper)
        => mapper
            .CreateMap<Field, CollectionItemFieldDto>();
}