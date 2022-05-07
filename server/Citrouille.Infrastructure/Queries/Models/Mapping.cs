using Citrouille.Data.Entities;

namespace Citrouille.Infrastructure.Queries.Models;

internal static class Mapping
{
    public static CollectionDto AsDto(this Collection value)
    {
        return new CollectionDto
        {
            Id = value.Id,
            Title = value.Title,
            Description = value.Description,
            Theme = new CollectionThemeDto
            {
                Id = value.Theme.Id, 
                Theme = value.Theme.Theme
            },
            Items = value.Items?.Select(i => new CollectionItemDto
            {
                Id = i.Id,
                Name = i.Name,
                Fields = i.Fields?.Select(f => new CollectionItemFieldDto
                {
                    Name = f.Name,
                    Type = f.Type,
                    Value = f.Value
                })
            }),
            Tags = value.Tags?.Select(t => t.Tag.Name),
            Fields = value.Fields?.Select(f => new CollectionFieldTemplateDto
            {
                Name = f.Name,
                Type = f.Type
            }),
        };
    }
}