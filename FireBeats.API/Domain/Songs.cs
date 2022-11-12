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
    }
}
