using System;
using System.Collections.Generic;

namespace UniSearch.Models
{
    public class PROGRAMS
    {
        public Guid PROGRAM_ID { get; set; }
        public string PROGRAM_NAME { get; set; }
        public int? SEMESTERS { get; set; }
        public string SEMESTER_START { get; set; }
        public string PROGRAM_TYPE { get; set; }
        public string IELTS_SCORE { get; set; }
        public string APPLICATION_DEADLINE { get; set; }
        public string LINKS { get; set; }
        public string LANGUAGE_ID { get; set; }
        public string UNIVERSITY_ID { get; set; }
        public bool IS_ACTIVE { get; set; }
    }
}
