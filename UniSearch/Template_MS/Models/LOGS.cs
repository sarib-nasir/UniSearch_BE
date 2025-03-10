using System;

namespace UniSearch.Models
{
    public class LOGS
    {
        public Guid LOGD_ID { get; set; }
        public Nullable<Guid> INPUT_BY { get; set; }
        public Nullable<System.DateTime> INPUT_DATE { get; set; }
        public string INPUT_IP { get; set; }
        public string INPUT_MAC { get; set; }
        public string INPUT_BROWSER { get; set; }
        public string INPUT_BRO_VERSION { get; set; }
        public string URL { get; set; }
        public string STATUS_CODE { get; set; }
        public string ACTION { get; set; }
        public string API_REQUESTDATA { get; set; }
        public string API_RESPONSEDATA { get; set; }
        public string TYPE_ID { get; set; }
        public Nullable<bool> IS_CLIENT_API { get; set; }
        
    }
}
