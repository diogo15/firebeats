using System.ComponentModel;

namespace FireBeats.Domain
{
    public class Cities
    {
        public Guid Id { get; set; }
        public string CityName { get; set; }

        /*
         * "Foreign Keys"
         * Relation 1:M to the Countries Class
         * The class you want to be the 1 in the relationship
         */
        public Guid CountriesId { get; set; }
        
        public virtual Countries Countries { get; set; }
    }
}
