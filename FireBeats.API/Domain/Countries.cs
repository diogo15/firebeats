﻿namespace FireBeats.Domain
{
    public class Countries
    {
        public Guid Id { get; set; }
        public string CountryName { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;

        // "Foreign Keys"
        //Relation 1:M to the Cities Class
        public virtual ICollection<Cities> Cities { get; set; }
    }
}
