using System;
using System.Text.Json.Serialization;

namespace UniSearch.Extensions
{
    //public class ApiResponse
    //{
    //    public string statusCode { get; set; } = "11";
    //    public string message { get; set; } = "No data found";
    //    public Object data { get; set; } = null;
    //    public string Token { get; set; }
    //    public string typeId { get; set; }
    //    public int NetworkStatus { get; set; }
    //}
    public class ApiResponse
    {
        [JsonPropertyName("status_code")]
        public string statusCode { get; set; }

        [JsonPropertyName("message")]
        public string message { get; set; }

        [JsonPropertyName("data")]
        public object data { get; set; }

        [JsonPropertyName("columns")]
        public object columns { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("type_id")]
        public string typeId { get; set; }

        [JsonPropertyName("network_status")]
        public int NetworkStatus { get; set; }
    }
}
