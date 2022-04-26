namespace Citrouille.Shared.Abstractions.Domain;

public abstract class Entity<TKey>
{
    public TKey Id { get; protected set; }
    
    public DateTimeOffset CreatedDateTime { get; protected set; }
    public DateTimeOffset? UpdatedDateTime { get; protected set; }
}