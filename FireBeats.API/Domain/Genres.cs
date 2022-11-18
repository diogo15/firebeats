using FireBeats.Domain;
using System.Text.Json.Serialization;

namespace FireBeats.API.Domain
{
    public class Genres
    {
        public Guid Id { get; set; }
        public string GenreName { get; set; } = string.Empty;
        [JsonIgnore]
        public virtual ICollection<Songs> Songs { get; set; }
    }
}
