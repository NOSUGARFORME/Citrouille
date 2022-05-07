using AutoMapper;
using Citrouille.Data.Entities;
using Citrouille.Shared.Mapping;

namespace Citrouille.Infrastructure.Queries.Models;

public class CollectionDto : IMapFrom<Collection>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public CollectionThemeDto Theme { get; set; } 
    public IEnumerable<string> Tags { get; set; }
    public IEnumerable<CollectionItemDto> Items { get; set; }
    public IEnumerable<CollectionFieldTemplateDto> Fields { get; set; }

    public virtual void Mapping(IProfileExpression profile)
        => profile
            .CreateMap<Collection, CollectionDto>()
            .ForMember(dto => dto.Tags, 
        opt => opt
                .MapFrom(c => c.Tags.Select(ct => ct.Tag.Name)
                    .ToList()));
}
