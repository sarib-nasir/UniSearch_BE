using System;

namespace UniSearch.ViewModels
{
    public class TableBaseViewModel
    {
        public bool isActive { get; set; }
        public Nullable<Guid> InputBy { get; set; }
        public Nullable<System.DateTime> InputDate { get; set; }
        public string InputIP { get; set; }
        public Nullable<Guid> ModifyBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public string ModifyIP { get; set; }
    }
}
