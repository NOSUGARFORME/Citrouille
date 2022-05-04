using Citrouille.Data.Exceptions.CollectionItem;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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
    
    internal sealed class Configuration : IEntityTypeConfiguration<CollectionItem>
    {
        public void Configure(EntityTypeBuilder<CollectionItem> builder)
        {
            builder.ToTable("Items");
            builder.HasKey(i => i.Id);

            // var fieldConverter = new ValueConverter<List<Field>, string[]>(fields => fields.Select(f => f.ToString()).ToArray(),
            //     fields => fields.Select(Field.Create).ToList());
            //
            // builder
            //     .Property(i => i.Fields)
            //     .HasConversion(fieldConverter)
            //     .HasColumnName("Fields");
            
            builder.OwnsMany(i => i.Fields);
        }
    }
}