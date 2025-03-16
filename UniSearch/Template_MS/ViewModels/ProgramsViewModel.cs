using System;

namespace UniSearch.ViewModels
{
    public class ProgramsViewModel
    {
        public Guid PROGRAM_ID { get; set; }
        public string PROGRAM_NAME { get; set; }
        public string PROGRAM_TAGS { get; set; }
        public string PROGRAM_MINIMUM_GPA { get; set; }
        public int? SEMESTERS { get; set; }
        public string SEMESTER_START { get; set; }
        public string PROGRAM_TYPE_ID { get; set; }
        public string IELTS_SCORE { get; set; }
        public string APPLICATION_DEADLINE { get; set; }
        public string LINKS { get; set; }
        public string LANGUAGE_ID { get; set; }
        public string UNIVERSITY_ID { get; set; }
        public bool IS_ACTIVE { get; set; }
    }
}
