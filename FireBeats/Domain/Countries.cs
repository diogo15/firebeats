﻿namespace FireBeats.Domain
{
    public class Countries
    {
        public Guid Id { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }

        // "Foreign Keys"
        //Relation One to Many to the Cities Class
        public virtual ICollection<Cities> Cities { get; set; }
    }
}
