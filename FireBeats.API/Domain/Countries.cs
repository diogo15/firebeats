using System.Text.Json.Serialization; // Use this instead of NewtonJson

namespace FireBeats.Domain
{
    public class Countries
    {
        public Guid Id { get; set; }
        public string CountryName { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;

        // "Foreign Keys"
        //Relation 1:M to the Cities Class
        [JsonIgnore]
        public virtual ICollection<Cities> Cities { get; set; }
    }
}
