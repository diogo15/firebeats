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

        // "Foreign Keys"
        // Relation One to One to the City Class
        // Reference to the class wanted to be the related to
        [ForeignKey("Cities")]
        public Guid CityId { get; set; }
        public virtual Cities Cities { get; set; }
    }
}
