using System;
using System.Collections.Generic;

namespace UniSearch.Models
{
    public class COUNTRIES
    {
        public COUNTRIES()
        {
            UNIVERSITIES = new List<UNIVERSITIES>();
        }
        public Guid COUNTRY_ID { get; set; }
        public string COUNTRY_NAME { get; set; }
        public bool IS_ACTIVE { get; set; }
        public List<UNIVERSITIES> UNIVERSITIES { get; set; }

    }
}
