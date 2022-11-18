using System.Text.Json.Serialization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FireBeats.Domain
{
    public class Users
    {
        public Guid Id { get; set; }

        [StringLength(50)]
        [DisplayName("User")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [DisplayName("Email")]
        public string UserEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string UserPassword { get; set; }

        public bool Artist { get; set; }

        // Foreign Keys ->
        // Reference to the class wanted to be the related to
        public Guid CitiesId { get; set; }
        public Cities Cities { get; set; }

        // One User can have Many Playlists
        public ICollection<Playlists> Playlists { get; set; }
    }
}
