namespace FireBeats.Domain
{
    public class Albums
    {
        public Albums()
        {
            this.Playlists = new HashSet<Playlists>();
        }

        public Guid Id { get; set; }
        public string AlbumName { get; set; }

        // Foreign Key
        public Guid SongId { get; set; }
        public Guid UserId { get; set; }

        // An unique song can be in one or zero albums
        public virtual Songs Song { get; set; }
        public virtual Users User { get; set; }
        public ICollection<Playlists>? Playlists { get; set; }
    }
}
