using Newtonsoft.Json;
using System;

namespace BackendProject.Entity.DomainModels
{


    public class Fields
    {
        public double ElapsedMilliseconds { get; set; }
        public int StatusCode { get; set; }
        public string Method { get; set; }
        public string Path { get; set; }
        public string QueryString { get; set; }
        public string RequestId { get; set; }
        public string RequestPath { get; set; }

        [JsonProperty("@timestamp")]
        public string Timestamp { get; set; }
    }
    public class Source
    {
        [JsonProperty("@timestamp")]
        public DateTime Timestamp { get; set; }
        public string level { get; set; }
        public string messageTemplate { get; set; }
        public string message { get; set; }
        public Fields fields { get; set; }
    }

    public class Monitor
    {
        public Fields fields { get; set; }
        public Source source { get; set; }
    }




}
