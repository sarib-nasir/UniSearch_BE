using System;
using System.Collections.Generic;

namespace UniSearch.Models
{
    public class UNIVERSITIES
    {
        public Guid UNIVERSITY_ID { get; set; }
        public string UNIVERSITY_NAME { get; set; }
        public string UNIVERSITY_iMAGE { get; set; }
        public string CITY { get; set; }
        public string UNIVERSITY_TYPE { get; set; }
        public bool IS_ACTIVE { get; set; }
        public Guid COUNTRY_ID { get; set; }
        public COUNTRIES COUNTRIES { get; set; }

    }
}
