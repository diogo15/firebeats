using FireBeats.Domain;
using Microsoft.EntityFrameworkCore;

namespace FireBeats.Context
{
    public class FireBeatsContext : DbContext
    {
        public FireBeatsContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {

        }

        // Add every single class you want to add to the database, so the migration que make the schema
        public DbSet<Countries> Countries { get; set; }
        public DbSet<Cities> Cities { get; set; }
        public DbSet<Songs> Songs { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Playlists> Playlists { get; set; }
        public DbSet<Albums> Albums { get; set; }
    }
}
