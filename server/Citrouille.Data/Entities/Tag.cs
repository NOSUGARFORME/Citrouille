using Citrouille.Data.Exceptions.Tag;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Citrouille.Data.Entities;

public class Tag
{
    private Tag()
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
    public ICollection<CollectionTag> Collections { get; set; }
    
    internal class Configuration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasKey(t => t.Id);
            builder.ToTable("Tags");
        }
    }
}