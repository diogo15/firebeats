using System.Text.Json.Serialization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FireBeats.Domain
{
    public class Songs
    {
        public Guid Id { get; set; }
        public string SongName { get; set; } = string.Empty;
        public string SongPath { get; set; } = string.Empty;
        public bool isFavorite { get; set; } = false;

        // Foreign Key
        public Guid? PlaylistId { get; set; }
        public Guid? AlbumId { get; set; }
        public virtual Playlists? Playlist { get; set; }
        public virtual Albums? Album { get; set; }
    }
}
