namespace Domain.Extensions;

internal interface IEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
}