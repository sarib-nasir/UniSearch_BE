﻿using System;
using System.Collections.Generic;

namespace UniSearch.Models
{
    public class LANGUAGES
    {
        public LANGUAGES()
        {
            PROGRAMS = new List<PROGRAMS>();
        }
        public Guid LANGUAGE_ID { get; set; }
        public string LANGUAGE { get; set; }
        public string LANGUAGE_CODE { get; set; }
        public bool IS_ACTIVE { get; set; }
        public List<PROGRAMS> PROGRAMS { get; set; }
    }
}
