using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Citrouille.Data.Entities;

public class User : IdentityUser 
{
    public List<LikedItem> Likes { get; set; }
    public List<CommentedItem> Comments { get; set; }
    
    internal sealed class Configuration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
        }
    }
}