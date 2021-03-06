using Citrouille.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Citrouille.Data;

public class CollectionDbContext : IdentityDbContext<User>
{
    public DbSet<Collection> Collections { get; set; }
    public DbSet<CollectionTheme> Themes { get; set; }
    public DbSet<Tag> Tags { get; set; }

    public CollectionDbContext(DbContextOptions<CollectionDbContext> options) 
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("CollectionSchema");

        modelBuilder.ApplyConfiguration(new Collection.Configuration());
        modelBuilder.ApplyConfiguration(new CollectionItem.Configuration());
        modelBuilder.ApplyConfiguration(new CollectionTag.Configuration());
        modelBuilder.ApplyConfiguration(new Tag.Configuration());
        modelBuilder.ApplyConfiguration(new LikedItem.Configuration());
        modelBuilder.ApplyConfiguration(new CommentedItem.Configuration());
    }
}
