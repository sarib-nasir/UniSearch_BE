using System;

namespace UniSearch.Extensions
{
    public class ApiResponse
    {
        public string statusCode { get; set; } = "11";
        public string message { get; set; } = "No data found";
        public Object data { get; set; } = null;
        public string Token { get; set; }
        public string typeId { get; set; }
        public int NetworkStatus { get; set; }
    }
}
