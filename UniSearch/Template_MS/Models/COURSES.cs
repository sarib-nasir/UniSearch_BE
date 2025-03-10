﻿using System;
using System.Collections.Generic;

namespace UniSearch.Models
{
    public class COURSES
    {
        public Guid COURSE_ID { get; set; }
        public string COURSE_NAME { get; set; }
        public string LANGUAGE_OF_INSTRUCTION { get; set; }
        public int? SEMESTERS { get; set; }
        public string SEMESTER_START { get; set; }
        public string COURSE_TYPE { get; set; }
        public string IELTS_SCORE { get; set; }
        public string APPLICATION_DEADLINE { get; set; }
        public string LINKS { get; set; }
        public bool IS_ACTIVE { get; set; }
    }
}
