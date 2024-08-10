using Microsoft.EntityFrameworkCore;
using Rocketseat.Domain.Entities;

namespace Rocketseat.Infra.Context;

public class RocketseatDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Message> Messages { get; set; }
}