using Microsoft.EntityFrameworkCore;

namespace FptwHotels.Data;

public sealed class HotelDbContext : DbContext
{
    public DbSet<Room> Rooms { get; set; }

    public string DbPath { get; }

    public HotelDbContext()
    {
        const Environment.SpecialFolder folder = Environment.SpecialFolder.LocalApplicationData;
        string path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "fptw-hotels.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}