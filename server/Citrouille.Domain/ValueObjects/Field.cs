namespace Citrouille.Domain.ValueObjects;

public record Field : FieldTemplate
{
    public string Value { get; }
    
    public Field(string name, string type, string value) : base(name, type)
    {
        Value = value;
    }
}