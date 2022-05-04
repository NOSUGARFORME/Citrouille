using Citrouille.Data.Exceptions.Tag;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Citrouille.Data.Entities;

public class Tag
{
    public Tag()
    {
    }

    public Tag(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new EmptyCollectionTagNameException();
        }

        Name = name;
    }
    
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<CollectionTag> Collections { get; set; }
}