namespace Citrouille.Shared.Abstractions.Domain;

public abstract class AggregateRoot<TKey> : Entity<TKey>
{
    private readonly List<IDomainEvent> _events = new();
    private bool _versionIncremented;
    
    public int Version { get; protected set; }
    public IEnumerable<IDomainEvent> Events => _events;
    
    public void ClearEvents() => _events.Clear();
    
    protected void AddEvent(IDomainEvent @event)
    {
        if (!_events.Any() && !_versionIncremented)
        {
            Version++;
            _versionIncremented = true;
        }

        _events.Add(@event);
    }

    protected void IncrementVersion()
    {
        if (_versionIncremented)
        {
            return;
        }

        Version++;
        UpdatedDateTime = DateTimeOffset.Now;
        _versionIncremented = true;
    }
}