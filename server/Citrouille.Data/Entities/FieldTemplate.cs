using Newtonsoft.Json;

namespace Citrouille.Data.Entities;

public record FieldTemplate(string Name, string Type)
{
    public static FieldTemplate Create(string value)
    {
        return JsonConvert.DeserializeObject<FieldTemplate>(value);
    }

    public override string ToString()
        => JsonConvert.SerializeObject(this);
}
