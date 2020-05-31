namespace Dal.Entities
{
    /// <summary>
    /// base entity abstraction
    /// </summary>
    public abstract class EntityBase : IEntity<int>
    {
        public int Id { get; set; }
    }

    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
