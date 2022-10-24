namespace FireBeats.Domain
{
    public class Albums
    {
        public Albums()
        {
            this.Songs = new HashSet<Songs>();
        }

        public Guid Id { get; set; }
        public string AlbumName { get; set; }

        public ICollection<Songs>? Songs { get; set; }
    }
}
