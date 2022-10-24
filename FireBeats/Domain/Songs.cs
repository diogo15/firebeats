using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FireBeats.Domain
{
    public class Songs
    {
        [Required]
        public Guid Id { get; set; }
        public string SongName { get; set; }
        public string SongPath { get; set; }
        public bool isFavorite { get; set; } = false;

        // One Playlists can have Many Songs
        public ICollection<Playlists> Playlists { get; set; }
    }
}
