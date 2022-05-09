using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Citrouille.Data.Entities;

public class LikedItem
{
    public User User { get; set; }
    public string UserId { get; set; }
    public CollectionItem Item { get; set; }
    public Guid ItemId { get; set; }
    
    private LikedItem() {}

    internal LikedItem(CollectionItem item, string userId)
    {
        UserId = userId;
        ItemId = item.Id;
    }
    
    internal sealed class Configuration : IEntityTypeConfiguration<LikedItem>
    {
        public void Configure(EntityTypeBuilder<LikedItem> builder)
        {
            builder.HasKey(ct => new { ct.ItemId, ct.UserId });
            builder.ToTable("LikedItems");

            builder
                .HasOne(ct => ct.User)
                .WithMany(c => c.Likes);

            builder
                .HasOne(ct => ct.Item)
                .WithMany(t => t.Likes);
        }
    }
}