using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Citrouille.Data.Entities;

public class CommentedItem
{
    public User User { get; set; }
    public string UserId { get; set; }
    public CollectionItem Item { get; set; }
    public Guid ItemId { get; set; }
    
    public string Text { get; set; }
    public DateTimeOffset CommentedAt { get; set; }
    
    private CommentedItem() {}

    internal CommentedItem(CollectionItem item, string userId, string text)
    {
        UserId = userId;
        ItemId = item.Id;
        Text = text;
        CommentedAt = DateTimeOffset.Now;
    }
    
    internal sealed class Configuration : IEntityTypeConfiguration<CommentedItem>
    {
        public void Configure(EntityTypeBuilder<CommentedItem> builder)
        {
            builder.HasKey(ct => new { ct.ItemId, ct.UserId });
            builder.ToTable("ItemComments");

            builder
                .HasOne(ct => ct.User)
                .WithMany(c => c.Comments);

            builder
                .HasOne(ct => ct.Item)
                .WithMany(t => t.Comments);
        }
    }
}