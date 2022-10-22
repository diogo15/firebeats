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
  
    }
}
