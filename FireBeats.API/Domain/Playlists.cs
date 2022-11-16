using System.Text.Json.Serialization;

namespace FireBeats.Domain
{
    public class Playlists
    {
        public Guid Id { get; set; }
        public string PlaylistName { get; set; } = string.Empty;

        // Foreign Keys
        public Guid UserId { get; set; }

        // References to the other entitiess
        public virtual Users User { get; set; }
        public virtual ICollection<Songs>? Songs { get; set; }
    }
}
