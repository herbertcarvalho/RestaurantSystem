namespace Domain.Extensions
{
    public abstract class Entity : IEntity
    {
        protected Entity()
        {
            CreatedAt = DateTime.UtcNow;
        }
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}