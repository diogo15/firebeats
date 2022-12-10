using System.Text.Json.Serialization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using FireBeats.API.Domain;

namespace FireBeats.Domain
{
    public class Songs
    {
        public Guid Id { get; set; }
        public string SongName { get; set; } = string.Empty;
        public string SongPath { get; set; } = string.Empty;
        public bool? isFavorite { get; set; } = false;

        // Foreign Key
        public Guid? GenreId { get; set; }
        public Guid? PlaylistId { get; set; }
        public Guid? AlbumId { get; set; }

        // References To
        [JsonIgnore]
        public virtual Genres? Genre { get; set; }
        [JsonIgnore]
        public virtual Playlists? Playlist { get; set; }
        [JsonIgnore]
        public virtual Albums? Album { get; set; }
    }
}
