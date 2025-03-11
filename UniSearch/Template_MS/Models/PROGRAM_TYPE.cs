using System;
using System.Collections.Generic;

namespace UniSearch.Models
{
    public class PROGRAM_TYPE
    {
        public PROGRAM_TYPE()
        {
            PROGRAMS = new List<PROGRAMS>();
        }
        public Guid PROGRAM_TYPE_ID { get; set; }
        public string PROGRAM_TYPE_NAME { get; set; }
        public bool IS_ACTIVE { get; set; }
        public List<PROGRAMS> PROGRAMS { get; set; }
    }
}
