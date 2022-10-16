using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FireBeats.Domain
{
    public class Canciones
    {
        public Guid Id { get; set; }

        [MaxLength(70)]
        public string CancionName { get; set; }
        public string CancionPath { get; set; }
  
    }
}
