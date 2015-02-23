namespace AOPMetrics.Core.Interfaces.Entities
{
    public interface IEntity<TId>
    {
        TId Id { get; set; }
    }
}