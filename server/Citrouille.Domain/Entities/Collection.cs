using Citrouille.Domain.Events;
using Citrouille.Domain.Exceptions;
using Citrouille.Domain.ValueObjects;
using Citrouille.Shared.Domain;

namespace Citrouille.Domain.Entities;

public class Collection : AggregateRoot<CollectionId>
{
    public CollectionId Id { get; private set; }
    
    private CollectionTitle _name;
    private CollectionDescription _description;
    private CollectionTheme _theme;
    private List<FieldTemplate> _fields;
    private List<Tag> _tags;
    private readonly LinkedList<CollectionItem> _items = new();

    private Collection(CollectionId id, CollectionTitle name, CollectionDescription description, CollectionTheme theme,
        List<Tag> tags, List<FieldTemplate> fields, LinkedList<CollectionItem> items)
        : this(id, name, description, theme, tags, fields)
    {
        _items = items;
    }

    private Collection() {}
    
    internal Collection(CollectionId id, CollectionTitle name, CollectionDescription description, CollectionTheme theme, List<Tag> tags, List<FieldTemplate> fields)
    {
        Id = id;
        _name = name;
        _description = description;
        _theme = theme;
        _tags = tags;
        _fields = fields;
    }
    
    public void AddItem(CollectionItem item)
    {
        var alreadyExists = _items.Any(i => i.Name == item.Name);

        if (alreadyExists)
        {
            throw new ItemAlreadyExistsException(_name, item.Name);
        }

        _items.AddLast(item);
        AddEvent(new CollectionItemAdded(this, item));
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
        _items.Remove(item);
        AddEvent(new CollectionItemRemoved(this, item));
    }
    
    private CollectionItem GetItem(string itemName)
    {
        var item = _items.SingleOrDefault(i => i.Name == itemName);

        if (item is null)
        {
            throw new CollectionItemNotFoundException(itemName);
        }

        return item;
    }
}
