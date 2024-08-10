using System.ComponentModel.DataAnnotations.Schema;

namespace Rocketseat.Domain.Entities;

public class Message
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("Room_Id")]
    public Guid RoomId { get; set; }

    [Column("Message")]
    public string MessageText { get; set; }
    [Column("Reaction_Count")]

    public long ReactionCount { get; set; } = 0;

    public bool Answered { get; set; } = false;

    [ForeignKey("RoomId")]
    public Room Room { get; set; } = default!;
}