namespace FireBeats.Domain
{
    public class Playlists
    {
        public Playlists()
        {
            this.Albums = new HashSet<Albums>();
            this.Songs = new HashSet<Songs>();
        }

        public Guid Id { get; set; }
        public string PlaylistName { get; set; } = string.Empty;

        // Foreign Keys
        public Guid UserId { get; set; }

        // References to the other entitiess
        public virtual Users User { get; set; }
        public virtual ICollection<Albums>? Albums { get; set; }
        public virtual ICollection<Songs>? Songs { get; set; }
    }
}
