using Citrouille.Data.Exceptions.CollectionItem;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Citrouille.Data.Entities;

public record CollectionItem
{
    public CollectionItem()
    {
    }
    
    public CollectionItem(string name, List<Field> fields)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new EmptyCollectionItemNameException();
        }
        
        Name = name;
        Fields = fields;
    }
    
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<Field> Fields { get; set; }
    
    public Collection Collection { get; set; }
    public List<LikedItem> Likes { get; set; } = new();
    public List<CommentedItem> Comments { get; set; } = new();

    public void ChangeLikeStatus(string userId)
    {
        var liked = Likes.FirstOrDefault(li => li.ItemId.Equals(Id) && li.UserId == userId);
        
        if (liked is null)
        {
            Likes.Add(new LikedItem(this, userId));
            return;
        }

        Likes.Remove(liked);
    }
    
    public void AddComment(string userId, string comment) 
        => Comments.Add(new CommentedItem(this, userId, comment));


    internal sealed class Configuration : IEntityTypeConfiguration<CollectionItem>
    {
        public void Configure(EntityTypeBuilder<CollectionItem> builder)
        {
            builder.ToTable("Items");
            builder.HasKey(i => i.Id);
            
            builder.OwnsMany(i => i.Fields);
        }
    }
}
