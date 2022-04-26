using Citrouille.Domain.Exceptions;
using Citrouille.Domain.ValueObjects;

namespace Citrouille.Domain.Entities;

public record CollectionItem
{
    public string Name { get; }
    public List<Field> Fields { get; init; }
    
    public CollectionItem(string name, List<Field> fields)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new EmptyItemNameException();
        }
        
        Name = name;
        Fields = fields;
    }
}