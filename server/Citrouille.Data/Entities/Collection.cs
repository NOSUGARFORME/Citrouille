using Citrouille.Data.Exceptions.Collection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Citrouille.Data.Entities;

public class Collection
{
    public Collection()
    {
    }
    
    public Collection(Guid id, string title, string description)
    {
        Id = id;
        Title = title;
        Description = description;
    }
    
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public CollectionTheme Theme { get; set; }
    public List<FieldTemplate> Fields { get; set; }
    public List<CollectionTag> Tags { get; set; }
    public List<CollectionItem> Items { get; } = new();
    
    internal sealed class Configuration : IEntityTypeConfiguration<Collection>
    {
        public void Configure(EntityTypeBuilder<Collection> builder)
        {
            builder.ToTable("Collections");
            builder.HasKey(c => c.Id);
            
            // var fieldConverter = new ValueConverter<List<FieldTemplate>, string[]>(list => list.Select(ft => ft.ToString()).ToArray(),
            //     list => list.Select(FieldTemplate.Create).ToList());
            //
            // builder
            //     .Property(c => c.Fields)
            //     .HasConversion(fieldConverter)
            //     .HasColumnName("Fields");
            
            builder.OwnsMany(c => c.Fields);
            
            builder
                .HasMany(c => c.Items)
                .WithOne(ci => ci.Collection);

            builder
                .HasOne(c => c.Theme)
                .WithMany(t => t.Collections);
        }
    }
    public void ThrowIfInvalid()
    {
        if (Id == Guid.Empty)
            throw new EmptyCollectionIdException();

        if (string.IsNullOrWhiteSpace(Title))
            throw new EmptyCollectionTitleException();

        if (string.IsNullOrWhiteSpace(Description))
            throw new EmptyCollectionDescriptionException();
    }
    
    public void AddItem(CollectionItem item)
    {
        var alreadyExists = Items.Any(i => i.Name == item.Name);

        if (alreadyExists)
        {
            throw new ItemAlreadyExistsException(Title, item.Name);
        }

        Items.Add(item);
    }
    
    public void AddItems(IEnumerable<CollectionItem> items)
    {
        foreach (var item in items)
        {
            AddItem(item);
        }
    }
    
    public void RemoveItem(string itemName)
    {
        var item = GetItem(itemName);
        Items.Remove(item);
    }
    
    private CollectionItem GetItem(string itemName)
    {
        var item = Items.SingleOrDefault(i => i.Name == itemName);

        if (item is null)
        {
            throw new CollectionItemNotFoundException(itemName);
        }

        return item;
    }
}
