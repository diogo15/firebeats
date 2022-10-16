using FireBeats.Domain;
using Microsoft.EntityFrameworkCore;

namespace FireBeats.Context
{
    public class FireBeatsContext : DbContext
    {
        public FireBeatsContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Canciones> Canciones { get; set; }
    }
}
