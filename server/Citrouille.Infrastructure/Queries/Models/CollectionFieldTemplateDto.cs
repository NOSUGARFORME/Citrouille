using AutoMapper;
using Citrouille.Data.Entities;
using Citrouille.Shared.Mapping;

namespace Citrouille.Infrastructure.Queries.Models;

public class CollectionFieldTemplateDto : IMapFrom<FieldTemplate>
{
    public string Name { get; set; }
    public string Type { get; set; }
    
    public virtual void Mapping(Profile mapper)
        => mapper
            .CreateMap<FieldTemplate, CollectionFieldTemplateDto>();
}