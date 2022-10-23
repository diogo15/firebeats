namespace FireBeats.Domain
{
    public class Cities
    {
        public Guid Id { get; set; }
        public string CityName { get; set; }

        // "Foreign Keys"

        // Relation One to One to the Users Class
        /*
         * For relations one to one:
         * On each class add the vrtual reference to each class
         */
        public virtual Users Users { get; set; }

        // Relation One to Many to the Countries Class
        /*
         * For relations one to many:
         * The class you want to be the one in the relationship
         */
        public virtual Countries Country { get; set; }
    }
}
