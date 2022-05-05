using Newtonsoft.Json;

namespace Citrouille.Data.Entities;

public record Field(string Name, string Type, string Value) : FieldTemplate(Name, Type)
{
    public CollectionItem Item { get; set; }
    public new static Field Create(string value)
    {
        return JsonConvert.DeserializeObject<Field>(value);
    }
    
    public override string ToString()
        => JsonConvert.SerializeObject(this);
}