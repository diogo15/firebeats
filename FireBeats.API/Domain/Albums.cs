using System.Text.Json.Serialization;

namespace FireBeats.Domain
{
    public class Albums
    {
        public Guid Id { get; set; }
        public string AlbumName { get; set; } = string.Empty;

        // Foreign Key
        public Guid? UserId { get; set; }

        // One album may be associated with one User
        public virtual Users User { get; set; }
        // An Album can have many Songs
        [JsonIgnore]
        public virtual ICollection<Songs>? Songs { get; set; }
    }
}
