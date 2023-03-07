using Newtonsoft.Json;
using Mastermind.Domain;
using Microsoft.EntityFrameworkCore;
namespace Mastermind.Infrastructure;

public class MastermindContext : DbContext
{
    public MastermindContext(DbContextOptions<MastermindContext> options)
        : base(options)
    {
    }

    public DbSet<Game> Games { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>(entity =>
        {
            entity.Property(g => g.GameState)
                .HasConversion(
                    v => v.ToString(),
                    v => (GameStatus)Enum.Parse(typeof(GameStatus), v));
            entity.Property(g => g.Clues)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v.Select(v => v.ToString()).ToList()),
                    v => JsonConvert.DeserializeObject<List<string>>(v).Select(v=> (Clue)Enum.Parse(typeof(Clue), v)).ToList());
            entity.Property(g => g.LatestPlayerGuess)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v.Select(v => v.ToString()).ToList()),
                    v => JsonConvert.DeserializeObject<List<string>>(v).Select(v=> (Colour)Enum.Parse(typeof(Colour), v)).ToList());
            entity.Property(g => g.SelectedColours)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v.Select(v => v.ToString()).ToList()),
                    v => JsonConvert.DeserializeObject<List<string>>(v).Select(v=> (Colour)Enum.Parse(typeof(Colour), v)).ToList());
        });
    }
}