namespace Rocketseat.Domain.Entities;

public class Room
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Theme { get; set; }
}